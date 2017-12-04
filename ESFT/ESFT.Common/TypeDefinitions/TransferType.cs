
namespace ESFT.Common.TypeDefinitions
{
    /// <summary>
    /// 客户端文件传输类型
    /// </summary>
    public enum TransferType
    {
        /// <summary>
        /// 未知，没有
        /// </summary>
        None,
        /// <summary>
        /// 客户端向服务器上传文件
        /// </summary>
        UploadFile,
        /// <summary>
        /// 客户端向服务器请求下载文件
        /// </summary>
        DownloadFile,
        /// <summary>
        /// 客户端请求本次连接的服务器信息
        /// </summary>
        RequestServerInfo,
        /// <summary>
        /// 请求服务端任务信息
        /// </summary>
        RequestServerTaskInfo,
        /// <summary>
        /// 子服务器注册服务器信息
        /// </summary>
        ChildServerRegistServerInfo
    }
}
