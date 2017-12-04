package com.example.uploadfile;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.util.ArrayList;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.provider.MediaStore;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.BaseAdapter;
import android.widget.CheckBox;
import android.widget.ImageView;
import android.widget.RelativeLayout;

import com.upload.common.ImageUtils;
import com.upload.common.ScreenUtils;
import com.upload.image.EjiangImageConfig;
import com.upload.image.EjiangImageLoader;
import com.upload.model.PhoneImageModel;

public class PhoneImageAdapter extends BaseAdapter {
	ArrayList<PhoneImageModel> list;
	Context mContext;
	int isVideo = 0;

	public PhoneImageAdapter(Context iContext) {
		list = new ArrayList<PhoneImageModel>();
		mContext = iContext;
	}

	public void loadList(ArrayList<PhoneImageModel> iList) {
		list = iList;
	}

	@Override
	public int getCount() {
		return list.size();
	}

	@Override
	public Object getItem(int position) {
		return list.get(position);
	}

	@Override
	public long getItemId(int position) {
		return position;
	}

	@Override
	public View getView(int position, View convertView, ViewGroup parent) {
		final ViewHolder holder;
		int width = ScreenUtils.getScreenWidth(mContext) / 3;
		if (convertView == null) {
			holder = new ViewHolder();
			convertView = LayoutInflater.from(mContext).inflate(
					R.layout.phone_image_activity_item, null);
			holder.imgInfo = (ImageView) convertView
					.findViewById(R.id.phone_image_item_img);
			LayoutParams params = holder.imgInfo.getLayoutParams();
			params.height = width;
			params.width = width;
			holder.imgInfo.setLayoutParams(params);
			convertView.setTag(holder);
		} else {
			holder = (ViewHolder) convertView.getTag();
		}

		PhoneImageModel model = list.get(position);
		// Bitmap bitmap = ImageUtils.getVideoThumbnail(model.getPhotoPath()
		// .replace("file://", ""), width, width,
		// MediaStore.Images.Thumbnails.MICRO_KIND);
		// if (bitmap != null) {
		// holder.imgInfo.setImageBitmap(bitmap);
		// }

		EjiangImageLoader.getInstance().displayImage(model.getPhotoPath(),
				holder.imgInfo);

		return convertView;
	}

	class ViewHolder {
		ImageView imgInfo;
	}
}
