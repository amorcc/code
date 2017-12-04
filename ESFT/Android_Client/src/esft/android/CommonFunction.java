package esft.android;

import java.io.UnsupportedEncodingException;
import java.text.DecimalFormat;

public class CommonFunction {
	public static String FormatFileLenghtToStr(long iFileLenght) {
		String lenghtStr = "";
		DecimalFormat df = new DecimalFormat("#.00");
		if (iFileLenght > 1024 * 1024) {
			// M
			double lenght = ((double) iFileLenght) / (1024 * 1024);
			lenghtStr = df.format(lenght) + "M";
		} else if (iFileLenght > 1024 * 1024 * 1024) {
			// G
			double lenght = ((double) iFileLenght) / (1024 * 1024 * 1024);
			lenghtStr = df.format(lenght) + "G";
		} else if (iFileLenght < 1024 * 1024) {
			// K
			double lenght = ((double) iFileLenght) / (1024);
			lenghtStr = df.format(lenght) + "K";
		}

		return lenghtStr;
	}

	public static String FormatFileLenghtToStr(double iFileLenght) {
		String lenghtStr = "";
		DecimalFormat df = new DecimalFormat("#.00");
		if (iFileLenght > 1024 * 1024) {
			// M
			double lenght = ((double) iFileLenght) / (1024 * 1024);
			lenghtStr = df.format(lenght) + "M";
		} else if (iFileLenght > 1024 * 1024 * 1024) {
			// G
			double lenght = ((double) iFileLenght) / (1024 * 1024 * 1024);
			lenghtStr = df.format(lenght) + "G";
		} else if (iFileLenght < 1024 * 1024) {
			// K
			double lenght = ((double) iFileLenght) / (1024);
			lenghtStr = df.format(lenght) + "K";
		}

		return lenghtStr;
	}

	public static byte[] IntToByte(int iValue) {

		byte[] ret = new byte[4];
		ret[0] = (byte) (iValue & 0xFF);
		ret[1] = (byte) ((iValue >> 8) & 0xFF);
		ret[2] = (byte) ((iValue >> 16) & 0xFF);
		ret[3] = (byte) ((iValue >> 24) & 0xFF);
		return ret;

	}

	public static byte[] LongToByte(long number) {
		byte[] bb = new byte[8];
		bb[0] = (byte) (number >> 0);
		bb[1] = (byte) (number >> 8);
		bb[2] = (byte) (number >> 16);
		bb[3] = (byte) (number >> 24);
		bb[4] = (byte) (number >> 32);
		bb[5] = (byte) (number >> 40);
		bb[6] = (byte) (number >> 48);
		bb[7] = (byte) (number >> 56);

		return bb;
	}

	public static long ByteToLong(byte[] bb, int iStartIndex) {

		return ((((long) bb[iStartIndex + 0] & 0xff) << 0)
				| (((long) bb[iStartIndex + 1] & 0xff) << 8)
				| (((long) bb[iStartIndex + 2] & 0xff) << 16)
				| (((long) bb[iStartIndex + 3] & 0xff) << 24)
				| (((long) bb[iStartIndex + 4] & 0xff) << 32)
				| (((long) bb[iStartIndex + 5] & 0xff) << 40)
				| (((long) bb[iStartIndex + 6] & 0xff) << 48) | (((long) bb[iStartIndex + 7] & 0xff) << 56));
	}

	public static int ByteArrayToInt(byte[] intByte, int offset) {

		int fromByte = 0;
		for (int i = 0; i < 2; i++) {
			int n = (intByte[i + offset] < 0 ? (int) intByte[i + offset] + 256
					: (int) intByte[i + offset]) << (8 * i);
			// System.out.println(n);
			fromByte += n;
		}
		return fromByte;
	}

	public static String ByteArrayToString(byte[] iData, int iOffset,
			int iStrLenght) throws UnsupportedEncodingException {
		byte[] strBytes = new byte[iStrLenght];
		System.arraycopy(iData, iOffset, strBytes, 0, iStrLenght);

		return new String(strBytes, "UTF8");
	}

	public static byte[] intToByteArray(int a) {
		byte[] ret = new byte[4];
		ret[0] = (byte) (a & 0xFF);
		ret[1] = (byte) ((a >> 8) & 0xFF);
		ret[2] = (byte) ((a >> 16) & 0xFF);
		ret[3] = (byte) ((a >> 24) & 0xFF);
		return ret;
	}

	public static String GetExtensionName(String fileName) {

		if (fileName != null && fileName.length() > 0) {

			int dot = fileName.lastIndexOf('.');
			if (dot > -1 && dot < fileName.length() - 1) {

				return "." + fileName.substring(dot + 1);
			}
		}

		return "";
	}
}
