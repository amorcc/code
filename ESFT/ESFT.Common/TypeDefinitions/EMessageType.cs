
namespace ESFT.Common.TypeDefinitions
{
    public class EMessageType
    {
        public const int M_Test = 1;
        /// <summary>
        /// 客户端请求下载文件
        /// </summary>
        public const int M_ClientRequestDownloadFile = 1000;
        /// <summary>
        /// 客户端请求上传文件
        /// </summary>
        public const int M_ClientRequestUploadFile = 1001;
        /// <summary>
        /// 客户端请求文件信息
        /// </summary>
        public const int M_ClientRequestFileInfo = 1002;
        /// <summary>
        /// 客户端发送文件块
        /// </summary>
        public const int M_ClientSendFileData = 1003;
        /// <summary>
        /// 客户端发送参数信息
        /// </summary>
        public const int M_ClientSendParameterInfo = 1004;
        /// <summary>
        /// 客户端发送文件信息
        /// </summary>
        public const int M_ClientSendFileInfo = 1005;

        /// <summary>
        /// 客户端请求获取服务器信息
        /// </summary>
        public const int M_ClientRequestServerInfo = 1006;

        /// <summary>
        /// 客户端向服务端请求下载文件的文件信息
        /// </summary>
        public const int M_Client_Download_RequestFileInfo = 1007;

        /// <summary>
        /// 客户端下载文件：请求服务器发送文件块信息
        /// </summary>
        public const int M_Client_Download_RequestSendFileBlock = 1008;

        /// <summary>
        /// 客户端发送一个命令形式的数据包
        /// </summary>
        public const int M_Client_SendCommandInfo = 10000;

        /// <summary>
        /// 服务器请求文件信息
        /// </summary>
        public const int M_ServerRequestParameterInfo = 2000;
        /// <summary>
        /// 服务器请求发送文件块信息
        /// </summary>
        public const int M_ServerRequestFileBlock = 2001;
        /// <summary>
        /// 服务器请求继续发送文件块信息
        /// </summary>
        public const int M_ServerRequestContinueSendFileBlock = 2002;



        /// <summary>
        /// 主服务器告诉客户端本次使用的服务器信息（ip和port）
        /// </summary>
        public const int M_ServerTellClientServerInfo = 2003;

        /// <summary>
        /// 服务器接收文件成功
        /// </summary>
        public const int M_ServerReceiveFileSuccess = 2004;

        /// <summary>
        /// 服务器接收文件失败
        /// </summary>
        public const int M_ServerReceiveFileFailure = 2005;

        /// <summary>
        /// 服务器接收参数信息成功
        /// </summary>
        public const int M_ServerReceiveParametersSuccess = 2006;

        /// <summary>
        /// 服务端告诉客户端请求下载的文件不存在
        /// </summary>
        public const int M_Download_ServerNonExistentFile = 2007;

        /// <summary>
        /// 服务端告诉客户端请求下载的文件存在
        /// </summary>
        public const int M_Download_ServerExistFile = 2008;

        /// <summary>
        /// 服务端告诉客户端开始发送文件
        /// </summary>
        public const int M_DownLoad_BeginSend = 2009;

        /// <summary>
        /// 服务端告诉客户端文件的信息
        /// </summary>
        public const int M_DownLoad_SendFileInfo = 2010;

        /// <summary>
        /// 服务端传递文件块信息给客户端
        /// </summary>
        public const int M_Download_SendFileBlock = 2011;


        /// <summary>
        /// 主服务器接收副服务器的信息成功
        /// </summary>
        public const int M_ReceiveViceServerInfoSuccess = 3002;

        /// <summary>
        /// 副服务器请求注册服务器信息
        /// </summary>
        public const int M_ViceServerRequestRegistration = 3007;

        /// <summary>
        /// 副服务器发送服务器信息
        /// </summary>
        public const int M_ViceServerSendServerInfo = 3008;

        /// <summary>
        /// 客户端请求获取服务器可用连接数等基础信息
        /// </summary>
        public const int C_GetServerBaseInfo = 6001;

        /// <summary>
        /// 客户端请求获取服务器正在传输的信息
        /// </summary>
        public const int C_GetServerTransferInfo = 6002;

        /// <summary>
        /// 服务器给客户端发送服务器可用连接数等基础信息
        /// </summary>
        public const int M_SendServerBaseInfo = 6101;

        /// <summary>
        /// 服务器给客户端发送正在传输的任务信息
        /// </summary>
        public const int M_SendServerTransferInfo = 6102;

        /// <summary>
        /// 服务器发送本机IP地址给主服务器
        /// </summary>
        public const int M_SendServerIPToHostServer = 6201;

        /// <summary>
        /// 服务UI请求子服务器信息
        /// </summary>
        public const int M_ServiceRequestChildInfo = 6202;


        /// <summary>
        /// 服务器发送一个命令形式的数据包
        /// </summary>
        public const int M_Server_SendCommandInfo = 20000;
    }
}
