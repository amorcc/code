package esft.android;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.net.SocketAddress;
import java.net.UnknownHostException;

import android.util.Log;

public class ClientSocket {

	Socket m_ClientSocket = null;
	String mIpAddress;
	int mPort = 8000;

	public void InitSocket(String iIp, int iPort) {
		this.mIpAddress = iIp;
		this.mPort = iPort;
		try {
			m_ClientSocket = new Socket();
			SocketAddress socAddress = new InetSocketAddress(mIpAddress, mPort);		
			m_ClientSocket.connect(socAddress, 30000);
			m_ClientSocket.setSoTimeout(30000);
		} catch (UnknownHostException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		} finally {
			Log.d("Upload", "ClientSocket 建立连接失败 ");
		}

	}

	public void DisposeSocket() {
		try {
			if (this.m_ClientSocket != null) {
				this.m_ClientSocket.close();
				this.m_ClientSocket = null;
			}

			Log.d("Upload ", "ClientSocket 释放完成");
		} catch (IOException e) {
			e.printStackTrace();
			Log.d("Upload ", "ClientSocket 释放失败" + e.toString());
		}

	}

	public boolean isClosed() {
		if (this.m_ClientSocket != null) {
			return this.m_ClientSocket.isClosed();
		} else {
			return false;
		}
	}

	public boolean isConnected() {
		if (this.m_ClientSocket != null) {
			return this.m_ClientSocket.isConnected();
		} else {
			return false;
		}
	}

	public boolean isInputShutdown() {
		if (this.m_ClientSocket != null) {
			return this.m_ClientSocket.isInputShutdown();
		} else {
			return false;
		}
	}

	public boolean isOutputShutdown() {
		if (this.m_ClientSocket != null) {
			return this.m_ClientSocket.isOutputShutdown();
		} else {
			return false;
		}
	}

	public boolean SendMsg(EsftMsg iMsg) {
		try {
			
			OutputStream m_SocketOutputStream= this.m_ClientSocket.getOutputStream();	
			
			byte[] msgData = EMessage.Serialization(iMsg);
			m_SocketOutputStream.write(msgData);
			m_SocketOutputStream.flush();			
			return true;
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
			DisposeSocket();
			Log.d("Upload ", "ClientSocket 发送数据失败:" + e.toString());
			return false;
		} catch (IOException e) {
			e.printStackTrace();
			DisposeSocket();
			Log.d("Upload ", "ClientSocket 发送数据失败:" + e.toString());
			return false;
		} 

	}

	public EsftMsg ReceiveMsg() {
		try {			
			InputStream m_SocketInputStream=this.m_ClientSocket.getInputStream();
			
			EsftMsg msg = null;
			Log.d("Upload ", "ClientSocket 开始接收数据");
			byte[] packetLenghtBytes = new byte[4];
			m_SocketInputStream.read(packetLenghtBytes, 0, 4);
			int packetLenght = CommonFunction.ByteArrayToInt(packetLenghtBytes,
					0);
			byte[] msgDataBytes = new byte[packetLenght];
			m_SocketInputStream.read(msgDataBytes, 0, packetLenght);
			msg = EMessage.DeserializationPacket(msgDataBytes, 0);
			Log.d("Upload ", "ClientSocket 成功接收数据：" + msg.m_msgType);
			return msg;
		} catch (IOException e) {
			// TODO 自动生成的 catch 块
			e.printStackTrace();
			DisposeSocket();
			Log.d("Upload ", "ClientSocket 接收数据失败:" + e.toString());
			return null;
		}

	}
}
