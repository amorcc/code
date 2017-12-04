package esft.android;

// MsgCommand 命令型消�?
public class MsgCommand extends EsftMsg {

	// 消息内容
	public String m_command;

	public MsgCommand(int iMsgType, String iMsgCommand) {
		this.m_packetType = EPackageType.CommandMsg;
		this.m_msgType = iMsgType;
		this.m_command = iMsgCommand;
	}
}
