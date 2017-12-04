package esft.android;

import java.util.Date;

public class TransferProgress {

	public String m_key;

	// / <summary>
	// / 客户端文件路�?
	// / </summary>
	public String m_ClientPath;

	// / <summary>
	// / 客户端文件名�?
	// / </summary>
	public String m_ClientFileName;

	// / <summary>
	// / 客户端要求的服务器文件路�?
	// / </summary>
	public String m_ServerPath;

	// / <summary>
	// / 客户端要求的服务器文件名�?
	// / </summary>
	public String m_ServerFileName;

	// 扩展�?
	public String m_Extension;

	// / <summary>
	// / 文件大小
	// / </summary>
	public long m_FileLenght;

	// / <summary>
	// / 当前已经完成的文件大�?
	// / </summary>
	public long m_CurrentCompleteLenght;

	// / <summary>
	// / 文件的MD5�?
	// / </summary>
	public String m_FileMd5Str;

	// / <summary>
	// / 传输状�??
	// / </summary>
	public TransferState m_TransferState;

	// / <summary>
	// / �?始传输时�?
	// / </summary>
	public Date m_StartTime;

	// / <summary>
	// / 结束传输时间
	// / </summary>
	public Date m_EndTime;

	// / <summary>
	// / �?后一个包的到达时�?
	// / </summary>
	public Date m_LastPacketTime;

	/**
	 * 错误信息
	 */
	public String ErrorInfo;
}
