package com.upload.common;

import java.io.UnsupportedEncodingException;
import java.security.Key;

import javax.crypto.Cipher;
import javax.crypto.SecretKeyFactory;
import javax.crypto.spec.DESedeKeySpec;
import javax.crypto.spec.IvParameterSpec;

import android.util.Base64;
import android.util.Log;

public class CryptoTools {
	private static byte[] key = Base64.decode(
			"YWJjZGVmZ2hpamtsbW5vcHFyc3R1dnd4".getBytes(), Base64.DEFAULT);
	private static byte[] keyiv = { 1, 2, 3, 4, 5, 6, 7, 8 };

	/**
	 * 加密
	 * 
	 * @param strEncrypt
	 * @return
	 */
	public static String encrypt(String strEncrypt) {

		try {
			byte[] temp;
			temp = CryptoTools.des3EncodeECB(strEncrypt.getBytes("UTF-8"));
			String message = new String(Base64.encode(temp, Base64.DEFAULT),
					"UTF-8");
			Log.d("encrypt", message);
			return message;

		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		return "";
	}

	/**
	 * 解密
	 * 
	 * @param strDecrypt
	 * @return
	 */
	public static String decrypt(String strDecrypt) {
		try {
			byte[] tmpt;
			tmpt = Base64.decode(strDecrypt.getBytes("UTF-8"), Base64.DEFAULT);
			tmpt = CryptoTools.ees3DecodeECB(tmpt);
			strDecrypt = new String(tmpt, "UTF-8");
			return strDecrypt;
		} catch (UnsupportedEncodingException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return "";
	}

	/**
	 * ECB加密,不要IV
	 * 
	 * @param key
	 *            密钥
	 * @param data
	 *            明文
	 * @return Base64编码的密文
	 * @throws Exception
	 */
	public static byte[] des3EncodeECB(byte[] data) throws Exception {
		Key deskey = null;
		DESedeKeySpec spec = new DESedeKeySpec(key);
		SecretKeyFactory keyfactory = SecretKeyFactory.getInstance("desede");
		deskey = keyfactory.generateSecret(spec);
		Cipher cipher = Cipher.getInstance("desede" + "/ECB/PKCS5Padding");
		cipher.init(Cipher.ENCRYPT_MODE, deskey);
		byte[] bOut = cipher.doFinal(data);
		return bOut;
	}

	/**
	 * ECB解密,不要IV
	 * 
	 * @param key
	 *            密钥
	 * @param data
	 *            Base64编码的密文
	 * @return 明文
	 * @throws Exception
	 */
	public static byte[] ees3DecodeECB(byte[] data) throws Exception {
		Key deskey = null;
		DESedeKeySpec spec = new DESedeKeySpec(key);
		SecretKeyFactory keyfactory = SecretKeyFactory.getInstance("desede");
		deskey = keyfactory.generateSecret(spec);
		Cipher cipher = Cipher.getInstance("desede" + "/ECB/PKCS5Padding");
		Log.i("des", "decode init before");
		cipher.init(Cipher.DECRYPT_MODE, deskey);
		Log.i("des", "decode init after" + new String(data, "UTF-8"));
		byte[] bOut = cipher.doFinal(data);
		Log.i("des", "decode doFinal after");
		return bOut;

	}

	/**
	 * CBC加密
	 * 
	 * @param key
	 *            密钥
	 * @param keyiv
	 *            IV
	 * @param data
	 *            明文
	 * @return Base64编码的密文
	 * @throws Exception
	 */
	public static byte[] des3EncodeCBC(byte[] data) throws Exception {
		Key deskey = null;
		DESedeKeySpec spec = new DESedeKeySpec(key);
		SecretKeyFactory keyfactory = SecretKeyFactory.getInstance("desede");
		deskey = keyfactory.generateSecret(spec);
		Cipher cipher = Cipher.getInstance("desede" + "/CBC/PKCS5Padding");
		IvParameterSpec ips = new IvParameterSpec(keyiv);
		cipher.init(Cipher.ENCRYPT_MODE, deskey, ips);
		byte[] bOut = cipher.doFinal(data);
		return bOut;
	}

	/**
	 * CBC解密
	 * 
	 * @param key
	 *            密钥
	 * @param keyiv
	 *            IV
	 * @param data
	 *            Base64编码的密文
	 * @return 明文
	 * @throws Exception
	 */
	public static byte[] des3DecodeCBC(byte[] data) throws Exception {

		Key deskey = null;
		DESedeKeySpec spec = new DESedeKeySpec(key);
		SecretKeyFactory keyfactory = SecretKeyFactory.getInstance("desede");
		deskey = keyfactory.generateSecret(spec);
		Cipher cipher = Cipher.getInstance("desede" + "/CBC/PKCS5Padding");
		IvParameterSpec ips = new IvParameterSpec(keyiv);
		cipher.init(Cipher.DECRYPT_MODE, deskey, ips);
		byte[] bOut = cipher.doFinal(data);
		return bOut;

	}
}
