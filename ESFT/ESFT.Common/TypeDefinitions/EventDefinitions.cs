using ESFT.Message;
using System;

namespace ESFT.Common.TypeDefinitions
{
    /// <summary>
    /// 服务器写文件被手动回收
    /// </summary>
    /// <param name="e"></param>
    public delegate void ServerFileWriteManualDispose(object e);

    /// <summary>
    /// 文件完成
    /// </summary>
    /// <param name="iLocalFileFullName">本地文件路径</param>
    /// <param name="iFileMD5">该文件的MD5值</param>
    public delegate void FileComplete(string iLocalFileFullName);

    /// <summary>
    /// 文件进度改变
    /// </summary>
    /// <param name="iCompleteLenght"></param>
    public delegate void FileProgressChange(long iCompleteLenght);

    /// <summary>
    /// 文件读写操作出错
    /// </summary>
    /// <param name="iLocalFileFullName"></param>
    /// <param name="ex"></param>
    public delegate void FileError(string iLocalFileFullName, Exception ex);

    /// <summary>
    /// 新增文件传输任务
    /// </summary>
    /// <param name="iTask"></param>
    public delegate void AddTransferTask(object iTask);

    /// <summary>
    /// 文件传输任务进度发生改变
    /// </summary>
    /// <param name="iTask"></param>
    public delegate void TransferTaskProgressChange(object iTask);

    /// <summary>
    /// 文件传输任务出错
    /// </summary>
    /// <param name="ex"></param>
    public delegate void TransferTaskError(Exception ex);

    /// <summary>
    /// 文件传输任务完成
    /// </summary>
    /// <param name="iTask"></param>
    public delegate void TransferTaskFinish(object iTask);

    /// <summary>
    /// 接收到命令形式的数据包:用来通讯的，
    /// </summary>
    /// <param name="iPara"></param>
    public delegate void ReceiveCommandPacket(object iAsyncUserToken, MsgCommand iMsg);

    /// <summary>
    /// 客户端收到用来通讯的数据包
    /// </summary>
    /// <param name="iMsg"></param>
    public delegate void ClientReceiveCommandPacket(MsgCommand iMsg);

    #region 事件定义：客户端上传文件完成
    /// <summary>
    /// 客户端上传文件成功 
    /// </summary>
    /// <param name="e"></param>
    public delegate void ClientUploadSuccess(object sender, ClientUploadSuccessArgs e);

    public class ClientUploadSuccessArgs : EventArgs
    {
        public string ClientFileFullName;
        public string Key;

        public ClientUploadSuccessArgs(string iClientFileFullname, string iKey)
        {
            this.ClientFileFullName = iClientFileFullname;
            this.Key = iKey;
        }
    }
    #endregion

    #region 事件定义：客户端上传文件发生错误
    /// <summary>
    /// 客户端上传文件成功 
    /// </summary>
    /// <param name="e"></param>
    public delegate void ClientUploadError(object sender, ClientUploadErrorArgs e);

    public class ClientUploadErrorArgs : EventArgs
    {
        public string ClientFileFullName;
        public string ErrorInfo;

        public ClientUploadErrorArgs(string iClientFileFullname, string iErrorInfo)
        {
            this.ClientFileFullName = iClientFileFullname;
            this.ErrorInfo = iErrorInfo;
        }
    }
    #endregion

    #region 事件定义：客户端下载文件完成
    /// <summary>
    /// 客户端上传文件成功 
    /// </summary>
    /// <param name="e"></param>
    public delegate void ClientDownloadSuccess(ClientDownloadSuccessArgs e);

    public class ClientDownloadSuccessArgs : EventArgs
    {
        public string ClientFileFullName;
        public string Key;

        public ClientDownloadSuccessArgs(string iClientFileFullname, string iKey)
        {
            this.ClientFileFullName = iClientFileFullname;
            this.Key = iKey;
        }
    }
    #endregion

    #region 事件定义：客户端下载文件不存在
    /// <summary>
    /// 客户端上传文件成功 
    /// </summary>
    /// <param name="e"></param>
    public delegate void ClientDownloadNonExistent(ClientDownloadNonExistentArgs e);

    public class ClientDownloadNonExistentArgs : EventArgs
    {
        public ClientDownloadNonExistentArgs()
        {
        }
    }
    #endregion

    #region 事件定义：客户端下载文件错误

    public delegate void ClientDownloadError(ClientDownloadErrorArgs e);

    public class ClientDownloadErrorArgs : EventArgs
    {
        public string ErrorInfo;

        public ClientDownloadErrorArgs(string iErrorInfo)
        {
            this.ErrorInfo = iErrorInfo;
        }
    }

    #endregion
}
