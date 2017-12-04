package com.upload.common;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.Map;

import android.content.Context;
import android.content.SharedPreferences;
public class SPUtils {
	private static final String MAK = "41227677";

	public SPUtils() {
		/* cannot be instantiated */
		throw new UnsupportedOperationException("cannot be instantiated");
	}

	/**
	 * 保存在手机里面的文件名
	 */
	public static final String FILE_NAME = "share_data_login_info";

	/**
	 * 保存数据的方法，我们需要拿到保存数据的具体类型，然后根据类型调用不同的保存方法
	 * 
	 * @param context
	 * @param key
	 * @param object
	 */
	public static void put(Context context, String shareName, String key,
			Object object) {

		SharedPreferences sp = context.getSharedPreferences(shareName,
				Context.MODE_PRIVATE);
		SharedPreferences.Editor editor = sp.edit();
		if (object instanceof String) {
			String str = (String) object;
			String message = CryptoTools.encrypt(str);
			editor.putString(key, message);
		} else if (object instanceof Integer) {
			String str = StrUtils.int2Str((Integer) object);
			String message = CryptoTools.encrypt(str);
			editor.putString(key, message);
		} else if (object instanceof Boolean) {

			editor.putBoolean(key, (Boolean) object);
		} else if (object instanceof Float) {
			String str = StrUtils.floatToString((Float) object);
			String message = CryptoTools.encrypt(str);
			editor.putString(key, message);
		} else if (object instanceof Long) {
			String str = StrUtils.long2Str((Long) object);
			String message = CryptoTools.encrypt(str);
			editor.putString(key, message);
		} else if (object instanceof Double) {
			String str = String.valueOf(object);
			String message = CryptoTools.encrypt(str);
			editor.putString(key, message);
		} else {
			String str = String.valueOf(object);
			String message = CryptoTools.encrypt(str);
			editor.putString(key, message);
		}

		SharedPreferencesCompat.apply(editor);
	}

	/**
	 * 得到保存数据的方法，我们根据默认值得到保存的数据的具体类型，然后调用相对于的方法获取值
	 * 
	 * @param context
	 * @param key
	 * @param defaultObject
	 * @return
	 */
	public static Object get(Context context, String shareName, String key,
			Object defaultObject) {
		SharedPreferences sp = context.getSharedPreferences(shareName,
				Context.MODE_PRIVATE);

		if (defaultObject instanceof String) {

			// return sp.getString(key, (String) defaultObject);

			String str = sp.getString(key, (String) defaultObject);
			String strdDcrypt = CryptoTools.decrypt(str);
			return strdDcrypt;

		} else if (defaultObject instanceof Integer) {
			String str = sp.getString(key, String.valueOf(defaultObject));
			String strdDcrypt = CryptoTools.decrypt(str);
			return StrUtils.str2Int(strdDcrypt);
			// return sp.getInt(key, (Integer) defaultObject);
		} else if (defaultObject instanceof Boolean) {
			return sp.getBoolean(key, (Boolean) defaultObject);
		} else if (defaultObject instanceof Float) {
			String str = sp.getString(key, (String.valueOf(defaultObject)));
			String strdDcrypt = CryptoTools.decrypt(str);
			return StrUtils.stringToFloat(strdDcrypt);
			// return sp.getFloat(key, (Float) defaultObject);
		} else if (defaultObject instanceof Long) {
			String str = sp.getString(key, (String.valueOf(defaultObject)));
			String strdDcrypt = CryptoTools.decrypt(str);
			return StrUtils.str2Long(strdDcrypt);
			// return sp.getLong(key, (Long) defaultObject);
		} else if (defaultObject instanceof Double) {
			String str = sp.getString(key, (String.valueOf(defaultObject)));
			String strdDcrypt = CryptoTools.decrypt(str);
			return StrUtils.str2Long(strdDcrypt);
		}

		return null;
	}

	/**
	 * 移除某个key值已经对应的值
	 * 
	 * @param context
	 * @param key
	 */
	public static void remove(Context context, String shareName, String key) {
		SharedPreferences sp = context.getSharedPreferences(shareName,
				Context.MODE_PRIVATE);
		SharedPreferences.Editor editor = sp.edit();
		editor.remove(key);
		SharedPreferencesCompat.apply(editor);
	}

	/**
	 * 清除所有数据
	 * 
	 * @param context
	 */
	public static void clear(Context context, String shareName) {
		SharedPreferences sp = context.getSharedPreferences(shareName,
				Context.MODE_PRIVATE);
		SharedPreferences.Editor editor = sp.edit();
		editor.clear();
		SharedPreferencesCompat.apply(editor);
	}

	/**
	 * 查询某个key是否已经存在
	 * 
	 * @param context
	 * @param key
	 * @return
	 */
	public static boolean contains(Context context, String shareName, String key) {
		SharedPreferences sp = context.getSharedPreferences(shareName,
				Context.MODE_PRIVATE);
		return sp.contains(key);
	}

	/**
	 * 返回所有的键值对
	 * 
	 * @param context
	 * @return
	 */
	public static Map<String, ?> getAll(Context context, String shareName) {
		SharedPreferences sp = context.getSharedPreferences(shareName,
				Context.MODE_PRIVATE);
		return sp.getAll();
	}

	/**
	 * 创建一个解决SharedPreferencesCompat.apply方法的一个兼容类
	 * 
	 * @author zhy
	 * 
	 */
	private static class SharedPreferencesCompat {
		private static final Method sApplyMethod = findApplyMethod();

		/**
		 * 反射查找apply的方法
		 * 
		 * @return
		 */
		@SuppressWarnings({ "unchecked", "rawtypes" })
		private static Method findApplyMethod() {
			try {
				Class clz = SharedPreferences.Editor.class;
				return clz.getMethod("apply");
			} catch (NoSuchMethodException e) {
			}

			return null;
		}

		/**
		 * 如果找到则使用apply执行，否则使用commit
		 * 
		 * @param editor
		 */
		public static void apply(SharedPreferences.Editor editor) {
			try {
				if (sApplyMethod != null) {
					sApplyMethod.invoke(editor);
					return;
				}
			} catch (IllegalArgumentException e) {
			} catch (IllegalAccessException e) {
			} catch (InvocationTargetException e) {
			}
			editor.commit();
		}
	}
}
