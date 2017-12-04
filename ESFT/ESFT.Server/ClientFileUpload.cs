using ESFT.Common;
using ESFT.Common.Log;
using ESFT.Common.SystemInfo;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace ESFT.Server
{
    /// <summary>
    /// 客户端请求上传文件
    /// </summary>
    public class ClientFileUpload : FileTransferBase
    {
        /// <summary>
        /// 是否已经存在该文件的写线程，如果存在则使用之前的写线程
        /// </summary>
        bool mExistServerFileWrite = false;
        FileManage.ServerFileWrite mServerFileWrite = null;

        public ClientFileUpload(AsyncUserToken iUserToken)
        {
            this.mUserToken = iUserToken;
            this.mTransferType = TransferType.UploadFile;
        }

        #region 处理消息
        protected override void HandleCommandMsg(MsgCommand iMsg)
        {
            if (iMsg.MsgType == EMessageType.M_ClientRequestUploadFile)
            {
                HandleClientRequestUploadFileMsg(iMsg);
            }
        }

        protected override void HandleParameterMsg(MsgParameter iMsg)
        {
            HandleClientSendParameterInfo(iMsg);
        }

        protected override void HandleFileInfoMsg(MsgFileInfo iMsg)
        {
            HandleClientSendFileInfo(iMsg);
        }

        protected override void HandleFileBlockMsg(MsgFileBlock iMsg)
        {
            ReceiveFileBlock(iMsg);
        }
        #endregion

        /// <summary>
        /// 1、客户端请求上传文件消息处理,第一次请求
        /// </summary>
        /// <param name="iMsg"></param>
        protected void HandleClientRequestUploadFileMsg(EsftMsg iMsg)
        {
            this.SetTransferState(TransferState.Connecting);
            ////客户端请求上传文件
            // 2、服务器回复要求传递参数信息
            MsgCommand tellClientSendFileInfo = new MsgCommand(EMessageType.M_ServerRequestParameterInfo, "服务器要求传递文件信息");
            this.SendMsg(tellClientSendFileInfo);

            this.mBeginTime = DateTime.Now;
            log4net.LogManager.GetLogger(this.GetType().ToString()).Debug("客户端" + this.mUserToken.mClientEndPoint.ToString() + "请求上传文件");
        }

        /// <summary>
        /// 3、接收客户端传递的参数信息
        /// </summary>
        /// <param name="iMsg"></param>
        protected void HandleClientSendParameterInfo(MsgParameter iMsg)
        {
            this.SetTransferState(TransferState.GetParameter);
            this.mParameter = iMsg.Parameters;

            //// 4、服务器回复要求传递文件信息
            MsgCommand tellClientSendFileInfo = new MsgCommand(EMessageType.M_ServerReceiveParametersSuccess, "服务器接收参数信息成功");
            this.SendMsg(tellClientSendFileInfo);
        }

        /// <summary>
        /// 5、接收客户端发送的文件信息
        /// </summary>
        /// <param name="iMsg"></param>
        protected void HandleClientSendFileInfo(MsgFileInfo iMsg)
        {
            this.SetTransferState(TransferState.GetFileInfo);

            // 保存客户端传递的文件信息
            this.mServerFilePath = iMsg.ServerDirectoryName;
            if (this.mServerFilePath.Length > 0 && (this.mServerFilePath[this.mServerFilePath.Length - 1] == '\\' || this.mServerFilePath[this.mServerFilePath.Length - 1] == '/'))
                this.mServerFilePath = this.mServerFilePath.Substring(0, this.mServerFilePath.Length - 1);
            this.mServerFileName = iMsg.ServerFileName;
            this.mClientFilePath = iMsg.ClietnDirectoryName;
            this.mClientFileName = iMsg.ClientFileName;
            this.mFileExtension = iMsg.Extension;
            this.mFileLenght = iMsg.FileLenght;
            this.mFileMD5 = iMsg.FileMD5;

            // 创建写线程，如果出现同名文件，则新建GUID为用户名
            this.mServerFileWrite = FileManage.ServerFileWrite.Add(this);
            if (this.mServerFileWrite != null)
            {
                this.mServerFileWrite.EventFileComplete += mServerFileWrite_EventFileComplete;
                this.mServerFileWrite.EventFileError += mServerFileWrite_EventFileError;
                this.mServerFileWrite.EventFileProgressChange += mServerFileWrite_EventFileProgressChange;
                this.mServerFileWrite.EventServerFileWriteManualDispose += EventServerFileWriteManualDispose;

                // 6、告诉客户端传递文件块信息的偏移位置
                MsgCommand tellClientSendFileBlock = new MsgCommand(EMessageType.M_ServerRequestFileBlock, this.mServerFileWrite.ReceiveFileLenght.ToString());
                this.SendMsg(tellClientSendFileBlock);

                this.SetTransferState(TransferState.Transferring);
            }
            else
            {
                this.SetTransferState(TransferState.Error);
            }
        }

        /// <summary>
        /// 7、接收客户端发送的文件块信息
        /// </summary>
        /// <param name="iMsg"></param>
        protected void ReceiveFileBlock(MsgFileBlock iMsg)
        {
            if (mServerFileWrite != null && this.mExistServerFileWrite == false)
            {
                mServerFileWrite.ReceiveData(iMsg);
                this.OnTransferTaskProgressChange(this);
            }
        }

        /// <summary>
        /// 写文件完成
        /// </summary>
        /// <param name="iLocalFileFullName"></param>
        public void mServerFileWrite_EventFileComplete(string iLocalFileFullName)
        {
            this.SetTransferState(TransferState.VerifyingFile);
            log4net.LogManager.GetLogger(this.GetType()).Debug(this.mUserToken.mClientEndPoint.ToString() + "的客户端上传 " + iLocalFileFullName + "  开始MD5值校验");
            this.mEndTime = DateTime.Now;
            string iFileMD5 = FileHash.GetMD5HashFromFile(iLocalFileFullName);


            //对比文件MD5值，验证文件有效性
            string clientFileMd5 = this.mFileMD5;
            string commandStr = iFileMD5 + "," + iLocalFileFullName + "," + this.mServerFileName + "," + this.mFileExtension;
            TransferState state = TransferState.Finish;
            MsgCommand tellClientMsg = null;

            if (iFileMD5 != null
                && iFileMD5.ToUpper() == clientFileMd5.ToUpper())
            {
                //接收文件的MD5值跟客户端文件MD5值一样，接收成功
                MsgCommand tellClientSuccess = new MsgCommand(EMessageType.M_ServerReceiveFileSuccess, commandStr);
                tellClientMsg = tellClientSuccess;

                state = TransferState.Finish;
                log4net.LogManager.GetLogger(this.GetType()).Debug(this.mUserToken.mClientEndPoint.ToString() + "的客户端上传 " + iLocalFileFullName + "    MD5值验证成功");
            }
            else
            {
                //接收文件的MD5值跟客户端文件MD5值不一样，接收失败
                MsgCommand tellClientFailure = new MsgCommand(EMessageType.M_ServerReceiveFileFailure, commandStr);
                tellClientMsg = tellClientFailure;

                state = TransferState.VerifyingError;
                MyLogManage.Debug(this.GetType(), "AsyncUserToken_Evnet_ServerWriteFileComplete", null, "服务器MD5验证失败, ' 服务端文件md5=" + iFileMD5 + "'  ,'客户端文件md5=" + clientFileMd5.ToUpper());
                log4net.LogManager.GetLogger(this.GetType()).Debug(this.mUserToken.mClientEndPoint.ToString() + "的客户端上传 " + iLocalFileFullName + "    MD5值验证失败，文件大小=" + this.FileLenght.ToString() + "，服务器MD5='" + iFileMD5 + "'  ,客户端文件md5='" + clientFileMd5.ToUpper() + "'");
            }

            ////通知程序，文件接收完成，开始处理文件
            this.ReceiveComplete(iLocalFileFullName, this.mParameter, this.mServerFilePath, this.mFileExtension);

            this.SetTransferState(state);
            this.SendMsg(tellClientMsg);
        }

        /// <summary>
        /// 写文件长度发生变化
        /// </summary>
        /// <param name="iCompleteLenght"></param>
        public void mServerFileWrite_EventFileProgressChange(long iCompleteLenght)
        {
            this.mCurrentLenght = iCompleteLenght;
            this.OnTransferTaskProgressChange(this);
        }

        /// <summary>
        /// 写文件进程出错
        /// </summary>
        /// <param name="iLocalFileFullName"></param>
        /// <param name="ex"></param>
        public void mServerFileWrite_EventFileError(string iLocalFileFullName, Exception ex)
        {
            this.Error(ex);
        }

        /// <summary>
        /// ServerFileWrite被手动回收
        /// </summary>
        /// <param name="e"></param>
        void EventServerFileWriteManualDispose(object e)
        {
        }

        /// <summary>
        /// 服务端接收文件完成 
        /// </summary>
        /// <param name="iServerFileFullName"></param>
        public void ReceiveComplete(string iServerFileFullName, ESFTParameter[] iParameter
            , string iLocalPath, string iExtension)
        {
            string[] args = new string[4];
            args[0] = iExtension;
            args[1] = System.IO.Path.GetFullPath(iServerFileFullName);
            args[2] = System.IO.Path.GetFullPath(SystemInfo.m_ServerRealFilePath + "\\" + this.mServerFilePath);
            args[3] = this.mServerFileName;

            log4net.LogManager.GetLogger(this.GetType()).Debug("开始转换文件格式，   '" + args[0] + "    ,   " + args[1] + " ,   " + args[2] + "  " + args[3] + "'");

            //CreateProcess(args);
        }

        public override long GetCurrentReceviceOrSendLenght()
        {
            if (this.mServerFileWrite == null)
            {
                return 0;
            }
            return this.mServerFileWrite.ReceiveFileLenght;
        }

        public override double GetTransferTimeSecond()
        {
            TimeSpan? ts;
            if (this.mTransferState == TransferState.Finish
                || this.mTransferState == TransferState.VerifyingError
                || this.mTransferState == TransferState.Error)
            {
                ts = this.mEndTime - this.mBeginTime;
            }
            else
            {
                ts = DateTime.Now - this.mBeginTime;
            }

            if (ts.HasValue == true)
            {
                return ts.Value.TotalSeconds;
            }
            else
            {
                return -1;
            }
        }

        public override void Dispose()
        {
            if (this.mServerFileWrite != null)
            {
                this.mServerFileWrite.Dispose();
                this.mServerFileWrite = null;
            }
        }

        public override void Error(Exception ex)
        {
            base.Error(ex);
        }

        private static void CreateProcess(string[] iArgs)
        {
            try
            {
                string str = string.Empty;
                foreach (var s in iArgs)
                {
                    str += "\"" + s + "\" ";
                }

                RunCommandUtils c = new RunCommandUtils(AppDomain.CurrentDomain.BaseDirectory + @"ESFT_FormatTool.exe", str.Trim(), AppDomain.CurrentDomain.BaseDirectory, false);
                c.WaitExit();
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("CreateProcess").Debug(ex.ToString());
            }
        }

    }
}
