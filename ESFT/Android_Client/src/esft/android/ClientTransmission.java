package esft.android;

import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import android.os.Handler;
import android.os.Message;
import android.util.Log;

import com.upload.model.UploadFileModel;

public class ClientTransmission {
	private static ClientTransmission instance = null;

	public static ClientTransmission getInstance() {
		if (instance == null) {
			instance = new ClientTransmission();
			Log.d("Upload", "创建ClientTransmission");
		}
		return instance;
	}

	/**
	 * 设置监听
	 * 
	 * @param listener
	 */
	public void setUploadListener(OnUploadListener listener) {
		mUploadListener = listener;
	}

	/**
	 * 停止监听
	 */
	public void clearUploadListener() {
		mUploadListener = null;
	}

	private OnUploadListener mUploadListener;

	public interface OnUploadListener {
		public void upload(UploadFileModel fileModel);
	}

	public ArrayList<String> keys = new ArrayList<String>();
	public ArrayList<String> keysMap = new ArrayList<String>();
	public HashMap<String, UploadFileModel> hashmap = new HashMap<String, UploadFileModel>();
	public HashMap<String, UploadFileModel> hashmapMood = new HashMap<String, UploadFileModel>();
	public HashMap<String, ClientUploadFile> mapUpload = new HashMap<String, ClientUploadFile>();

	boolean isFirstLoadAll = true;

	private ExecutorService executorService = Executors.newFixedThreadPool(1);

	// 向服务器上传文件
	// iFileFullName: 本地文件全路�?
	// iServerPath: 保存到服务器的相对路径：前后都有'\\',例如 �? "\\wenjianjia\\wenjian\\"
	// iServerFileName: 保存到服务器的文件名：最好使用guid+后缀�?
	// iMasterServerIP: 主服务器的ip地址
	// iMasterPort: 主服务器的端口号
	public void addTransmissionTask(UploadFileModel model, String ip) {
		if (!hashmap.containsKey(model)) {
			hashmap.put(model.fileId, model);
			hashmapMood.put(model.fileId, model);
			keysMap.add(model.fileId);
			if (hashmap.containsKey(model.fileId)) {
				isFirstLoadAll = true;
				Log.d("Upload", model.fileId + "：：：没有上传进度，上传");
				File file = new File(model.localpath.replace("file://", ""));
				if (file.exists()) {
					keys.add(model.fileId);
					ClientUploadFile clientUploadFile = new ClientUploadFile(
							model.fileId,
							model.localpath.replace("file://", ""),
							model.serverpath, model.serverName, ip, 8000,
							handler, model.fileId);
					executorService.execute(clientUploadFile);
					mapUpload.put(model.fileId, clientUploadFile);
					Log.d("Upload", model.fileId + ":::任务添加成功：："
							+ model.localpath);
				} else {
					Log.d("Upload", model.fileId + ":::本地文件不存在，删除");
				}
			} else {
				Log.d("Upload", model.fileId + "：：：已经包含，不再上传");
			}

		}
	}

	Handler handler = new Handler() {
		public void handleMessage(Message msg) {
			super.handleMessage(msg);
			try {
				String[] str = msg.obj.toString().split("[|]");
				UploadFileModel model = hashmap.get(str[0]);
				String alert = "";
				switch (msg.what) {
				case HandlerMessage.MSG_NOFILE:
					alert = " 本地文件不存在";
					break;
				case HandlerMessage.MSG_WAITWIFI:
					alert = "等待Wifi连接";
					break;
				case HandlerMessage.MSG_WAITNETWORK:
					alert = "等待网络连接";
					break;
				case HandlerMessage.MSG_ERROR:
					alert = "传输错误";
					break;
				case HandlerMessage.MSG_UPLOADING:
					model.fileCompleteSize = Long.parseLong(msg.getData()
							.getString("Extra"));
					alert = "传输中";
					break;
				case HandlerMessage.MSG_COMPLETE:
					alert = "完成";
					break;
				case HandlerMessage.MSG_SERVERFAILURE:
					alert = "找不到服务器";
					break;
				case HandlerMessage.MSG_START:
					model.fileSeize = Long.parseLong(msg.getData().getString(
							"Extra"));
					alert = "开始上传";
					break;
				case HandlerMessage.MSG_WAITING:
					alert = "等待传输";
					break;
				case HandlerMessage.MSG_VERIFYINGFILE:
					alert = "验证传输文件有效性";
					break;
				case HandlerMessage.MSG_VERIFYINGERROR:
					alert = "传输完成但验证错误";

					break;
				case HandlerMessage.MSG_CLIENTPAUSE:
					alert = "客户端请求暂停";
					break;
				case HandlerMessage.MSG_CLIENTSTOP:
					alert = " 客户端请求停止";
					break;
				case HandlerMessage.MSG_RECEIVEFINISH:
					alert = "完成";
					break;
				case HandlerMessage.MSG_CLIENTTIMEOUT:
					alert = " 客户端超时被回收";
					break;
				case HandlerMessage.MSG_CLIENTDISCONNECTINITIATIVE:
					alert = "客户端主动断开";
					break;
				default:
					break;
				}

				model.fileUploadStatus = msg.what;
				Log.d("Upload", "ClientTransmission===fileUploadStatus:"
						+ msg.what);
				if (mUploadListener != null) {

					mUploadListener.upload(model);
				}

				Log.d("Upload",
						"ClientTransmission==handler  HandlerMessage State:"
								+ alert + "  fileCompleteSize:"
								+ model.fileCompleteSize);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
	};

	/**
	 * 上传完成后的处理
	 * 
	 * @param id
	 */
	public void dealUploadComplete(final String id, String fileName,
			String iFileId) {
		if (fileName != null) {
			if (hashmap.containsKey(id)) {
				hashmap.remove(id);
				keysMap.remove(id);
			}

			if (keysMap.contains(id)) {
				keysMap.remove(id);
			}
		}
	}

	/**
	 * 停止所有传输，关闭程序
	 */
	@SuppressWarnings("deprecation")
	public void shutdownUpload() {
		if (instance != null) {

			Iterator<ClientUploadFile> iter = mapUpload.values().iterator();
			while (iter.hasNext()) {
				iter.next().stopThread();
			}

			keys.clear();
			hashmap.clear();
			hashmapMood.clear();
			mapUpload.clear();
			instance = null;
		}
	}

	/**
	 * 
	 */
	@SuppressWarnings("deprecation")
	public void shutdownUploadID(String Id) {
		if (instance != null) {
			UploadFileModel uploadDel = hashmapMood.get(Id);
			mapUpload.get(uploadDel.fileId).stopThread();
			keys.remove(uploadDel.fileId);
			mapUpload.remove(uploadDel.fileId);
			hashmapMood.remove(uploadDel.fileId);
			hashmap.remove(Id);
			keysMap.remove(Id);
		}
	}
}
