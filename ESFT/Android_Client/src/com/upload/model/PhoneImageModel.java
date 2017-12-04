package com.upload.model;

import java.io.Serializable;

public class PhoneImageModel  implements Serializable{
	/**
	 *
	 */
	private String id;
	/**
	 * 
	 */
	private String name;

	/**
	 * 
	 */
	private String photoPath;
	/**
	 * 
	 */
	private String thumbnail;

	/**
	 *
	 */
	private Boolean isSelect = false;

	/**
	 * 
	 */
	private String shootDate;

	/**
	 * 
	 */
	private String latitude;

	/**
	 * 
	 */
	private String longitude;

	/**
	 * 
	 */
	private long fileSize;

	private int isVideo;

	public PhoneImageModel() {
	}

	/**
	 * 
	 * 
	 * @return
	 */
	public String getId() {
		return id;
	}

	/**
	 * 
	 * 
	 * @param id
	 */
	public void setId(String id) {
		this.id = id;
	}

	/**
	 * 
	 * 
	 * @return
	 */
	public String getName() {
		return this.name;
	}

	/**
	 * 
	 * 
	 * @param name
	 */
	public void setName(String name) {
		this.name = name;
	}

	/**
	 * 
	 * 
	 * @return
	 */
	public String getPhotoPath() {
		return photoPath;
	}

	/**
	 * 
	 * 
	 * @param src
	 */
	public void setPhotoPath(String photoPath) {
		this.photoPath = photoPath;
	}

	/**
	 * 
	 * 
	 * @return
	 */
	public String getThumbnail() {
		return thumbnail;
	}

	/**
	 * 
	 * 
	 * @param thumbnail
	 */
	public void setThumbnail(String thumbnail) {
		this.thumbnail = thumbnail;
	}

	/**
	 * 
	 * 
	 * @return
	 */
	public Boolean getIsSelect() {
		return isSelect;
	}

	/**
	 * 
	 * 
	 * @param isSelect
	 */
	public void setIsSelect(Boolean isSelect) {
		this.isSelect = isSelect;
	}

	/**
	 * 
	 * 
	 * @return
	 */
	public String getShootDate() {
		return shootDate;
	}

	/**
	 * 
	 * 
	 * @return
	 */
	public void setShootDate(String shootDate) {
		this.shootDate = shootDate;
	}

	/**
	 *
	 */
	public String getLatitude() {
		return latitude;
	}

	/**
	 * 
	 */
	public void setLatitude(String latitude) {
		this.latitude = latitude;
	}

	/**
	 * 
	 */
	public String getLongitude() {
		return longitude;
	}

	/**
	 *
	 */
	public void setLongitude(String longitude) {
		this.longitude = longitude;
	}

	/**
	 * @return
	 */
	public long getFileSize() {
		return fileSize;
	}

	/**
	 * @param fileSize
	 */
	public void setFileSize(long fileSize) {
		this.fileSize = fileSize;
	}

	public int getIsVideo() {
		return isVideo;
	}

	public void setIsVideo(int isVideo) {
		this.isVideo = isVideo;
	}
}
