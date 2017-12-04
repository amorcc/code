package com.upload.image;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;

import com.example.uploadfile.R;
import com.nostra13.universalimageloader.cache.disc.naming.HashCodeFileNameGenerator;
import com.nostra13.universalimageloader.cache.memory.impl.WeakMemoryCache;
import com.nostra13.universalimageloader.core.DisplayImageOptions;
import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.ImageLoaderConfiguration;
import com.nostra13.universalimageloader.core.assist.ImageScaleType;
import com.nostra13.universalimageloader.core.assist.QueueProcessingType;

public class EjiangImageConfig {
	public EjiangImageConfig(Context mContext) {
		ImageLoader.getInstance().init(setConfig(mContext, setOption()));
	}

	public DisplayImageOptions setOption() {
		DisplayImageOptions options = new DisplayImageOptions.Builder()
				.showImageOnLoading(R.drawable.image_load)
				.showImageForEmptyUri(R.drawable.image_empty)
				.showImageOnFail(R.drawable.image_error).cacheInMemory(true)
				.cacheOnDisc(true).imageScaleType(ImageScaleType.EXACTLY)
				.bitmapConfig(Bitmap.Config.RGB_565).build();

		return options;
	}

	public ImageLoaderConfiguration setConfig(Context iContext,
			DisplayImageOptions iOptions) {
		ImageLoaderConfiguration config = new ImageLoaderConfiguration.Builder(
				iContext)
				.memoryCacheExtraOptions(1024, 768)
				.memoryCache(new WeakMemoryCache())
				.discCacheExtraOptions(1024, 768, CompressFormat.JPEG, 70, null)
				.threadPoolSize(3).threadPriority(Thread.NORM_PRIORITY - 2)
				.tasksProcessingOrder(QueueProcessingType.FIFO)
				.denyCacheImageMultipleSizesInMemory()
				.discCacheFileNameGenerator(new HashCodeFileNameGenerator())
				.defaultDisplayImageOptions(iOptions).build();

		return config;
	}
}
