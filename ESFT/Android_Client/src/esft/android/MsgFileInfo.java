package esft.android;

// MsgFileInfo 文件信息消息
public class MsgFileInfo extends EsftMsg {

	// 文件大小
	public long m_FileLenght;

	// 客户端文件名
	public String m_ClientFileName;

	// 客户端路�?
	public String m_ClietnDirectoryName;

	// 服务器文件名
	public String m_ServerFileName;

	// 服务器路�?
	public String m_ServerDirectoryName;

	// 文件扩展�?
	public String m_Extension;

	// 文件MD5�?
	public String m_FileMD5;

	public MsgFileInfo(int iMsgType, long iFileLenght, String iClientFileName,
			String iClietnDirectoryName, String iServerFileName,
			String iServerDirectoryName, String iExtension, String iFileMD5) {
		this.m_packetType = EPackageType.FileInfoMsg;
		this.m_msgType = iMsgType;
		this.m_FileLenght = iFileLenght;
		this.m_ClientFileName = iClientFileName;
		this.m_ClietnDirectoryName = iClietnDirectoryName;
		this.m_ServerFileName = iServerFileName;
		this.m_ServerDirectoryName = iServerDirectoryName;
		this.m_Extension = iExtension;
		this.m_FileMD5 = iFileMD5;
	}
}
