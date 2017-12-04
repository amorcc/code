package esft.android;

// MsgParameter 参数型消�?
public class MsgParameter extends EsftMsg {

	// 参数列表
	public ESFTParameter[] m_parameters;

	public MsgParameter() {
	}

	public MsgParameter(int iMsgType, ESFTParameter[] iParameters) {
		this.m_packetType = EPackageType.ParameterMsg;
		this.m_msgType = iMsgType;
		this.m_parameters = iParameters;
	}
}
