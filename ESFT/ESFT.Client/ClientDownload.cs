using ESFT.Common;
using ESFT.Common.Log;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.IO;

namespace ESFT.Client
{
    public class ClientDownload : ClientSocket
    {
        DownloadFilePathType mServerFilePathType;
        string mServerFileName;
        string mLocalPath;
        string mLocalFileName;
        string mMasterServerIP;
        int mMasterPort;

        string mFileMD5;
        long mFileLenght;
        string mKey;

        /// <summary>
        /// 本地已经下载的文件大小
        /// </summary>
        long mLocalFilelenght;

        /// <summary>
        /// 下载文件长度
        /// </summary>
        public long FileLenght
        {
            get
            {
                return this.mFileLenght;
            }
        }

        /// <summary>
        /// 已经下载完成的长度
        /// </summary>
        public long LocalFileLenght
        {
            get
            {
                return this.mLocalFilelenght;
            }
        }

        public string Key
        {
            get
            {
                return this.mKey;
            }
        }

        FileStream mFileStream = null;

        #region 客户端下载文件成功事件
        public event ClientDownloadSuccess Evnet_ClientDownloadSuccess;

        public virtual void OnClientDownloadSuccess(ClientDownloadSuccessArgs e)
        {
            if (this.Evnet_ClientDownloadSuccess != null)
            {
                this.Evnet_ClientDownloadSuccess(e);
            }
        }
        #endregion

        #region 客户端下载文件错误事件
        public event ClientDownloadError Event_ClientDownloadError;

        public virtual void OnClientDownloadError(ClientDownloadErrorArgs e)
        {
            if (this.Event_ClientDownloadError != null)
            {
                this.Event_ClientDownloadError(e);
            }
        }
        #endregion

        #region 客户端下载文件不存在事件
        public event ClientDownloadNonExistent Evnet_ClientDownloadNonExistent;

