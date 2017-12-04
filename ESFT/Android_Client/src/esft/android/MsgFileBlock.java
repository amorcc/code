package esft.android;

// MsgFileBlock 文件块消�?
public class MsgFileBlock extends EsftMsg {
	// / <summary>
	// / 文件块在文件中的偏移位置
	// / </summary>
	public long m_Offset;
	// / <summary>
	// / 文件块信�?
	// / </summary>
	public byte[] m_fileBlockData;

	public MsgFileBlock(int iMsgType, long iOffset, byte[] iData) {
		this.m_packetType = EPackageType.FileBlockMsg;
		this.m_msgType = iMsgType;
		this.m_Offset = iOffset;
		this.m_fileBlockData = iData;
	}
}
