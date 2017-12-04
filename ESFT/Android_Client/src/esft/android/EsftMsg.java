package esft.android;

//EsftMsg 消息类型的基�?
public class EsftMsg {

	// 数据包的类型
	public int m_packetType;

	// 消息类型
	public int m_msgType;

	public EsftMsg() {
		this.m_packetType = EPackageType.Unknown;
	}

	public EsftMsg(int iPacketType) {
		this.m_packetType = iPacketType;
	}
}
