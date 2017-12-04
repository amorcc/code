package com.upload.common;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Bitmap.Config;
import android.media.ThumbnailUtils;

public class ImageUtils {
	public ImageUtils() {
		// TODO Auto-generated constructor stub
	}

	/**
	 * 将图片进行切图处理
	 * 
	 * @param scrBitmap
	 * @param limitWidth
	 * @param limitHeight
	 * @return
	 */
	public static Bitmap cutterBitmap(Bitmap srcBitmap, int limitWidth,
			int limitHeight) {
		float width = srcBitmap.getWidth();
		float height = srcBitmap.getHeight();

		float limitScale = limitWidth / limitHeight;
		float srcScale = width / height;

		if (limitScale > srcScale) {
			// 高小了，所以要去掉多余的高度
			height = limitHeight / ((float) limitWidth / width);
		} else {
			// 宽度小了，所以要去掉多余的宽度
			width = limitWidth / ((float) limitHeight / height);
		}

		Bitmap bitmap = Bitmap.createBitmap((int) width, (int) height,
				Config.ARGB_8888);
		Canvas canvas = new Canvas(bitmap);
		canvas.drawBitmap(srcBitmap, 0, 0, new Paint());
		return bitmap;
	}

	/**
	 * 获取视频的缩略图 先通过ThumbnailUtils来创建一个视频的缩略图，然后再利用ThumbnailUtils来生成指定大小的缩略图。
	 * 如果想要的缩略图的宽和高都小于MICRO_KIND，则类型要使用MICRO_KIND作为kind的值，这样会节省内存。
	 * 
	 * @param videoPath
	 *            视频的路径
	 * @param width
	 *            指定输出视频缩略图的宽度
	 * @param height
	 *            指定输出视频缩略图的高度度
	 * @param kind
	 *            参照MediaStore.Images.Thumbnails类中的常量MINI_KIND和MICRO_KIND。
	 *            其中，MINI_KIND: 512 x 384，MICRO_KIND: 96 x 96
	 * @return 指定大小的视频缩略图
	 */
	public static Bitmap getVideoThumbnail(String videoPath, int width,
			int height, int kind) {
		Bitmap bitmap = null;
		// 获取视频的缩略图
		bitmap = ThumbnailUtils.createVideoThumbnail(videoPath, kind);
		bitmap = ThumbnailUtils.extractThumbnail(bitmap, width, height,
				ThumbnailUtils.OPTIONS_RECYCLE_INPUT);
		return bitmap;
	}
}
