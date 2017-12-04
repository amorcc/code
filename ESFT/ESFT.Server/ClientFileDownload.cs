using ESFT.Common;
using ESFT.Common.Log;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using ESFT.Server.Download;
using ESFT.Server.FileManage;

namespace ESFT.Server
{
    public class ClientFileDownload : FileTransferBase
    {
        /// <summary>
        /// 客户端请求下载文件的路径，这里是服务器文件的真实路径
        /// </summary>
        string mClientDownloadServerFileName;

        /// <summary>
        /// 服务器读取文件类
        /// </summary>
        ServerFileReader mServerFileReader;

        public ClientFileDownload(AsyncUserToken iUserToken)
        {
            this.mUserToken = iUserToken;
            this.mTransferType = TransferType.DownloadFile;
        }

        #region 处理消息
        protected override void HandleCommandMsg(MsgCommand iMsg)
        {
            ///客户端请求下载文件
            if (iMsg.MsgType == EMessageType.M_ClientRequestDownloadFile)
            {
                HandleClientRequestDownloadFile(iMsg);
            }

            ////客户端请求下载文件的文件信息
            if (iMsg.MsgType == EMessageType.M_Client_Download_RequestFileInfo)
            {
                HandleClientRequestFileInfo(iMsg);
            }

            //// 客户端请求发送文件块信息
            if (iMsg.MsgType == EMessageType.M_Client_Download_RequestSendFileBlock)
            {
                HandleClientRequestSendFileBlock(iMsg);
            }
        }

        protected override void HandleParameterMsg(MsgParameter iMsg)
        {
        }

        protected override void HandleFileInfoMsg(MsgFileInfo iMsg)
        {
        }

        protected override void HandleFileBlockMsg(MsgFileBlock iMsg)
        {
        }
        #endregion

        /// <summary>
        /// 1、客户端请求下载文件，并传递要下载文件的路径
        /// </summary>
        /// <param name="iMsg">Command字符串包含：下载路径的类型（0：相对路径，1：绝对路径)+路径字符串，使用','隔开</param>
        void HandleClientRequestDownloadFile(MsgCommand iMsg)
        {
            string[] commandStrs = iMsg.Command.Split(',');
            if (commandStrs.Length == 2)
            {
                DownloadFilePathType filePathType = DownloadFilePathType.RelativePath;
                if (commandStrs[0] == "0")
                {
                    filePathType = DownloadFilePathType.RelativePath;
                }
                else if (commandStrs[0] == "1")
                {
                    filePathType = DownloadFilePathType.AbsolutePath;
                }

                string serverFileName = commandStrs[1];

                string realFileFullName = DownloadHandler.ServerRealFileFullName(filePathType, serverFileName);
                realFileFullName = System.IO.Path.GetFullPath(realFileFullName);

                ////查询服务器上是否存在客户端请求的文件
                if (DownloadHandler.ServerFileExist(serverFileName)
                    && realFileFullName != null
                    && realFileFullName != ""
                    && System.IO.File.Exists(realFileFullName))
                {
                    ////文件存在
                    this.mClientDownloadServerFileName = realFileFullName;
                    MsgCommand tellClientServerExistFile = new MsgCommand(EMessageType.M_Download_ServerExistFile, "服务器存在请求下载的文件");
                    this.SendMsg(tellClientServerExistFile);
                    MyLogManage.Debug(this.GetType(), "HandleCommandMsg", null, "客户端"+this.mUserToken.mClientEndPoint.ToString()+"请求下载文件，服务器存在请求下载的文件:" + realFileFullName);
                }
                else
                {
                    ////文件不存在
                    MsgCommand tellClientServerNoFile = new MsgCommand(EMessageType.M_Download_ServerNonExistentFile, "服务器不存在请求下载的文件");
                    this.SendMsg(tellClientServerNoFile);
                    MyLogManage.Debug(this.GetType(), "HandleCommandMsg", null, "客户端"+this.mUserToken.mClientEndPoint.ToString()+"请求下载文件，服务器不存在请求下载的文件:" + realFileFullName);
                }
            }
        }

        /// <summary>
        /// 3、客户端请求下载文件的文件信息
        /// </summary>
        /// <param name="iMsg"></param>
        void HandleClientRequestFileInfo(MsgCommand iMsg)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(this.mClientDownloadServerFileName);
            string fileMD5 = FileHash.GetMD5HashFromFile(this.mClientDownloadServerFileName);

            this.mFileLenght = fileInfo.Length;

            MsgFileInfo fileInfoMsg = new MsgFileInfo(
                EMessageType.M_DownLoad_SendFileInfo,
                fileInfo.Length,
                "",
                "",
                "",
                "",
                fileInfo.Extension,
                fileMD5);

            this.SendMsg(fileInfoMsg);

            this.mServerFileReader = new ServerFileReader(this.mClientDownloadServerFileName, this.mFileLenght);
        }

        /// <summary>
        /// 5、客户端请求发送文件块信息
        /// </summary>
        /// <param name="iMsg"></param>
        void HandleClientRequestSendFileBlock(MsgCommand iMsg)
        {
            long startLenght = -1;
            if (long.TryParse(iMsg.Command, out startLenght) == true)
            {
                if (this.mServerFileReader != null)
                {
                    byte[] data = this.mServerFileReader.ReadFileBlock(startLenght);

                    if (data != null)
                    {
                        MsgFileBlock fileBlockMsg = new MsgFileBlock(EMessageType.M_Download_SendFileBlock, startLenght, data);
                        this.SendMsg(fileBlockMsg);
                    }
                }
            }
        }

        public override void Dispose()
        {
            if (this.mServerFileReader != null)
            {
                this.mServerFileReader.Dispose();
                this.mServerFileReader = null;
            }
        }
    }
}