        public virtual void OnClientDownloadNonExistent(ClientDownloadNonExistentArgs e)
        {
            if (this.Evnet_ClientDownloadNonExistent != null)
            {
                this.Evnet_ClientDownloadNonExistent(e);
            }
        }
        #endregion

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="iServerFilePathType">绝对路径还是相对路径</param>
        /// <param name="iServerFileName">服务器上的文件路径+文件名</param>
        /// <param name="iLocalPath">本地保存文件路径</param>
        /// <param name="iLocalFileName">本地保存文件名</param>
        /// <param name="iMasterServerIP">主服务器IP</param>
        /// <param name="iMasterPort">主服务器端口号</param>
        public ClientDownload(DownloadFilePathType iServerFilePathType, string iServerFileName
            , string iLocalPath, string iLocalFileName
            , string iMasterServerIP, int iMasterPort)
        {
            this.mLocalFilelenght = 0;
            this.mServerFilePathType = iServerFilePathType;
            this.mServerFileName = iServerFileName;
            this.mLocalPath = iLocalPath;
            this.mLocalFileName = iLocalFileName;
            this.mMasterPort = iMasterPort;
            this.mMasterServerIP = iMasterServerIP;
            this.mKey = System.Guid.NewGuid().ToString();

            try
            {
                if (!System.IO.Directory.Exists(this.mLocalPath))
                {
                    System.IO.Directory.CreateDirectory(this.mLocalPath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StartDownload(object obj)
        {
            try
            {
                this.InitSocket(this.mMasterServerIP, this.mMasterPort);
                //开始与服务器协商
                //1、告诉服务器我要下载文件,并传递要下载文件的路径
                string serverFileName = string.Empty;
                switch (this.mServerFilePathType)
                {
                    case DownloadFilePathType.RelativePath:
                        serverFileName += Convert.ToInt32(DownloadFilePathType.RelativePath).ToString();
                        serverFileName += ",";
                        break;
                    case DownloadFilePathType.AbsolutePath:
                        serverFileName += Convert.ToInt32(DownloadFilePathType.AbsolutePath).ToString();
                        serverFileName += ",";
                        break;
                    default:
                        break;
                }
                serverFileName += this.mServerFileName;
                MsgCommand tellServerDownloadFile = new MsgCommand(EMessageType.M_ClientRequestDownloadFile, serverFileName);
                this.SendMsg(tellServerDownloadFile);

                //2、等待服务器响应
                EsftMsg msg = this.ReveiceMsg();
                if (msg != null
                    && (msg is MsgCommand)
                    && msg.MsgType == EMessageType.M_Download_ServerExistFile)
                {
                    ////服务器存在请求下载的文件存在

                    ////3、客户端请求服务器下载文件的信息
                    MsgCommand requestFileInfo = new MsgCommand(EMessageType.M_Client_Download_RequestFileInfo, this.mServerFileName);
                    this.SendMsg(requestFileInfo);

                    ////4、等待服务器响应
                    msg = this.ReveiceMsg();

                    if (msg != null
                        && msg is MsgFileInfo
                        && msg.MsgType == EMessageType.M_DownLoad_SendFileInfo)
                    {
                        MsgFileInfo fileInfoMsg = (MsgFileInfo)msg;

                        this.mFileMD5 = fileInfoMsg.FileMD5;
                        this.mFileLenght = fileInfoMsg.FileLenght;

                        //long localFileLenght = 0;
                        if (this.LocalFileExist())
                        {
                            //this.mLocalFilelenght = this.GetLocalFileLenght();
                            File.Delete(this.mLocalPath + this.mLocalFileName);
                            this.mLocalFilelenght = 0;
                        }


                        //// 开始写文件进程
                        this.InitFileStream();

                        while (this.mLocalFilelenght < this.mFileLenght
                                && this.IsStop == false)
                        {
                            ////5、请求服务器开始发送文件块信息，参数commandStr为开始发送文件的路径
                            MsgCommand tellServerBeginSendFileBlock = new MsgCommand(EMessageType.M_Client_Download_RequestSendFileBlock, this.mLocalFilelenght.ToString());
                            this.SendMsg(tellServerBeginSendFileBlock);

                            msg = this.ReveiceMsg();

                            if (msg != null
                                && msg is MsgFileBlock
                                && msg.MsgType == EMessageType.M_Download_SendFileBlock)
                            {
                                MsgFileBlock fileblockMsg = (MsgFileBlock)msg;

                                if (fileblockMsg.Offset + fileblockMsg.FileBlockData.Length <= this.mFileLenght)
                                {
                                    WriteFileBlock(fileblockMsg.Offset, fileblockMsg.FileBlockData);
                                    this.mLocalFilelenght += fileblockMsg.FileBlockData.Length;
                                    MyLogManage.Debug("", "", "写文件" + fileblockMsg.Offset + ", 写长度" + fileblockMsg.FileBlockData.Length);
                                }
                            }
                            else
                            {
                                MyLogManage.Debug("", "", "接收的文件块为null");
                            }
                        }


                        this.CloseSocket();
                        this.mFileStream.Close();
                        this.mFileStream.Dispose();
                        this.mFileStream = null;

                        if (this.mLocalFilelenght == this.mFileLenght)
                        {
                            // 触发下载完成事件
                            this.OnClientDownloadSuccess(new ClientDownloadSuccessArgs(System.IO.Path.GetFullPath(this.mLocalPath + "\\" + this.mLocalFileName), this.mKey));
                        }
                    }

                }
                else if (msg != null
                        && msg is MsgCommand
                        && msg.MsgType == EMessageType.M_Download_ServerNonExistentFile)
                {
                    ////服务端告诉客户端请求下载的文件不存在

                    this.CloseSocket();

                    // 出发文件不存在事件
                    this.OnClientDownloadNonExistent(new ClientDownloadNonExistentArgs());
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error(ex.ToString());
            }
            finally
            {
                this.CloseSocket();
            }
        }

        void InitFileStream()
        {
            try
            {
                string fileFullName = System.IO.Path.GetFullPath(this.mLocalPath + "\\" + this.mLocalFileName);
                this.mFileStream = new FileStream(fileFullName, FileMode.OpenOrCreate, FileAccess.Write);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(ClientDownload)).Error(ex.Message, ex);
            }
        }

        void WriteFileBlock(long iStartIndex, byte[] iData)
        {
            if (this.mFileStream != null
                && iData != null
                && iStartIndex + iData.Length <= this.mFileLenght)
            {
                this.mFileStream.Seek(iStartIndex, SeekOrigin.Begin);
                this.mFileStream.Write(iData, 0, iData.Length);
            }
        }

        bool LocalFileExist()
        {
            if (System.IO.File.Exists(this.mLocalPath + this.mLocalFileName))
            {
                return true;
            }
            return false;
        }
    }
}
