package esft.android;

public class MsgServerInfo extends EsftMsg {

	// / <summary>
	// / 服务器IP地址
	// / </summary>
	public String m_ServerIP;

	// / <summary>
	// / 服务器端口号
	// / </summary>
	public int m_Port;

	// / <summary>
	// / 服务器网卡序列号
	// / </summary>
	public String m_ServerNetworkCordNumber;

	// / <summary>
	// / 副服务器名称
	// / </summary>
	public String m_ViceServerName;

	public MsgServerInfo(int iMsgType, String iServerIP, int iPort,
			String iServerNetworkCordNumber, String iViceServerName) {
		this.m_packetType = EPackageType.ServerInfoMsg;
		this.m_msgType = iMsgType;
		this.m_ServerIP = iServerIP;
		this.m_Port = iPort;
		this.m_ServerNetworkCordNumber = iServerNetworkCordNumber;
		this.m_ViceServerName = iViceServerName;
	}
}
