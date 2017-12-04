package esft.android;

import android.R.fraction;

public class HandlerMessage {
	/**
	 * 本地文件不存在
	 */
	public final static int MSG_NOFILE = 300;
	/**
	 * 等待wifi连接
	 */
	public final static int MSG_WAITWIFI = 301;
	/**
	 * 等待网络连接
	 */
	public final static int MSG_WAITNETWORK = 302;
	/**
	 * 传输错误，请重试
	 */
	public final static int MSG_ERROR = 303;
	/**
	 * 传输中
	 */
	public final static int MSG_UPLOADING = 304;
	/**
	 * 传输完毕
	 */
	public final static int MSG_COMPLETE = 305;
	/**
	 * 找不到服务器
	 */
	public final static int MSG_SERVERFAILURE = 306;
	/**
	 * 开始上传
	 */
	public final static int MSG_START = 307;
	/**
	 * 等待传输
	 */
	public final static int MSG_WAITING = 308;

	/**
	 * 正在验证传输文件有效性
	 */
	public final static int MSG_VERIFYINGFILE = 309;
	/**
	 * 传输完成但验证错误
	 */
	public final static int MSG_VERIFYINGERROR = 310;

	/**
	 * 客户端主动断开
	 */
	public final static int MSG_CLIENTDISCONNECTINITIATIVE = 311;
	/**
	 * 客户端请求暂停
	 */
	public final static int MSG_CLIENTPAUSE = 312;

	/**
	 * 客户端请求停止
	 */
	public final static int MSG_CLIENTSTOP = 313;

	/**
	 * 服务器完成文件的接收
	 */
	public final static int MSG_RECEIVEFINISH = 314;
	/**
	 * 客户端超时被回收
	 */
	public final static int MSG_CLIENTTIMEOUT = 315;
}
