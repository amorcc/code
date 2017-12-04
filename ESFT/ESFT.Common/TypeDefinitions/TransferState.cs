
namespace ESFT.Common.TypeDefinitions
{
    /// <summary>
    /// 传输状态
    /// </summary>
    public enum TransferState
    {
        /// <summary>
        /// 正在连接
        /// </summary>
        Connecting,
        /// <summary>
        /// 正在获取参数信息
        /// </summary>
        GetParameter,
        /// <summary>
        /// 正在获取文件信息
        /// </summary>
        GetFileInfo,
        /// <summary>
        /// 正在传输
        /// </summary>
        Transferring,
        /// <summary>
        /// 正在验证传输文件有效性
        /// </summary>
        VerifyingFile,
        /// <summary>
        /// 传输完成但验证错误
        /// </summary>
        VerifyingError,
        /// <summary>
        /// 传输完成
        /// </summary>
        Finish,
        /// <summary>
        /// 生成文件MD5哈希值
        /// </summary>
        CreateMD5Hash,
        /// <summary>
        /// 客户端主动断开
        /// </summary>
        ClientDisconnectInitiative,
        /// <summary>
        /// 客户端请求暂停
        /// </summary>
        ClientPause,
        /// <summary>
        /// 客户端请求停止
        /// </summary>
        ClientStop,
        /// <summary>
        /// 服务器完成文件的接收
        /// </summary>
        ReceiveFinish,
        /// <summary>
        /// 客户端超时被回收
        /// </summary>
        ClientTimeOut,
        /// <summary>
        /// 错误
        /// </summary>
        Error
    }
}
