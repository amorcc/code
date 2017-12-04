package esft.android;

// 传输状�??
public enum TransferState {
	// / <summary>
	// / 正在连接
	// / </summary>
	Connecting,
	// / <summary>
	// / 正在获取文件信息
	// / </summary>
	GetFileInfo,
	// / <summary>
	// / 正在传输
	// / </summary>
	Transferring,
	// / <summary>
	// / 正在验证传输文件有效�?
	// / </summary>
	VerifyingFile,
	// / <summary>
	// / 传输完成
	// / </summary>
	Finish,
	// / <summary>
	// / 生成文件MD5哈希�?
	// / </summary>
	CreateMD5Hash,
	// / <summary>
	// / 客户端主动断�?
	// / </summary>
	ClientDisconnectInitiative,
	// / <summary>
	// / 客户端请求暂�?
	// / </summary>
	ClientPause,
	// / <summary>
	// / 客户端请求停�?
	// / </summary>
	ClientStop,
	// / <summary>
	// / 服务器完成文件的接收
	// / </summary>
	ServerReceiveFinish
}
