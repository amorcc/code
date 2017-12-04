package esft.android;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Date;

import android.content.Context;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;

import com.example.uploadfile.BaseApplication;

public class ClientUploadFile extends Thread {
	private static final int TIME_RESTART = 5000;
	// String mFileFullName;
	// String mServerPath;
	// String mServerFileName;
	ESFTParameter[] mParameters;
	String mServerIP;
	int mServerPort;
	String mMasterServerIP;
	int mMasterPort;
	boolean isStop = false;

	String mFileFullName;// 全路径
	String mServerFileName;
	String mServerPath;

	// �?次读取多少字节的文件进行传输
	protected int m_OnceReadSize = 1024 * 32;

	// 进度
	private TransferProgress mTransferProgress;

	ClientSocket mClientSocket;
	private Handler mHandler;
	private String mKey;
	private String mFileId;
	int mFileType;// 上传类型

	public ClientUploadFile(String key, String iFileFullName,
			String iServerPath, String iServerFileName, String iMasterServerIP,
			int iMasterPort, Handler handler, String iFileId) {
		this.mTransferProgress = new TransferProgress();
		mKey = key;
		mFileId = iFileId;
		mHandler = handler;
		try {
			this.mMasterServerIP = iMasterServerIP;
			this.mMasterPort = iMasterPort;
			this.mFileFullName = iFileFullName;
			this.mServerFileName = iServerFileName;
			this.mServerPath = iServerPath;

			Log.d("Upload", "上ClientUploadFile ====" + iFileFullName);
		} catch (Exception e) {
			Log.d("Upload", "上ClientUploadFile失败" + e.toString());
		}
	}

	public void stopThread() {
		this.isStop = true;
	}

	private void sendMessage(int msgType, Object msgData) {
		Message msg = new Message();
		msg.what = msgType;
		msg.obj = mKey + "|" + mFileId;
		Bundle bundle = new Bundle();
		bundle.putString("Extra", String.valueOf(msgData));
		msg.setData(bundle);
		mHandler.sendMessage(msg);
	}

	/**
	 * 等待网络连接
	 */
	private boolean waitNetwork() {
		boolean isNetwork = false;
		try {
			ConnectivityManager nw = (ConnectivityManager) BaseApplication
					.getContext()
					.getSystemService(Context.CONNECTIVITY_SERVICE);
			if (nw != null) {
				NetworkInfo netinfo = nw.getActiveNetworkInfo();
				if (netinfo != null) {
					if (netinfo.getState() == android.net.NetworkInfo.State.CONNECTED) {
						isNetwork = true;
					} else {
						isNetwork = false;
					}
				} else {
					isNetwork = false;
				}
			}
		} catch (Exception ex) {
			ex.printStackTrace();
			isNetwork = false;
		}

		if (!isNetwork) {
			if (BaseApplication.S_IsJustWifi) {
				sendMessage(HandlerMessage.MSG_WAITWIFI, "等待wifi连接");
				Log.d("Upload", "waitNetwork:等待wifi连接");
			} else {
				sendMessage(HandlerMessage.MSG_WAITNETWORK, "等待网络连接");
				Log.d("Upload", "waitNetwork:等待网络连接");
			}
		}

		boolean isUpload = isNetwork && BaseApplication.S_IsJustWifi;
		return isUpload;
	}

	@Override
	public void run() {
		try {
			String fileFullName = "";
			File file = new File(mFileFullName);
			if (file.exists()) {			

				ESFTParameter[] iParameters = new ESFTParameter[1];
				iParameters[0] = new ESFTParameter("ClientType", "android");
				this.mParameters = iParameters;
				this.mTransferProgress.m_ClientPath = file.getPath();
				this.mTransferProgress.m_ClientFileName = file.getName();
				this.mTransferProgress.m_FileLenght = file.length();
				this.mTransferProgress.m_ServerFileName = mServerFileName;
				this.mTransferProgress.m_ServerPath = mServerPath;
				this.mTransferProgress.m_Extension = CommonFunction
						.GetExtensionName(fileFullName);
				Log.d("Upload", "上ClientUploadFile::::"
						+ mTransferProgress.m_ClientPath);

				if (waitNetwork()) {
					uploadRunMethod();
				}
			}
		} catch (Exception e) {
			Log.d("Upload", "上ClientUploadFile失败" + e.toString());
		}

	}

