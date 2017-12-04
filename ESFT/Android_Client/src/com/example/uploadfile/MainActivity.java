package com.example.uploadfile;

import java.io.File;
import java.util.UUID;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import com.example.uploadfile.FileUploadAdapter.ViewHolder;
import com.upload.common.SPUtils;
import com.upload.model.UploadFileModel;

import esft.android.ClientTransmission;
import esft.android.ClientTransmission.OnUploadListener;
import esft.android.HandlerMessage;

public class MainActivity extends Activity {

	Button btn;
	Button btnSelect;
	Button btnVideo;
	ListView lvUpload;
	FileUploadAdapter adapter;
	TextView tv;
	EditText ip;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		btn = (Button) findViewById(R.id.upload_btn);
		btnSelect = (Button) findViewById(R.id.select_btn);
		tv = (TextView) findViewById(R.id.upload_tv);
		lvUpload = (ListView) findViewById(R.id.upload_lv);
		ip=(EditText)findViewById(R.id.upload_ip);
		
		ip.setText("192.168.0.28");
		adapter = new FileUploadAdapter(MainActivity.this);
		lvUpload.setAdapter(adapter);
		adapter.notifyDataSetChanged();
		tv.setText(SPUtils.get(MainActivity.this, "info", "Uri", "").toString());
		ClientTransmission.getInstance().setUploadListener(
				new OnUploadListener() {
					@Override
					public void upload(UploadFileModel fileModel) {
						updateView(fileModel);
					}
				});

		lvUpload.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				// TODO 自动生成的方法存根
				UploadFileModel model = (UploadFileModel) parent
						.getItemAtPosition(position);
			}
		});

		btn.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				if (tv.getText().toString().length() < 0) {
					Toast.makeText(MainActivity.this, "请选择文件",
							Toast.LENGTH_SHORT).show();
					return;
				} else {

					File file;
					file = new File(tv.getText().toString()
							.replace("file:", ""));
					if (!file.exists()) {
						Toast.makeText(MainActivity.this, "文件不存在",
								Toast.LENGTH_SHORT).show();
						return;
					}

					UploadFileModel fileModel = new UploadFileModel();
					fileModel.latitude = "";
					fileModel.localpath = file.getAbsolutePath();
					fileModel.longitude = "";
					fileModel.fileId = UUID.randomUUID().toString();
					fileModel.serverName = fileModel.fileId + ".jpg";
					fileModel.fileName = file.getName();
					fileModel.serverpath = "/ChildPlat/image/";
					fileModel.shootDate = "";
					fileModel.fileSeize = file.length();
					ClientTransmission.getInstance().addTransmissionTask(
							fileModel,ip.getText().toString());
					adapter.notifyDataSetChanged();
				}

			}
		});

		btnSelect.setOnClickListener(new OnClickListener() {

			@Override
			public void onClick(View v) {
				Intent intent = new Intent(MainActivity.this,
						PhoneVideoActivity.class);
				startActivityForResult(intent, 101);
			}
		});
	}

	// 更新指定item的数据
	private void updateView(UploadFileModel fileModel) {
		int visiblePos = lvUpload.getFirstVisiblePosition();
		int index = ClientTransmission.getInstance().keysMap
				.indexOf(fileModel.fileId);

		int offset = index - visiblePos;
		if (offset < 0) {
			return;
		} else {
			View view = lvUpload.getChildAt(offset);
			if (view != null) {
				ViewHolder holder = (ViewHolder) view.getTag();
				holder.tvState.setText(adapter.getAlert(fileModel));
				int progress = (int) (fileModel.fileCompleteSize);
				holder.bar.setProgress(progress);
				holder.tvUpload.setText(fileModel.fileCompleteSize + "");
				holder.tvCount.setText(fileModel.fileSeize + "");
				adapter.notifyDataSetChanged();
			}

		}
	}

	@Override
	protected void onActivityResult(int requestCode, int resultCode, Intent data) {
		// TODO 自动生成的方法存根
		super.onActivityResult(requestCode, resultCode, data);
		if (requestCode == 101) {
			if (resultCode == RESULT_OK) {

				if (data.getStringExtra("Uri") != null) {
					String str = data.getStringExtra("Uri");
					tv.setText(str);
					SPUtils.put(MainActivity.this, "info", "Uri", str);
				}
			}
		}
	}

	@Override
	protected void onPause() {
		super.onPause();
	}

	@Override
	public void onBackPressed() {
		super.onBackPressed();
		System.exit(0);
	}

}
