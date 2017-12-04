package esft.android;

import java.io.IOException;

import android.util.Log;

public class ClientGetServerInfo extends ClientSocket {

	String m_MasterServerIP;
	int m_MasterPort;

	public ClientGetServerInfo(String iMasterServerIP, int iMasterPort) {
		this.m_MasterServerIP = iMasterServerIP;
		this.m_MasterPort = iMasterPort;
	}

	// 获取服务器IP和PORT
	public ServerIpAndPort GetServerInfo() {
		ServerIpAndPort serverIp = null;
		try {
			this.InitSocket(this.m_MasterServerIP, this.m_MasterPort);

			MsgCommand tellMasterGetServerInfo = new MsgCommand(
					EMessageType.M_ClientRequestServerInfo, "请求分配服务器");
			this.SendMsg(tellMasterGetServerInfo);

			// 2、等待服务器响应
			EsftMsg msg = this.ReceiveMsg();
			if (msg == null
					|| !(msg.getClass().equals(MsgServerInfo.class))
					|| !(msg.m_msgType == EMessageType.M_ServerTellClientServerInfo)) {
				return null;
			} else {
				MsgServerInfo serverInfoMsg = (MsgServerInfo) msg;
				serverIp = new ServerIpAndPort(serverInfoMsg.m_ServerIP,
						serverInfoMsg.m_Port);
				return serverIp;
			}

		} catch (Exception ex) {
			this.DisposeSocket();
			Log.d("Upload", "ClientGetServerInfo 分配服务器失败: " + ex.toString());
			return null;
		} finally {
			this.DisposeSocket();
		}
	}
}