	private void uploadRunMethod() {
		if (isStop) {
			Log.d("Upload", "结束线程！");
			return;
		}

		this.mTransferProgress.m_TransferState = TransferState.CreateMD5Hash;
		this.mTransferProgress.ErrorInfo = "";
		try {
			this.mTransferProgress.m_FileMd5Str = MD5FileUtil
					.getFileMD5String(new File(
							this.mTransferProgress.m_ClientPath));
		} catch (IOException e1) {
			Log.d("Upload", "e1 获取MD5报错" + e1.toString());
			e1.printStackTrace();
		}
		Log.d("Upload", "MD5:" + this.mTransferProgress.m_FileMd5Str);
		// 传输消息
		EsftMsg msg = null;
		// 判断上传文件是否暂停和是否上传完成
		while (!isStop
				&& this.mTransferProgress.m_CurrentCompleteLenght < this.mTransferProgress.m_FileLenght) {
			this.mTransferProgress.m_TransferState = TransferState.Connecting;
			try {
				this.mTransferProgress.ErrorInfo = "";
				if (waitNetwork()) {
					ClientGetServerInfo clientGetServerInfo = new ClientGetServerInfo(
							this.mMasterServerIP, this.mMasterPort);
					// 判断是否连接成功
					ServerIpAndPort serverIpAndPort = clientGetServerInfo
							.GetServerInfo();
					if (serverIpAndPort == null) {
						Log.d("Upload", "GetServerInfo:连接服务器失败");
						// sendMessage(HandlerMessage.MSG_SERVERFAILURE,
						// "服务器链接失败");
						try {
							Thread.sleep(1000);
						} catch (InterruptedException e) {
							// TODO 自动生成的 catch 块
							e.printStackTrace();
						}
						continue;
					}
					Log.d("Upload", "文件开始传输！");
					sendMessage(HandlerMessage.MSG_START,
							this.mTransferProgress.m_FileLenght);
					this.mServerIP = serverIpAndPort.m_ServerIP;
					this.mServerPort = serverIpAndPort.m_ServerPort;
					this.mClientSocket = new ClientSocket();
					this.mClientSocket.InitSocket(this.mServerIP,
							this.mServerPort);
					// 开始与服务器协商
					// 1、告诉服务器我要上传文件
					MsgCommand tellServerUploadFile = new MsgCommand(
							EMessageType.M_ClientRequestUploadFile, "客户端要求上传文件");
					this.mClientSocket.SendMsg(tellServerUploadFile);
					Log.d("Upload", "等待服务器响应");
					// 1.1、等待服务器响应
					msg = this.mClientSocket.ReceiveMsg();
					if (msg == null
							|| !(msg.getClass().equals(MsgCommand.class))
							|| ((MsgCommand) msg).m_msgType != EMessageType.M_ServerRequestFileInfo) {
						Log.d("Upload", "获取M_ServerRequestParameterInfo指令失败。");
						continue;
					}

					// //2、给服务器传递参数信息
					MsgParameter paraMsg = new MsgParameter(
							EMessageType.M_ClientSendParameterInfo,
							this.mParameters);
					this.mClientSocket.SendMsg(paraMsg);

					// //2.1、等待服务器响应
					msg = this.mClientSocket.ReceiveMsg();
					if (msg == null
							|| !(msg.getClass().equals(MsgCommand.class))
							|| ((MsgCommand) msg).m_msgType != EMessageType.M_ServerReceiveParametersSuccess) {
						Log.d("Upload",
								"获取M_ServerReceiveParametersSuccess指令失败。");
						continue;
					}

					// 3、给服务器传递文件信息
					MsgFileInfo fileInfoMsg = new MsgFileInfo(
							EMessageType.M_ClientSendFileInfo,
							this.mTransferProgress.m_FileLenght,
							this.mTransferProgress.m_ClientFileName,
							this.mTransferProgress.m_ClientPath,
							this.mTransferProgress.m_ServerFileName,
							this.mTransferProgress.m_ServerPath,
							this.mTransferProgress.m_Extension,
							this.mTransferProgress.m_FileMd5Str);
					this.mClientSocket.SendMsg(fileInfoMsg);
					Log.d("Upload", "给服务器传递文件:"
							+ this.mTransferProgress.m_ServerPath
							+ "-------------"
							+ this.mTransferProgress.m_ServerPath);
					// 3.1、等待服务器响应
					msg = this.mClientSocket.ReceiveMsg();
					if (msg == null
							|| !(msg.getClass().equals(MsgCommand.class))
							|| ((MsgCommand) msg).m_msgType != EMessageType.M_ServerRequestFileBlock) {
						Log.d("Upload", "获取M_ServerRequestFileBlock指令失败。");
						continue;
					}

					MsgCommand serverRequestFileBlock = (MsgCommand) msg;
					String fileLenghtStr = serverRequestFileBlock.m_command;

					if (fileLenghtStr == "" || fileLenghtStr == "0") {
						this.mTransferProgress.m_CurrentCompleteLenght = 0;
					} else if (fileLenghtStr.equals("-1")) {
						this.mTransferProgress.m_CurrentCompleteLenght = 0;

					} else {
						this.mTransferProgress.m_CurrentCompleteLenght = Long
								.parseLong(fileLenghtStr);
					}

					Log.d("Upload", "从服务器端获取的字节数" + fileLenghtStr);
					this.mTransferProgress.m_TransferState = TransferState.Transferring;
					this.mTransferProgress.ErrorInfo = "";
					sendMessage(HandlerMessage.MSG_UPLOADING,
							this.mTransferProgress.m_CurrentCompleteLenght);
					// 4、开始传递文件
					// 初始化文件读取类
					RandomAccessFile randomFile;
					boolean isError = false;
					try {
						randomFile = new RandomAccessFile(
								mTransferProgress.m_ClientPath, "r");

						Boolean ok = true;
						boolean isConnection = waitNetwork();
						while (!isStop
								&& this.mTransferProgress.m_CurrentCompleteLenght < this.mTransferProgress.m_FileLenght
								&& ok) {
							isConnection = waitNetwork();
							if (isConnection) {
								int readSize = -1;
								// 文件未读取完成
								if (this.mTransferProgress.m_CurrentCompleteLenght
										+ this.m_OnceReadSize < this.mTransferProgress.m_FileLenght) {
									// 文件可以读取_OnceReadSize的数据
									readSize = m_OnceReadSize;
								} else {
									// 文件最后不够读取_OnceReadSize的数据
									readSize = (int) (this.mTransferProgress.m_FileLenght - this.mTransferProgress.m_CurrentCompleteLenght);
								}

								if (readSize > 0) {
									byte[] data = new byte[readSize];
									randomFile
											.seek(this.mTransferProgress.m_CurrentCompleteLenght);
									randomFile.read(data);
									MsgFileBlock fileBlock = new MsgFileBlock(
											EMessageType.M_ClientSendFileData,
											this.mTransferProgress.m_CurrentCompleteLenght,
											data);
									if (fileBlock != null) {
										ok = this.mClientSocket
												.SendMsg(fileBlock);
										if (ok) {
											this.mTransferProgress.m_CurrentCompleteLenght += readSize;
										} else {
											if (this.mClientSocket != null) {
												if (randomFile != null) {
													randomFile.close();
													this.mClientSocket
															.DisposeSocket();
													this.mClientSocket = null;
													Log.d("Upload",
															"Socket释放未连接网络");
												}
											}
										}
									}

									sendMessage(
											HandlerMessage.MSG_UPLOADING,
											this.mTransferProgress.m_CurrentCompleteLenght);
								}
							} else {
								ok = false;
								if (this.mClientSocket != null) {
									if (randomFile != null) {
										randomFile.close();
										this.mClientSocket.DisposeSocket();
										this.mClientSocket = null;
										Log.d("Upload", "Socket释放未连接网络");
									}
								}
							}
						}

						if (!isConnection) {
							try {
								Thread.sleep(1000);
							} catch (InterruptedException e) {
								// TODO 自动生成的 catch 块
								e.printStackTrace();
							}
							continue;
						}
					} catch (FileNotFoundException e) {
						e.printStackTrace();
						isError = true;
					} catch (IOException e) {
						e.printStackTrace();
						isError = true;
					}

					if (isError) {
						continue;
					}
				} else {
					try {
						Thread.sleep(1000);
					} catch (InterruptedException e) {
						// TODO 自动生成的 catch 块
						e.printStackTrace();
					}
					continue;
				}

				msg = null;

				while (this.isStop == false && msg == null
						&& this.mClientSocket.m_ClientSocket.isConnected()) {
					this.mTransferProgress.m_TransferState = TransferState.VerifyingFile;
//					sendMessage(HandlerMessage.MSG_COMPLETE,
//							this.mTransferProgress.m_ClientPath);
					Log.d("Upload", "验证文件有效性");
					sendMessage(HandlerMessage.MSG_VERIFYINGFILE, "验证文件有效性");
					msg = this.mClientSocket.ReceiveMsg();
				}

				this.mTransferProgress.m_EndTime = new Date();
				if (msg != null
						&& (msg.getClass().equals(MsgCommand.class))
						&& ((MsgCommand) msg).m_msgType == EMessageType.M_ServerReceiveFileSuccess) {
					String[] paras = ((MsgCommand) msg).m_command.split(","); // md5,服务器全路径，文件全名，后缀名
					sendMessage(HandlerMessage.MSG_COMPLETE,
							this.mTransferProgress.m_ClientPath);

					try {
						Thread.sleep(100);
					} catch (InterruptedException e) {
						// TODO 自动生成的 catch 块
						e.printStackTrace();
					}

					if (paras != null && paras.length >= 3) {
						String filename = paras[2];
						 ClientTransmission.getInstance().dealUploadComplete(
						 mKey, filename, mFileId);
						Log.d("Upload", "文件上传完成！" + filename);
					} else {
						Log.d("Upload", "文件上传完成！");
						 ClientTransmission.getInstance().dealUploadComplete(
						 mKey, null, mFileId);
					}
				} else if (msg != null
						&& (msg instanceof MsgCommand)
						&& ((MsgCommand) msg).m_msgType == EMessageType.M_ServerReceiveFileFailure) {
					Log.d("Upload", "文件传输失败，服务器接收的文件MD5值与源文件MD5值不一致！");
					sendMessage(HandlerMessage.MSG_VERIFYINGERROR, "传输完成但验证错误");
				} else {
					Log.d("Upload", "文件传输失败，未能成功接受文件传输结果的指令。");
					sendMessage(HandlerMessage.MSG_VERIFYINGERROR, "传输完成但验证错误");
				}
			} catch (Exception ex) {
				Log.d("Upload", "ClientUploadFile:  上传发送错误" + ex.toString());
				sendMessage(HandlerMessage.MSG_ERROR, "上传失败");
				try {
					Thread.sleep(1000);
					continue;
				} catch (InterruptedException e) {
					// TODO 自动生成的 catch 块
					e.printStackTrace();
				}
			}
		}

		if (this.mClientSocket != null) {
			this.mClientSocket.DisposeSocket();
			this.mClientSocket = null;
			Log.d("Upload", "ClientUploadFile:  Socket上传完成释放");
		}

	}
}
