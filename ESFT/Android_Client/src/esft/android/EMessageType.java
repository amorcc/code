package esft.android;

public class EMessageType {
	// / <summary>
	// / 客户端请求下载文�?
	// / </summary>
	public static final int M_ClientRequestDownloadFile = 1000;
	// / <summary>
	// / 客户端请求上传文�?
	// / </summary>
	public static final int M_ClientRequestUploadFile = 1001;
	// / <summary>
	// / 客户端请求文件信�?
	// / </summary>
	public static final int M_ClientRequestFileInfo = 1002;
	// / <summary>
	// / 客户端发送文件块
	// / </summary>
	public static final int M_ClientSendFileData = 1003;
	// / <summary>
	// / 客户端发送参数信�?
	// / </summary>
	public static final int M_ClientSendParameterInfo = 1004;
	// / <summary>
	// / 客户端发送文件信�?
	// / </summary>
	public static final int M_ClientSendFileInfo = 1005;

	// / <summary>
	// / 客户端请求获取服务器信息
	// / </summary>
	public static final int M_ClientRequestServerInfo = 1006;

	// / <summary>
	// / 服务器请求文件信�?
	// / </summary>
	public static final int M_ServerRequestFileInfo = 2000;
	// / <summary>
	// / 服务器请求发送文件块信息
	// / </summary>
	public static final int M_ServerRequestFileBlock = 2001;

	// / <summary>
	// / 主服务器接收副服务器的信息成�?
	// / </summary>
	public static final int M_ReceiveViceServerInfoSuccess = 2002;

	// / <summary>
	// / 主服务器告诉客户端本次使用的服务器信息（ip和port�?
	// / </summary>
	public static final int M_ServerTellClientServerInfo = 2003;

	// / <summary>
	// / 服务器接收文件成�?
	// / </summary>
	public static final int M_ServerReceiveFileSuccess = 2004;

	// / <summary>
	// / 服务器接收文件失�?
	// / </summary>
	public static final int M_ServerReceiveFileFailure = 2005;

	// / <summary>
	// / 服务器接收参数信息成�?
	// / </summary>
	public static final int M_ServerReceiveParametersSuccess = 2006;

	// / <summary>
	// / 副服务器请求注册服务器信�?
	// / </summary>
	public static final int M_ViceServerRequestRegistration = 3007;

	// / <summary>
	// / 副服务器发�?�服务器信息
	// / </summary>
	public static final int M_ViceServerSendServerInfo = 3008;
}
