package com.example.uploadfile;
import java.util.ArrayList;
import java.util.Vector;

import com.upload.model.PhoneImageModel;

import android.content.Context;
import android.database.Cursor;
import android.provider.BaseColumns;
import android.provider.MediaStore;
import android.provider.MediaStore.MediaColumns;
import android.provider.MediaStore.Images.ImageColumns;
import android.provider.MediaStore.Video.VideoColumns;

public class PhoneImageDAL {
	public ArrayList<PhoneImageModel> getPhoneImageList(Context mContext) {

		ArrayList<PhoneImageModel> list = new ArrayList<PhoneImageModel>();

		String[] columns = { BaseColumns._ID, MediaColumns.DATA,
				MediaColumns.SIZE, MediaColumns.DISPLAY_NAME,
				ImageColumns.DATE_TAKEN, ImageColumns.ORIENTATION,
				ImageColumns.BUCKET_ID, ImageColumns.BUCKET_DISPLAY_NAME,
				ImageColumns.LATITUDE, ImageColumns.LONGITUDE };
		final String orderBy = ImageColumns.DATE_TAKEN;
		String selection = MediaStore.Images.Media.MIME_TYPE + "!=?";
		String[] selectionArgs = { "image/gif" };

		Cursor albumCursor = mContext.getContentResolver().query(
				MediaStore.Images.Media.EXTERNAL_CONTENT_URI, columns,
				selection, selectionArgs, orderBy + " desc");
		if (albumCursor != null) {
			for (int i = 0; i < albumCursor.getCount(); i++) {
				albumCursor.moveToPosition(i);
				PhoneImageModel model = new PhoneImageModel();
				model.setId(albumCursor.getString(0));
				model.setPhotoPath("file://" + albumCursor.getString(1));
				model.setThumbnail("file://" + albumCursor.getString(1));
				model.setName(albumCursor.getString(3));
				model.setShootDate("");
				model.setLatitude(albumCursor.getString(8));
				model.setLongitude(albumCursor.getString(9));
				model.setFileSize(albumCursor.getLong(2));
				model.setIsVideo(0);
				list.add(model);
			}
		}
		return list;
	}

	

	public Vector<PhoneImageModel> getLocalImageByAlbum(String iAlbumId,
			Context mContext) {
		Vector<PhoneImageModel> allModels = new Vector<PhoneImageModel>();

		String[] columns = { BaseColumns._ID, MediaColumns.DATA,
				MediaColumns.SIZE, MediaColumns.DISPLAY_NAME,
				ImageColumns.DATE_TAKEN, ImageColumns.ORIENTATION,
				ImageColumns.BUCKET_ID, ImageColumns.BUCKET_DISPLAY_NAME,
				ImageColumns.LATITUDE, ImageColumns.LONGITUDE };
		final String orderBy = ImageColumns.DATA;
		String selection = MediaStore.Images.Media.BUCKET_ID + "=? and "
				+ MediaStore.Images.Media.MIME_TYPE + "!=?";
		String[] selectionArgs = { iAlbumId, "image/gif" };

		Cursor imagecursor = mContext.getContentResolver().query(
				MediaStore.Images.Media.EXTERNAL_CONTENT_URI, columns,
				selection, selectionArgs, orderBy);

		for (int i = 0; i < imagecursor.getCount(); i++) {
			imagecursor.moveToPosition(i);
			PhoneImageModel model = new PhoneImageModel();
			model.setId(imagecursor.getString(0));
			model.setPhotoPath("file://" + imagecursor.getString(1));
			model.setThumbnail("file://" + imagecursor.getString(1));
			model.setName(imagecursor.getString(3));
			model.setShootDate("");
			model.setLatitude(imagecursor.getString(8));
			model.setLongitude(imagecursor.getString(9));
			model.setFileSize(imagecursor.getLong(2));
			model.setIsVideo(0);
			allModels.add(model);
		}
		return allModels;
	}

