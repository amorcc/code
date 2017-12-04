package com.upload.common;

import com.example.uploadfile.BaseApplication;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.net.NetworkInfo.State;
import android.util.Log;

public class NetworkBroadcastReceiver extends BroadcastReceiver {
	@Override
	public void onReceive(Context context, Intent intent) {
		if (ConnectivityManager.CONNECTIVITY_ACTION.equals(intent.getAction())) {
			NetworkInfo info = intent
					.getParcelableExtra(ConnectivityManager.EXTRA_NETWORK_INFO);
			if (info != null) {
				if (info.getState() == State.CONNECTED) {
					if (info.getType() == ConnectivityManager.TYPE_WIFI) {
						BaseApplication.S_IsWifiConnection = true;
						BaseApplication.S_IsMobileConnection = false;
						Log.d("Wifi", "Wifi:连接"
								+ BaseApplication.S_IsWifiConnection
								+ BaseApplication.S_IsMobileConnection);
					} else {
						BaseApplication.S_IsWifiConnection = false;
						BaseApplication.S_IsMobileConnection = true;
						Log.d("Wifi", "3G:连接"
								+ BaseApplication.S_IsWifiConnection
								+ BaseApplication.S_IsMobileConnection);
					}
				} else {
					BaseApplication.S_IsWifiConnection = false;
					BaseApplication.S_IsMobileConnection = false;
					Log.d("Wifi", "没有网络连接" + BaseApplication.S_IsWifiConnection
							+ BaseApplication.S_IsMobileConnection);
				}
			}
		}
	}
}
