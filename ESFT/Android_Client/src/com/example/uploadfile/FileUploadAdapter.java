package com.example.uploadfile;

import java.io.FileInputStream;
import java.io.FileNotFoundException;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.provider.MediaStore;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.upload.common.ImageUtils;
import com.upload.image.EjiangImageLoader;
import com.upload.model.UploadFileModel;

import esft.android.ClientTransmission;
import esft.android.HandlerMessage;

public class FileUploadAdapter extends BaseAdapter {
	Context mContext;
	UploadFileModel mInfoModel = null;

	public FileUploadAdapter(Context iContext) {
		mContext = iContext;
	}

	public void setFileInfoModel(UploadFileModel infoModel) {
		mInfoModel = infoModel;
	}

	@Override
	public int getCount() {
		return ClientTransmission.getInstance().hashmap.size();
	}

	@Override
	public Object getItem(int position) {
		UploadFileModel model = null;
		try {
			model = ClientTransmission.getInstance().hashmap
					.get(ClientTransmission.getInstance().keysMap.get(position));
		} catch (Exception e) {

		}
		return model;
	}

	@Override
	public long getItemId(int position) {
		// TODO 自动生成的方法存根
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		// TODO 自动生成的方法存根
		ViewHolder holder;
		if (convertView == null) {
			holder = new ViewHolder();
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.upload_progress_activity_item, parent, false);

			holder.tvCount = (TextView) convertView
					.findViewById(R.id.upload_progress_activity_item_count_tv);
			holder.tvUpload = (TextView) convertView
					.findViewById(R.id.upload_progress_activity_item_upload_count_tv);
			holder.iv = (ImageView) convertView
					.findViewById(R.id.upload_progress_activity_item_img);
			holder.bar = (ProgressBar) convertView
					.findViewById(R.id.upload_progress_activity_item_bar);
			holder.tvState = (TextView) convertView
					.findViewById(R.id.upload_progress_activity_item_state_tv);
			holder.tvName = (TextView) convertView
					.findViewById(R.id.upload_progress_activity_item_name_tv);
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}

		UploadFileModel file = (UploadFileModel) ClientTransmission
				.getInstance().hashmap.values().toArray()[position];
		if (file.isVideo) {
			Bitmap bitmap = ImageUtils.getVideoThumbnail(
					file.localpath.replace("file://", ""), 80, 80,
					MediaStore.Images.Thumbnails.MICRO_KIND);
			if (bitmap != null) {
				holder.iv.setImageBitmap(bitmap);
			}
		} else {

			EjiangImageLoader.getInstance().displayImage(
					"file://" + file.localpath, holder.iv);
		}

		holder.bar.setMax((int) file.fileSeize);
		holder.tvState.setText(getAlert(file));
		holder.tvUpload.setText(file.fileCompleteSize + "");
		holder.tvCount.setText(file.fileSeize + "");
		holder.tvName.setText(file.fileName);
		return convertView;
	}

	/**
	 * 获取提示信息
	 * 
	 * @param model
	 * @return
	 */
	public String getAlert(UploadFileModel model) {
		String alert = "等待上传";
		switch (model.fileUploadStatus) {
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
			alert = " 传输中";
			break;
		case HandlerMessage.MSG_COMPLETE:
			alert = "完成";
			break;
		case HandlerMessage.MSG_SERVERFAILURE:
			alert = "找不到服务器";
			break;
		case HandlerMessage.MSG_START:
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
			model.fileCompleteSize = model.fileSeize;
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
		return alert;
	}

	public class ViewHolder {
		public TextView tvUpload;
		public TextView tvCount;
		public TextView tvState;
		public ImageView iv;
		public ProgressBar bar;
		public TextView tvName;
	}
}
