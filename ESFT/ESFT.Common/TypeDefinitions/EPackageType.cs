
namespace ESFT.Common.TypeDefinitions
{
    public enum EPackageType
    {
        /// <summary>
        /// 命令类型的数据包
        /// </summary>
        CommandMsg,
        /// <summary>
        /// 传递参数类型的数据包
        /// </summary>
        ParameterMsg,
        /// <summary>
        /// 文件信息类型的数据包
        /// </summary>
        FileInfoMsg,
        /// <summary>
        /// 文件块类型的数据包
        /// </summary>
        FileBlockMsg,
        /// <summary>
        /// 服务器信息
        /// </summary>
        ServerInfoMsg,
        /// <summary>
        /// 未知的数据包
        /// </summary>
        Unknown
    }
}