	public ArrayList<PhoneImageModel> getLocalVideoList(Context context) {

		ArrayList<PhoneImageModel> list = new ArrayList<PhoneImageModel>();
		try {
			final String[] columns = { BaseColumns._ID, MediaColumns.DATA,
					MediaColumns.SIZE, MediaColumns.DISPLAY_NAME,
					MediaColumns.DATE_ADDED, VideoColumns.DATE_TAKEN,
					VideoColumns.BUCKET_ID, VideoColumns.BUCKET_DISPLAY_NAME,
					ImageColumns.LATITUDE, ImageColumns.LONGITUDE };
			String orderBy = VideoColumns.DATE_TAKEN;
			Cursor imagecursor = context.getContentResolver().query(
					MediaStore.Video.Media.EXTERNAL_CONTENT_URI, columns, null,
					null, orderBy + " desc");
			if (imagecursor != null) {
				int count = imagecursor.getCount();
				for (int i = 0; i < count; i++) {
					imagecursor.moveToPosition(i);
					PhoneImageModel model = new PhoneImageModel();
					model.setId(imagecursor.getString(0));
					model.setPhotoPath("file://" + imagecursor.getString(1));
					model.setThumbnail("file://" + imagecursor.getString(1));
					model.setName(imagecursor.getString(3));
					model.setShootDate("");
					model.setLatitude(imagecursor.getString(8));
					model.setLongitude(imagecursor.getString(9));
					model.setIsVideo(1);
					list.add(model);
				}
			}
		} catch (Exception ex) {
			ex.printStackTrace();
		}

		return list;
	}

	public PhoneImageModel getPhoneImageModel(Context mContext,
			String iImageName) {

		PhoneImageModel model = new PhoneImageModel();

		String[] columns = { BaseColumns._ID, MediaColumns.DATA,
				MediaColumns.SIZE, MediaColumns.DISPLAY_NAME,
				ImageColumns.DATE_TAKEN, ImageColumns.ORIENTATION,
				ImageColumns.BUCKET_ID, ImageColumns.BUCKET_DISPLAY_NAME,
				ImageColumns.LATITUDE, ImageColumns.LONGITUDE };
		Cursor albumCursor = mContext.getContentResolver().query(
				MediaStore.Images.Media.EXTERNAL_CONTENT_URI, columns,
				MediaColumns.DISPLAY_NAME + "=?", new String[] { iImageName },
				null);
		if (albumCursor != null) {
			for (int i = 0; i < albumCursor.getCount(); i++) {
				albumCursor.moveToPosition(i);
				model.setId(albumCursor.getString(0));
				model.setPhotoPath("file://" + albumCursor.getString(1));
				model.setThumbnail("file://" + albumCursor.getString(1));
				model.setName(albumCursor.getString(3));
				model.setShootDate("");
				model.setLatitude(albumCursor.getString(8));
				model.setLongitude(albumCursor.getString(9));
				model.setFileSize(albumCursor.getLong(2));

			}
		}
		return model;
	}

	public PhoneImageModel getPhoneImage(Context mContext, String iData) {

		PhoneImageModel model = new PhoneImageModel();

		String[] columns = { BaseColumns._ID, MediaColumns.DATA,
				MediaColumns.SIZE, MediaColumns.DISPLAY_NAME,
				ImageColumns.DATE_TAKEN, ImageColumns.ORIENTATION,
				ImageColumns.BUCKET_ID, ImageColumns.BUCKET_DISPLAY_NAME,
				ImageColumns.LATITUDE, ImageColumns.LONGITUDE };
		Cursor albumCursor = mContext.getContentResolver().query(
				MediaStore.Images.Media.EXTERNAL_CONTENT_URI, columns,
				MediaColumns.DATA + "='" + iData + "'", null, null);
		if (albumCursor != null) {
			for (int i = 0; i < albumCursor.getCount(); i++) {
				albumCursor.moveToPosition(i);
				model.setId(albumCursor.getString(0));
				model.setPhotoPath("file://" + albumCursor.getString(1));
				model.setThumbnail("file://" + albumCursor.getString(1));
				model.setName(albumCursor.getString(3));
				model.setShootDate("");
				model.setLatitude(albumCursor.getString(8));
				model.setLongitude(albumCursor.getString(9));
				model.setFileSize(albumCursor.getLong(2));
				model.setIsSelect(true);

			}
		}
		return model;
	}
}
