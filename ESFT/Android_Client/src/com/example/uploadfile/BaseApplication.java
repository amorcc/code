package com.example.uploadfile;

import com.upload.common.NetworkBroadcastReceiver;
import com.upload.image.EjiangImageConfig;

import android.app.Application;
import android.content.Context;
import android.content.IntentFilter;
import android.net.ConnectivityManager;

public class BaseApplication extends Application {
	public static Boolean S_IsWifiConnection = false;
	public static Boolean S_IsMobileConnection = false;
	public static Boolean S_IsJustWifi = true;
	private static Context currentContext;
	
	@Override
	public void onCreate() {
		super.onCreate();
		currentContext=getApplicationContext();
		IntentFilter filter = new IntentFilter(
				ConnectivityManager.CONNECTIVITY_ACTION);
		NetworkBroadcastReceiver mConnectionReceiver = new NetworkBroadcastReceiver();
		this.registerReceiver(mConnectionReceiver, filter);
		new EjiangImageConfig(getApplicationContext());
	}
	
	public static Context getContext() {
		return currentContext;
	}

}
