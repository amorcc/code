package com.upload.image;

import android.graphics.Bitmap;
import android.widget.ImageView;

import com.example.uploadfile.R;
import com.nostra13.universalimageloader.core.DisplayImageOptions;
import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.assist.ImageScaleType;
import com.nostra13.universalimageloader.core.display.RoundedBitmapDisplayer;

public class EjiangImageLoader extends ImageLoader {
	String _Url;
	ImageView _Iv;

	/**
	 * 
	 * 
	 * @param iUrl
	 * @param iv
	 */
	public EjiangImageLoader(String iUrl, ImageView iIv) {
		_Url = iUrl;
		_Iv = iIv;
	}

	/**
	 *
	 */
	public void displayImage() {
		ImageLoader.getInstance().displayImage(_Url, _Iv);
	}

	public void SetDisplayRoundImage(int iRadian) {
		DisplayImageOptions options = new DisplayImageOptions.Builder()
				.showImageOnLoading(R.drawable.image_load)
				.showImageForEmptyUri(R.drawable.image_empty)
				.showImageOnFail(R.drawable.image_error).cacheInMemory(true)
				.cacheOnDisc(true).imageScaleType(ImageScaleType.EXACTLY)
				.bitmapConfig(Bitmap.Config.RGB_565)
				.displayer(new RoundedBitmapDisplayer(iRadian)).build();
		ImageLoader.getInstance().displayImage(_Url, _Iv, options);
	}

	public void SetDiaplayCircularImage() {
		DisplayImageOptions options = new DisplayImageOptions.Builder()
				.displayer(new CircleBitmapDisplayer()).build();

		ImageLoader.getInstance().displayImage(_Url, _Iv, options);
	}

}
