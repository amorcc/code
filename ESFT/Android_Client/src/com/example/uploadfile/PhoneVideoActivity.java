package com.example.uploadfile;

import java.util.ArrayList;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.GridView;

import com.upload.model.PhoneImageModel;

public class PhoneVideoActivity extends Activity {

	ArrayList<PhoneImageModel> phoneImageList;
	PhoneImageAdapter imageAdapter;
	GridView gvImage;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_phone_video);
		gvImage = (GridView) findViewById(R.id.phone_image_activity);
		phoneImageList = new ArrayList<PhoneImageModel>();

		imageAdapter = new PhoneImageAdapter(PhoneVideoActivity.this);
		PhoneImageDAL phoneImage = new PhoneImageDAL();
		phoneImageList = phoneImage.getPhoneImageList(PhoneVideoActivity.this);
		imageAdapter.loadList(phoneImageList);
		gvImage.setAdapter(imageAdapter);
		imageAdapter.notifyDataSetChanged();

		gvImage.setOnItemClickListener(new OnItemClickListener() {

			@Override
			public void onItemClick(AdapterView<?> parent, View view,
					int position, long id) {
				PhoneImageModel model = (PhoneImageModel) parent
						.getItemAtPosition(position);
				Intent intent = new Intent();
				intent.putExtra("Uri", model.getPhotoPath());
				setResult(RESULT_OK, intent);
				PhoneVideoActivity.this.finish();
			}
		});

	}
}
