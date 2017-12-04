using ESFT.Common;
using ESFT.Common.Log;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.IO;
using System.Threading;

namespace ESFT.Client
{
    public class ClientUploadFile : ClientSocket
    {
        /// <summary>
        /// 传输号
        /// </summary>
        public string Key { get; private set; }
        long m_FileLenght;
        string m_FileFullName;
        string m_FilePath;
        string m_FileName;
        string m_Extension;
        string m_ServerPath;
        string m_ServerFileName;
        string m_FileMD5;
        string m_ServerIp;
        int m_ServerPort;
        ESFTParameter[] m_Parameters;
        //public TransferProgress m_TransferProgress;
        public FileIO mFileIO;
        string m_MasterServerIP;
        int m_MasterPort;



        #region 客户端上传文件成功事件
        public event ClientUploadSuccess Evnet_ClientUploadSuccess;

        public virtual void OnClientUploadSuccess(ClientUploadSuccessArgs e)
        {
            if (this.Evnet_ClientUploadSuccess != null)
            {
                this.Evnet_ClientUploadSuccess(this, e);
            }
        }
        #endregion

        #region 客户端上传文件发生错误事件
        public event ClientUploadError Evnet_ClientUploadError;

        public virtual void OnClientUploadError(ClientUploadErrorArgs e)
        {
            if (this.Evnet_ClientUploadError != null)
            {
                this.Evnet_ClientUploadError(this, e);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iFileFullName">本地文件全路径</param>
        /// <param name="iServerPath">服务器相对路径</param>
        /// <param name="iServerFileName">服务器文件名</param>
        /// <param name="iMasterServerIP"></param>
        /// <param name="iMasterPort"></param>
        /// <param name="iParameters"></param>
        public ClientUploadFile(string iFileFullName
            , string iServerPath, string iServerFileName
            , string iMasterServerIP, int iMasterPort
            , ESFTParameter[] iParameters)
        {
            this.Key = Guid.NewGuid().ToString();
            if (File.Exists(iFileFullName))
            {
                FileInfo fileInfo = new FileInfo(iFileFullName);
                this.m_FileLenght = fileInfo.Length;
                this.m_FileFullName = iFileFullName;
                this.m_FilePath = fileInfo.DirectoryName + "/";
                this.m_FileName = fileInfo.Name;
                this.m_Extension = fileInfo.Extension;
                this.m_ServerPath = iServerPath;
                this.m_ServerFileName = iServerFileName;
                this.m_MasterServerIP = iMasterServerIP;
                this.m_MasterPort = iMasterPort;

                if (iParameters == null)
                {
                    this.m_Parameters = new ESFTParameter[1];
                    this.m_Parameters[0] = new ESFTParameter("ClientType", "c#");
                }

                //this.m_TransferProgress = new FileReader();
                this.mFileIO = new FileIOReader(fileInfo.DirectoryName, fileInfo.Name,
                    "/", iServerFileName, fileInfo.Length);
            }
            else
            {
                throw new Exception(this.GetType().ToString() + " : 客户端选择上传的文件\"" + iFileFullName + "\"不存在");
            }
        }

        /// <summary>
        /// 获取服务器IP和PORT
        /// </summary>
        /// <param name="iServerIP"></param>
        /// <param name="iServerPort"></param>
        public bool GetServerInfo(ref string iServerIP, ref int iServerPort)
        {
            try
            {
                this.InitSocket(this.m_MasterServerIP, this.m_MasterPort);

                //开始与服务器协商
                //1、客户端发送获取服务器信息请求
                MsgCommand tellMasterGetServerInfo = new MsgCommand(EMessageType.M_ClientRequestServerInfo, "请求分配服务器");
                this.SendMsg(tellMasterGetServerInfo);

                //2、等待服务器响应
                EsftMsg msg = this.ReveiceMsg();
                if (msg == null || !(msg is MsgServerInfo) || ((MsgServerInfo)msg).MsgType != EMessageType.M_ServerTellClientServerInfo)
                {
                    return false;
                }
                else
                {
                    //主服务器接收信息成功
                    MsgServerInfo serverInfoMsg = (MsgServerInfo)msg;
                    iServerIP = serverInfoMsg.ServerIP;
                    iServerPort = serverInfoMsg.Port;
                    return true;
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error(ex.ToString());
                return false;
            }
            finally
            {
                this.CloseSocket();
            }
        }

        public void StartSendFile(object obj)
        {
            try
            {
                //生成md5值
                this.mFileIO.State = TransferState.CreateMD5Hash;
                this.mFileIO.ErrorInfo = string.Empty;
                this.m_FileMD5 = FileHash.GetMD5HashFromFile(this.m_FileFullName);

                EsftMsg msg = null;
                while (!this.IsStop && this.mFileIO.ReceiveOrReadFileLenght < this.mFileIO.FileLenght)
                {
                    //开始连接服务器
                    this.mFileIO.State = TransferState.Connecting;
                    this.mFileIO.ErrorInfo = string.Empty;
                    bool successGetServerInfo = this.GetServerInfo(ref this.m_ServerIp, ref this.m_ServerPort);
                    log4net.LogManager.GetLogger(typeof(ClientUploadFile)).DebugFormat("获取服务器信息{0} ip:{1} port:{2}", successGetServerInfo, this.m_ServerIp, this.m_ServerPort);
                    if (!successGetServerInfo)
                    {
                        System.Threading.Thread.Sleep(1000);
                        continue;
                    }

                    log4net.LogManager.GetLogger(this.GetType()).Debug(this.m_FileFullName + "文件开始传输！");

                    this.InitSocket(this.m_ServerIp, this.m_ServerPort);

                    //开始与服务器协商
                    //1、告诉服务器我要上传文件
                    this.SendMsg(new MsgCommand(EMessageType.M_ClientRequestUploadFile, "客户端要求上传文件"));

                    //2、等待服务器响应
                    msg = this.ReveiceMsg();
                    if (msg == null || !(msg is MsgCommand) || ((MsgCommand)msg).MsgType != EMessageType.M_ServerRequestParameterInfo)
                    {
                        SetState("获取M_ServerRequestParameterInfo指令失败。");
                        continue;
                    }

                    ////3、给服务器传递参数信息
                    this.SendMsg(new MsgParameter(EMessageType.M_ClientSendParameterInfo, this.m_Parameters));

                    ////4、等待服务器响应
                    msg = this.ReveiceMsg();
                    if (msg == null || !(msg is MsgCommand) || ((MsgCommand)msg).MsgType != EMessageType.M_ServerReceiveParametersSuccess)
                    {
                        SetState("获取M_ServerReceiveParametersSuccess指令失败。");
                        continue;
                    }

                    //5、给服务器传递文件信息
                    this.SendMsg(new MsgFileInfo(EMessageType.M_ClientSendFileInfo, this.m_FileLenght, this.m_FileName, this.m_FilePath, this.m_ServerFileName, this.m_ServerPath, this.m_Extension, this.m_FileMD5));

                    //6、等待服务器响应
                    msg = this.ReveiceMsg();
                    if (msg == null || !(msg is MsgCommand) || ((MsgCommand)msg).MsgType != EMessageType.M_ServerRequestFileBlock)
                    {
                        SetState("获取M_ServerRequestFileBlock指令失败。");
                        continue;
                    }

                    MsgCommand serverRequestFileBlock = (MsgCommand)msg;

                    string fileLenghtStr = serverRequestFileBlock.Command;
                    long startLenght = 0;
                    if (long.TryParse(fileLenghtStr, out startLenght) == false)
                    {
                        startLenght = 0;
                    }

                    ////7、开始传递文件
                    ////初始化文件读取类
                    this.mFileIO.State = TransferState.Transferring;
                    this.mFileIO.ErrorInfo = string.Empty;
                    FileIOReader fileIOReader = (FileIOReader)this.mFileIO;
                    fileIOReader.SetStartIndex(startLenght);
                    try
                    {
                        fileIOReader.InitFileIOReader();
                    }
                    catch (Exception ex)
                    {
                        log4net.LogManager.GetLogger(typeof(ClientUploadFile)).Error(ex.Message, ex);
                        this.mFileIO.State = TransferState.Error;
                        this.mFileIO.ErrorInfo = ex.Message;
                        return;
                    }

                    bool ok = true;
                    while (fileIOReader.ReceiveOrReadFileLenght < fileIOReader.FileLenght
                        && IsStop == false && ok)
                    {
                        byte[] fileData = fileIOReader.ReadFileData();

                        MsgFileBlock fileBlockMsg = new MsgFileBlock(EMessageType.M_ClientSendFileData, fileIOReader.ReceiveOrReadFileLenght - fileData.Length, fileData);

                        if (fileBlockMsg != null)
                        {
                            ok = this.SendMsg(fileBlockMsg);
                            if (ok)
                                this.mFileIO.CompletedFileLenght += fileBlockMsg.FileBlockData.Length;
                        }
                    }
                }

                //8、等待服务器通知文件是否接受成功
                msg = null;
                while (this.IsStop == false && msg == null && this.IsSocketConnected())
                {
                    this.mFileIO.State = TransferState.VerifyingFile;
                    log4net.LogManager.GetLogger(typeof(ClientUploadFile)).DebugFormat("connected:{0} isstop:{1}", this.IsSocketConnected(), this.IsStop);
                    msg = this.ReveiceMsg();
                }

                log4net.LogManager.GetLogger(typeof(ClientUploadFile)).DebugFormat("connected:{0} isstop:{1}", this.IsSocketConnected(), this.IsStop);

                this.mFileIO.ReleaseFileHandler();

                if (msg != null && (msg is MsgCommand)
                    && ((MsgCommand)msg).MsgType == EMessageType.M_ServerReceiveFileSuccess)
                {
                    MsgCommand msgCommand = (MsgCommand)msg;
                    string[] commandStrs = msgCommand.Command.Split(',');
                    string serverFileMd5 = commandStrs[0];
                    this.mFileIO.RemoteFileName = System.IO.Path.GetFileName(commandStrs[1]);
                    this.mFileIO.RemoteFileName = System.IO.Path.GetFullPath(commandStrs[1]);
                    if (serverFileMd5 == this.m_FileMD5)
                    {
                        SetState(string.Empty);
                        this.OnClientUploadSuccess(new ClientUploadSuccessArgs(this.m_FileFullName, this.Key));
                    }
                    else
                    {
                        SetState("文件传输失败，本地文件MD5值与源文件MD5值不一致！");
                        this.OnClientUploadError(new ClientUploadErrorArgs(this.m_FileFullName, "文件传输失败，本地文件MD5值与源文件MD5值不一致！"));
                    }
                }
                else if (msg != null && (msg is MsgCommand)
                    && ((MsgCommand)msg).MsgType == EMessageType.M_ServerReceiveFileFailure)
                {
                    SetState("文件传输失败，服务器接收的文件MD5值与源文件MD5值不一致！");
                    this.OnClientUploadError(new ClientUploadErrorArgs(this.m_FileFullName, "文件传输失败，服务器接收的文件MD5值与源文件MD5值不一致！"));
                }
                else
                {
                    SetState("文件传输失败，未能成功接受文件传输结果的指令。");
                    this.OnClientUploadError(new ClientUploadErrorArgs(this.m_FileFullName, "文件传输失败，未能成功接受文件传输结果的指令。"));
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

        private void SetState(string s)
        {
            this.mFileIO.State = string.IsNullOrEmpty(s) ? TransferState.Finish : TransferState.Error;
            this.mFileIO.ErrorInfo = s;
            this.mFileIO.EndTime = DateTime.Now;
            log4net.LogManager.GetLogger(this.GetType()).Debug(this.m_FileFullName + (string.IsNullOrEmpty(s) ? " 文件传输成功！" : s));
        }

        public void Stop()
        {
            this.IsStop = true;
            this.mFileIO.mQueue.Clear();

            this.mFileIO.State = TransferState.ClientStop;
            this.mFileIO.EndTime = DateTime.Now;
            this.mFileIO.ReleaseFileHandler();

            this.CloseSocket();
        }
    }
}
