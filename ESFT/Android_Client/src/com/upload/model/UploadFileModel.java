package com.upload.model;

import esft.android.TransferState;

/**
 * @author ejiang
 * 
 */
public class UploadFileModel {
	public UploadFileModel() {
	}

	public static final int FILE_PHOTO = 0;
	public static final int FILE_VIDEO = 1;

	/**
	 * 图片ID(服务器端)
	 */
	public String fileId;

	/**
	 * 文件名称
	 */
	public String fileName;

	/**
	 * 文件大小
	 */
	public long fileSeize;

	/**
	 * 已上传文件大小
	 */
	public long fileCompleteSize;

	/**
	 * 文件上传状态
	 */
	public int fileUploadStatus;

	/**
	 * 上传服务器路径
	 */
	public String serverpath;

	/**
	 * 服务器端文件名称
	 */
	public String serverName;

	/**
	 * 本地路径
	 */
	public String localpath;

	/**
	 * 纬度
	 */
	public String latitude;

	/**
	 * 经度
	 */
	public String longitude;

	/**
	 * 拍摄时间
	 */
	public String shootDate;
	
	public boolean isVideo=false;

}
