package esft.android;

import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.List;

public class EMessage {

	// -------------------------------------------------------------
	// 序列化消�?
	public static byte[] Serialization(EsftMsg iMsg)
			throws UnsupportedEncodingException {
		byte[] data = null;

		if (iMsg.getClass().equals(MsgCommand.class)
				&& iMsg.m_packetType == EPackageType.CommandMsg) {
			MsgCommand commandMsg = (MsgCommand) iMsg;

			data = SerializationCommandMsg(commandMsg.m_msgType,
					commandMsg.m_command);
		} else if (iMsg.getClass().equals(MsgParameter.class)
				&& iMsg.m_packetType == EPackageType.ParameterMsg) {
			MsgParameter paraMsg = (MsgParameter) iMsg;

			data = SerializationParameterMsg((int) paraMsg.m_packetType,
					paraMsg.m_msgType, paraMsg.m_parameters);
		} else if (iMsg.getClass().equals(MsgFileInfo.class)
				&& iMsg.m_packetType == EPackageType.FileInfoMsg) {
			MsgFileInfo fileInfoMsg = (MsgFileInfo) iMsg;

			data = SerializationFileInfoMsg(fileInfoMsg.m_msgType,
					fileInfoMsg.m_FileLenght, fileInfoMsg.m_ClientFileName,
					fileInfoMsg.m_ClietnDirectoryName,
					fileInfoMsg.m_ServerFileName,
					fileInfoMsg.m_ServerDirectoryName, fileInfoMsg.m_Extension,
					fileInfoMsg.m_FileMD5);
		} else if (iMsg.getClass().equals(MsgFileBlock.class)
				&& iMsg.m_packetType == EPackageType.FileBlockMsg) {
			MsgFileBlock fileBlockMsg = (MsgFileBlock) iMsg;

			data = SerializationFileBlockMsg(fileBlockMsg.m_msgType,
					fileBlockMsg.m_Offset, fileBlockMsg.m_fileBlockData);
		} else {
			data = null;
		}

		return data;
	}

	// -------------------------------------------------------------
	// 反序列化消息
	public static EsftMsg DeserializationPacket(byte[] iData, int iStartIndex)
			throws UnsupportedEncodingException {
		EsftMsg msg = null;

		int packetType = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		// byte[] data = new byte[iData.Length - 4];
		// Buffer.BlockCopy(iData, 4, data, 0, data.Length);
		iStartIndex += 4;

		switch (packetType) {
		case (int) EPackageType.CommandMsg:
			msg = DeserializationCommand(iData, iStartIndex);
			break;
		case (int) EPackageType.ParameterMsg:
			msg = DeserializationParameter(iData, iStartIndex);
			break;
		case (int) EPackageType.FileInfoMsg:
			msg = DeserializationFileInfo(iData, iStartIndex);
			break;
		case (int) EPackageType.FileBlockMsg:
			msg = DeserializationFileBlock(iData, iStartIndex);
			break;
		case (int) EPackageType.ServerInfoMsg:
			msg = DeserializationServerInfo(iData, iStartIndex);
			break;
		}

		return msg;
	}

	// -------------------------------------------------------------
	// 序列化命令型消息
	public static byte[] SerializationCommandMsg(int iMsgType, String iCommand)
			throws UnsupportedEncodingException {
		byte[] data = null;

		// packetType
		byte[] packetTypeBytes = CommonFunction
				.IntToByte(EPackageType.CommandMsg);

		// msgType
		byte[] messageTypeByte = CommonFunction.IntToByte(iMsgType);

		// command内容
		byte[] commandByte = iCommand.getBytes("UTF8");
		// command byte[] 长度
		byte[] commandMsgLenght = CommonFunction.IntToByte(commandByte.length);

		int dataLenght = packetTypeBytes.length + messageTypeByte.length
				+ commandMsgLenght.length + commandByte.length;

		data = new byte[dataLenght + 4];

		byte[] dataLenghtBytes = CommonFunction.IntToByte(dataLenght);

		int index = 0;

		System.arraycopy(dataLenghtBytes, 0, data, index,
				dataLenghtBytes.length);
		index += dataLenghtBytes.length;

		System.arraycopy(packetTypeBytes, 0, data, index,
				packetTypeBytes.length);
		index += dataLenghtBytes.length;

		System.arraycopy(messageTypeByte, 0, data, index,
				messageTypeByte.length);
		index += dataLenghtBytes.length;

		System.arraycopy(commandMsgLenght, 0, data, index,
				commandMsgLenght.length);
		index += dataLenghtBytes.length;

		System.arraycopy(commandByte, 0, data, index, commandByte.length);

		index += dataLenghtBytes.length;

		return data;
	}

	// -------------------------------------------------------------
	// 序列化参数类型的消息
	public static byte[] SerializationParameterMsg(int iPacketType,
			int iMsgType, ESFTParameter[] iParameters)
			throws UnsupportedEncodingException {
		byte[] data = null;

		int dataBitNum = 0;
		// List<byte[]> dataList = new List<byte[]>();
		List<byte[]> dataList = new ArrayList<byte[]>();
		//
		// 记录packetType
		// byte[] packetTypeByte = BitConverter.GetBytes(iPacketType);
		byte[] packetTypeByte = CommonFunction.IntToByte(iPacketType);
		// dataList.Add(packetTypeByte);
		dataList.add(packetTypeByte);
		// dataBitNum += packetTypeByte.Length;
		dataBitNum += packetTypeByte.length;
		//
		// 记录msgType，消息类�?
		// byte[] msgTypeBytes = BitConverter.GetBytes(iMsgType);
		byte[] msgTypeBytes = CommonFunction.IntToByte(iMsgType);
		// dataList.Add(msgTypeBytes);
		dataList.add(msgTypeBytes);
		// dataBitNum += msgTypeBytes.Length;
		dataBitNum += msgTypeBytes.length;
		//
		// 记录参数的个�?
		// byte[] parameterNum = BitConverter.GetBytes(iParameters.Length);
		byte[] parameterNum = CommonFunction.IntToByte(iParameters.length);
		// dataList.Add(parameterNum);
		dataList.add(parameterNum);
		// dataBitNum += parameterNum.Length;
		dataBitNum += parameterNum.length;
		//
		// for (int i = 0; i < iParameters.Length; i++)
		// {
		for (int i = 0; i < iParameters.length; i++) {
			// string paraName = iParameters[i].ParaName;
			// string paraContent = iParameters[i].ParaContent;
			//
			String paraName = iParameters[i].m_ParameterName;
			String paraContent = iParameters[i].m_ParaContent;
			// byte[] paraNameBytes = m_utf8Encoding.GetBytes(paraName);
			// byte[] paraContentBytes = m_utf8Encoding.GetBytes(paraContent);
			byte[] paraNameBytes = paraName.getBytes("UTF8");
			byte[] paraContentBytes = paraContent.getBytes("UTF8");
			//
			// byte[] nameLenghtBytes =
			// BitConverter.GetBytes(paraNameBytes.Length);
			// dataList.Add(nameLenghtBytes);
			// dataBitNum += nameLenghtBytes.Length;
			byte[] nameLenghtBytes = CommonFunction
					.IntToByte(paraNameBytes.length);
			dataList.add(nameLenghtBytes);
			dataBitNum += nameLenghtBytes.length;
			//
			// dataList.Add(paraNameBytes);
			// dataBitNum += paraNameBytes.Length;
			dataList.add(paraNameBytes);
			dataBitNum += paraNameBytes.length;
			//
			// byte[] contentLenghtBytes =
			// BitConverter.GetBytes(paraContentBytes.Length);
			// dataList.Add(contentLenghtBytes);
			// dataBitNum += contentLenghtBytes.Length;
			byte[] contentLenghtBytes = CommonFunction
					.IntToByte(paraContentBytes.length);
			dataList.add(contentLenghtBytes);
			dataBitNum += contentLenghtBytes.length;
			//
			// dataList.Add(paraContentBytes);
			// dataBitNum += paraContentBytes.Length;
			dataList.add(paraContentBytes);
			dataBitNum += paraContentBytes.length;
			// }
		}
		//
		// int dataLenght = dataBitNum;
		int dataLenght = dataBitNum;
		//
		// data = new byte[dataLenght + 4];
		data = new byte[dataLenght + 4];
		//
		// byte[] dataLenghtBytes = BitConverter.GetBytes(dataLenght);
		byte[] dataLenghtBytes = CommonFunction.IntToByte(dataLenght);
		//
		// dataLenghtBytes.CopyTo(data, 0);
		int index = 0;
		System.arraycopy(dataLenghtBytes, 0, data, index,
				dataLenghtBytes.length);
		index += 4;
		//
		// int index = 4;
		//
		// for (int i = 0; i < dataList.Count; i++)
		// {
		// dataList[i].CopyTo(data, index);
		// index += dataList[i].Length;
		// }
		for (int i = 0; i < dataList.size(); i++) {
			System.arraycopy(dataList.get(i), 0, data, index,
					dataList.get(i).length);
			index += dataList.get(i).length;
		}

		return data;
	}

	// -------------------------------------------------------------
	// 序列化文件信息类型的消息
	public static byte[] SerializationFileInfoMsg(int iMsgType,
			long iFileLenght, String iClientFileName,
			String iClietnDirectoryName, String iServerFileName,
			String iServerDirectoryName, String iExtension, String iFileMD5)
			throws UnsupportedEncodingException {
		byte[] data = null;

		// byte[] packetTypeBytes =
		// BitConverter.GetBytes((int)EPackageType.FileInfoMsg);
		// byte[] msgTypeBytes = BitConverter.GetBytes(iMsgType);
		// byte[] fileLenghtBytes = BitConverter.GetBytes(iFileLenght);
		// byte[] clientFileNameBytes = WriteStringToBytes(iClientFileName);
		// byte[] clientDirectoryNameBytes =
		// WriteStringToBytes(iClietnDirectoryName);
		// byte[] serverFileNameBytes = WriteStringToBytes(iServerFileName);
		// byte[] serverDirectoryNameBytes =
		// WriteStringToBytes(iServerDirectoryName);
		// byte[] extensionBytes = WriteStringToBytes(iExtension);
		// byte[] fileMD5Bytes = WriteStringToBytes(iFileMD5);
		byte[] packetTypeBytes = CommonFunction
				.IntToByte(EPackageType.FileInfoMsg);
		byte[] msgTypeBytes = CommonFunction.IntToByte(iMsgType);
		byte[] fileLenghtBytes = CommonFunction.LongToByte(iFileLenght);

		byte[] clientFileNameBytes = iClientFileName.getBytes("UTF8");
		byte[] clientFileNameLenghtBytes = CommonFunction
				.IntToByte(clientFileNameBytes.length);

		byte[] clientDirectoryNameBytes = iClietnDirectoryName.getBytes("UTF8");
		byte[] clientDirectoryNameLenghtBytes = CommonFunction
				.IntToByte(clientDirectoryNameBytes.length);

		byte[] serverFileNameBytes = iServerFileName.getBytes("UTF8");
		byte[] serverFileNameLenghtBytes = CommonFunction
				.IntToByte(serverFileNameBytes.length);

		byte[] serverDirectoryNameBytes = iServerDirectoryName.getBytes("UTF8");
		byte[] serverDirectoryNameLenghtBytes = CommonFunction
				.IntToByte(serverDirectoryNameBytes.length);

		byte[] extensionBytes = iExtension.getBytes("UTF8");
		byte[] extensionLenghtBytes = CommonFunction
				.IntToByte(extensionBytes.length);

		byte[] fileMD5Bytes = iFileMD5.getBytes("UTF8");
		byte[] fileMD5LenghtBytes = CommonFunction
				.IntToByte(fileMD5Bytes.length);
		//
		// int dataLenght = packetTypeBytes.Length + msgTypeBytes.Length +
		// fileLenghtBytes.Length
		// + clientFileNameBytes.Length + clientDirectoryNameBytes.Length
		// + serverFileNameBytes.Length + serverDirectoryNameBytes.Length
		// + extensionBytes.Length + fileMD5Bytes.Length;
		//
		int dataLenght = packetTypeBytes.length + msgTypeBytes.length
				+ fileLenghtBytes.length + clientFileNameBytes.length
				+ clientDirectoryNameBytes.length + serverFileNameBytes.length
				+ serverDirectoryNameBytes.length + extensionBytes.length
				+ fileMD5Bytes.length + clientFileNameLenghtBytes.length
				+ clientDirectoryNameLenghtBytes.length
				+ serverFileNameLenghtBytes.length
				+ serverDirectoryNameLenghtBytes.length
				+ extensionLenghtBytes.length + fileMD5LenghtBytes.length;
		//
		// data = new byte[dataLenght + 4];
		data = new byte[dataLenght + 4];
		//
		// byte[] dataLenghtBytes = BitConverter.GetBytes(dataLenght);
		byte[] dataLenghtBytes = CommonFunction.IntToByte(dataLenght);
		//
		// dataLenghtBytes.CopyTo(data, 0);
		int index = 0;
		System.arraycopy(dataLenghtBytes, 0, data, index,
				dataLenghtBytes.length);
		index += dataLenghtBytes.length;
		//
		// int index = 4;
		// packetTypeBytes.CopyTo(data, index);
		// index += packetTypeBytes.Length;
		System.arraycopy(packetTypeBytes, 0, data, index,
				packetTypeBytes.length);
		index += packetTypeBytes.length;
		// msgTypeBytes.CopyTo(data, index);
		// index += msgTypeBytes.Length;
		System.arraycopy(msgTypeBytes, 0, data, index, msgTypeBytes.length);
		index += msgTypeBytes.length;
		// fileLenghtBytes.CopyTo(data, index);
		// index += fileLenghtBytes.Length;
		System.arraycopy(fileLenghtBytes, 0, data, index,
				fileLenghtBytes.length);
		index += fileLenghtBytes.length;
		// clientFileNameBytes.CopyTo(data, index);
		// index += clientFileNameBytes.Length;

		System.arraycopy(clientFileNameLenghtBytes, 0, data, index,
				clientFileNameLenghtBytes.length);
		index += clientFileNameLenghtBytes.length;
		System.arraycopy(clientFileNameBytes, 0, data, index,
				clientFileNameBytes.length);
		index += clientFileNameBytes.length;
		// clientDirectoryNameBytes.CopyTo(data, index);
		// index += clientDirectoryNameBytes.Length;

		System.arraycopy(clientDirectoryNameLenghtBytes, 0, data, index,
				clientDirectoryNameLenghtBytes.length);
		index += clientDirectoryNameLenghtBytes.length;
		System.arraycopy(clientDirectoryNameBytes, 0, data, index,
				clientDirectoryNameBytes.length);
		index += clientDirectoryNameBytes.length;
		// serverFileNameBytes.CopyTo(data, index);
		// index += serverFileNameBytes.Length;

		System.arraycopy(serverFileNameLenghtBytes, 0, data, index,
				serverFileNameLenghtBytes.length);
		index += serverFileNameLenghtBytes.length;
		System.arraycopy(serverFileNameBytes, 0, data, index,
				serverFileNameBytes.length);
		index += serverFileNameBytes.length;
		// serverDirectoryNameBytes.CopyTo(data, index);
		// index += serverDirectoryNameBytes.Length;

		System.arraycopy(serverDirectoryNameLenghtBytes, 0, data, index,
				serverDirectoryNameLenghtBytes.length);
		index += serverDirectoryNameLenghtBytes.length;
		System.arraycopy(serverDirectoryNameBytes, 0, data, index,
				serverDirectoryNameBytes.length);
		index += serverDirectoryNameBytes.length;
		// extensionBytes.CopyTo(data, index);
		// index += extensionBytes.Length;

		System.arraycopy(extensionLenghtBytes, 0, data, index,
				extensionLenghtBytes.length);
		index += extensionLenghtBytes.length;
		System.arraycopy(extensionBytes, 0, data, index, extensionBytes.length);
		index += extensionBytes.length;
		// fileMD5Bytes.CopyTo(data, index);
		// index += fileMD5Bytes.Length;

		System.arraycopy(fileMD5LenghtBytes, 0, data, index,
				fileMD5LenghtBytes.length);
		index += fileMD5LenghtBytes.length;
		System.arraycopy(fileMD5Bytes, 0, data, index, fileMD5Bytes.length);
		index += fileMD5Bytes.length;

		return data;
	}

	// -------------------------------------------------------------
	// 序列化文件块信息类型的消�?
	public static byte[] SerializationFileBlockMsg(int iMsgType, long iOffset,
			byte[] iFileBlockData) {
		byte[] data = null;

		// byte[] packetTypeByte =
		// BitConverter.GetBytes((int)EPackageType.FileBlockMsg);
		// byte[] msgTypeBytes = BitConverter.GetBytes(iMsgType);

		// packetType
		byte[] packetTypeBytes = CommonFunction
				.IntToByte(EPackageType.FileBlockMsg);

		// msgType
		byte[] messageTypeByte = CommonFunction.IntToByte(iMsgType);

		// byte[] offsetBytes = BitConverter.GetBytes(iOffset);
		//
		// byte[] fileBlockDataLenght =
		// BitConverter.GetBytes(iFileBlockData.Length);
		byte[] offsetBytes = CommonFunction.LongToByte(iOffset);

		byte[] fileBlockDataLenght = CommonFunction
				.IntToByte(iFileBlockData.length);
		//
		//
		//
		// int dataLenght = packetTypeByte.Length + msgTypeBytes.Length +
		// offsetBytes.Length
		// + fileBlockDataLenght.Length + iFileBlockData.Length;
		//
		int dataLenght = packetTypeBytes.length + messageTypeByte.length
				+ offsetBytes.length + fileBlockDataLenght.length
				+ iFileBlockData.length;
		// data = new byte[dataLenght + 4];

		data = new byte[dataLenght + 4];
		//
		// byte[] dataLenghtBytes = BitConverter.GetBytes(dataLenght);
		//
		// dataLenghtBytes.CopyTo(data, 0);

		byte[] dataLenghtBytes = CommonFunction.IntToByte(dataLenght);

		int index = 0;

		System.arraycopy(dataLenghtBytes, 0, data, index,
				dataLenghtBytes.length);
		index += 4;
		//
		// int index = 4;
		//
		// packetTypeByte.CopyTo(data, index);
		// index += packetTypeByte.Length;
		System.arraycopy(packetTypeBytes, 0, data, index,
				packetTypeBytes.length);
		index += packetTypeBytes.length;

		// msgTypeBytes.CopyTo(data, index);
		// index += msgTypeBytes.Length;
		System.arraycopy(messageTypeByte, 0, data, index,
				messageTypeByte.length);
		index += messageTypeByte.length;

		// offsetBytes.CopyTo(data, index);
		// index += offsetBytes.Length;
		System.arraycopy(offsetBytes, 0, data, index, offsetBytes.length);
		index += offsetBytes.length;

		// fileBlockDataLenght.CopyTo(data, index);
		// index += fileBlockDataLenght.Length;
		System.arraycopy(fileBlockDataLenght, 0, data, index,
				fileBlockDataLenght.length);
		index += fileBlockDataLenght.length;

		// iFileBlockData.CopyTo(data, index);
		System.arraycopy(iFileBlockData, 0, data, index, iFileBlockData.length);
		index += iFileBlockData.length;

		return data;
	}

	// 序列化服务器信息类型的消�?
	public static byte[] SerializationServerInfoMsg(int iMsgType,
			String iServerIP, int iPort, String iServerNetworkCordNumber,
			String iViceServerName) throws UnsupportedEncodingException {
		byte[] data = null;

		byte[] packetTypeBytes = CommonFunction
				.IntToByte(EPackageType.ServerInfoMsg);
		byte[] msgTypeBytes = CommonFunction.IntToByte(iMsgType);
		byte[] serverIpBytes = iServerIP.getBytes("UTF8");
		byte[] serverIpLenghtBytes = CommonFunction
				.IntToByte(serverIpBytes.length);
		byte[] serverPortBytes = CommonFunction.IntToByte(iPort);
		byte[] serverNetworkCardBytes = iServerNetworkCordNumber
				.getBytes("UTF8");
		byte[] serverNetworkCardLenghtBytes = CommonFunction
				.IntToByte(serverNetworkCardBytes.length);
		byte[] viceServerNameBytes = iViceServerName.getBytes("UTF8");
		byte[] viceServerNameLenghtBytes = CommonFunction
				.IntToByte(viceServerNameBytes.length);

		int dataLenght = packetTypeBytes.length + msgTypeBytes.length
				+ serverIpBytes.length + serverPortBytes.length
				+ serverNetworkCardBytes.length + viceServerNameBytes.length
				+ serverIpLenghtBytes.length
				+ serverNetworkCardLenghtBytes.length
				+ viceServerNameLenghtBytes.length;

		data = new byte[dataLenght + 4];

		byte[] dataLenghtBytes = CommonFunction.IntToByte(dataLenght);

		// dataLenghtBytes.CopyTo(data, 0);
		int index = 0;
		System.arraycopy(dataLenghtBytes, 0, data, index,
				dataLenghtBytes.length);
		index += dataLenghtBytes.length;

		// packetTypeBytes.CopyTo(data, index);
		// index += packetTypeBytes.Length;
		System.arraycopy(packetTypeBytes, 0, data, index,
				packetTypeBytes.length);
		index += packetTypeBytes.length;
		// msgTypeBytes.CopyTo(data, index);
		// index += msgTypeBytes.Length;
		System.arraycopy(msgTypeBytes, 0, data, index, msgTypeBytes.length);
		index += msgTypeBytes.length;
		// serverIpBytes.CopyTo(data, index);
		// index += serverIpBytes.Length;
		System.arraycopy(serverIpLenghtBytes, 0, data, index,
				serverIpLenghtBytes.length);
		index += serverIpLenghtBytes.length;

		System.arraycopy(serverIpBytes, 0, data, index, serverIpBytes.length);
		index += serverIpBytes.length;

		// serverPortBytes.CopyTo(data, index);
		// index += serverPortBytes.Length;
		System.arraycopy(serverPortBytes, 0, data, index,
				serverPortBytes.length);
		index += serverPortBytes.length;

		// serverNetworkCardBytes.CopyTo(data, index);
		// index += serverNetworkCardBytes.Length;
		System.arraycopy(serverNetworkCardLenghtBytes, 0, data, index,
				serverNetworkCardLenghtBytes.length);
		index += serverNetworkCardLenghtBytes.length;

		System.arraycopy(serverNetworkCardBytes, 0, data, index,
				serverNetworkCardBytes.length);
		index += serverNetworkCardBytes.length;

		// viceServerNameBytes.CopyTo(data, index);
		// index += viceServerNameBytes.Length;
		System.arraycopy(viceServerNameLenghtBytes, 0, data, index,
				viceServerNameLenghtBytes.length);
		index += viceServerNameLenghtBytes.length;

		System.arraycopy(viceServerNameBytes, 0, data, index,
				viceServerNameBytes.length);
		index += viceServerNameBytes.length;

		return data;
	}

	// -------------------------------------------------------------
	// 反序列化命令型消�?
	public static MsgCommand DeserializationCommand(byte[] iData,
			int iStartIndex) throws UnsupportedEncodingException {
		MsgCommand commandMsg = null;

		int msgType = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;

		int commandMsgLength = CommonFunction
				.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;

		byte[] commandBytes = new byte[commandMsgLength];
		System.arraycopy(iData, iStartIndex, commandBytes, 0, commandMsgLength);

		String commandContent = new String(commandBytes, "UTF8");

		commandMsg = new MsgCommand(msgType, commandContent);
		return commandMsg;
	}

	// -------------------------------------------------------------
	// 反序列化参数信息消息
	public static MsgParameter DeserializationParameter(byte[] iData,
			int iStartIndex) {
		MsgParameter paraMsg = null;
		int index = iStartIndex;
		// int msgType = BitConverter.ToInt32(iData, index);
		// index += 4;
		int msgType = CommonFunction.ByteArrayToInt(iData, index);
		index += 4;
		//
		// int paraLenght = BitConverter.ToInt32(iData, index);
		// index += 4;
		int paraLenght = CommonFunction.ByteArrayToInt(iData, index);
		index += 4;
		//
		// ESFTParameter[] parameters = new ESFTParameter[paraLenght];
		ESFTParameter[] parameters = new ESFTParameter[paraLenght];
		//
		// for (int i = 0; i < paraLenght; i++)
		// {
		for (int i = 0; i < paraLenght; i++) {
			// parameters[i] = new ESFTParameter();
			parameters[i] = new ESFTParameter();
			//
			// int paraNameLenght = BitConverter.ToInt32(iData, index);
			// index += 4;
			int paraNameLenght = CommonFunction.ByteArrayToInt(iData, index);
			index += 4;
			//
			// byte[] paraNameBytes = new byte[paraNameLenght];
			// Buffer.BlockCopy(iData, index, paraNameBytes, 0, paraNameLenght);
			// index += paraNameLenght;
			byte[] paraNameBytes = new byte[paraNameLenght];
			System.arraycopy(iData, index, paraNameBytes, 0,
					paraNameBytes.length);
			index += paraNameLenght;
			//
			// string paraName = m_utf8Encoding.GetString(paraNameBytes);
			String paraName = new String(paraNameBytes);
			//
			// int paraContentLenght = BitConverter.ToInt32(iData, index);
			// index += 4;
			//
			int paraContentLenght = CommonFunction.ByteArrayToInt(iData, index);
			index += 4;
			// byte[] paraContentBytes = new byte[paraContentLenght];
			// Buffer.BlockCopy(iData, index, paraContentBytes, 0,
			// paraContentLenght);
			// index += paraContentLenght;
			//
			byte[] paraContentBytes = new byte[paraContentLenght];
			System.arraycopy(iData, index, paraContentBytes, 0,
					paraContentBytes.length);
			index += paraContentLenght;
			// string paraContent = m_utf8Encoding.GetString(paraContentBytes);
			//
			String paraContent = new String(paraContentBytes);
			// parameters[i].ParaName = paraName;
			// parameters[i].ParaContent = paraContent;
			parameters[i].m_ParameterName = paraName;
			parameters[i].m_ParaContent = paraContent;
			// }
		}
		//
		// paraMsg = new MsgParameter(msgType, parameters);
		paraMsg = new MsgParameter(msgType, parameters);

		return paraMsg;

	}

	// -------------------------------------------------------------
	// 反序列化文件信息消息
	public static MsgFileInfo DeserializationFileInfo(byte[] iData,
			int iStartIndex) throws UnsupportedEncodingException {
		MsgFileInfo fileInfoMsg = null;
		// int msgType = BitConverter.ToInt32(iData, index);
		// index += 4;
		int msgType = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;

		// long fileLenght = BitConverter.ToInt64(iData, index);
		// index += 8;
		long fileLenght = CommonFunction.ByteToLong(iData, iStartIndex);
		iStartIndex += 8;

		// string clientFileName = ReadBytesToString(iData, ref index);
		int clientFileNameLenght = CommonFunction.ByteArrayToInt(iData,
				iStartIndex);
		iStartIndex += 4;

		String clientFileName = CommonFunction.ByteArrayToString(iData,
				iStartIndex, clientFileNameLenght);
		iStartIndex += clientFileNameLenght;
		// string clientDirectoryName = ReadBytesToString(iData, ref index);
		int clientDirectoryNameLenght = CommonFunction.ByteArrayToInt(iData,
				iStartIndex);
		iStartIndex += 4;

		String clientDirectoryName = CommonFunction.ByteArrayToString(iData,
				iStartIndex, clientDirectoryNameLenght);
		iStartIndex += clientDirectoryNameLenght;

		// string serverFileName = ReadBytesToString(iData, ref index);
		int serverFileNameLenght = CommonFunction.ByteArrayToInt(iData,
				iStartIndex);
		iStartIndex += 4;

		String serverFileName = CommonFunction.ByteArrayToString(iData,
				iStartIndex, serverFileNameLenght);
		iStartIndex += serverFileNameLenght;

		// string serverDirectoryName = ReadBytesToString(iData, ref index);
		int serverDirectoryNameLenght = CommonFunction.ByteArrayToInt(iData,
				iStartIndex);
		iStartIndex += 4;

		String serverDirectoryName = CommonFunction.ByteArrayToString(iData,
				iStartIndex, serverDirectoryNameLenght);
		iStartIndex += serverDirectoryNameLenght;

		// string extension = ReadBytesToString(iData, ref index);
		int extensionLenght = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;

		String extension = CommonFunction.ByteArrayToString(iData, iStartIndex,
				extensionLenght);
		iStartIndex += extensionLenght;

		// string fileMD5 = ReadBytesToString(iData, ref index);
		int fileMD5Lenght = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;

		String fileMD5 = CommonFunction.ByteArrayToString(iData, iStartIndex,
				fileMD5Lenght);
		iStartIndex += fileMD5Lenght;

		fileInfoMsg = new MsgFileInfo(msgType, fileLenght, clientFileName,
				clientDirectoryName, serverFileName, serverDirectoryName,
				extension, fileMD5);
		return fileInfoMsg;
	}

	// -------------------------------------------------------------
	// 反序列化文件块消�?
	public static MsgFileBlock DeserializationFileBlock(byte[] iData,
			int iStartIndex) {
		MsgFileBlock fileBlockMsg = null;
		// int index = iStartIndex;
		// int msgType = BitConverter.ToInt32(iData, index);
		// index += 4;
		int msgType = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;
		//
		// long offset = BitConverter.ToInt64(iData, index);
		// index += 8;
		long offset = CommonFunction.ByteToLong(iData, iStartIndex);
		iStartIndex += 8;
		//
		// int dataLenght = BitConverter.ToInt32(iData, index);
		// index += 4;
		int dataLenght = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;
		//
		// byte[] data = new byte[dataLenght];
		byte[] fileBlockData = new byte[dataLenght];
		//
		// Buffer.BlockCopy(iData, index, data, 0, dataLenght);
		System.arraycopy(iData, iStartIndex, fileBlockData, 0,
				fileBlockData.length);
		//
		fileBlockMsg = new MsgFileBlock(msgType, offset, fileBlockData);

		return fileBlockMsg;
	}

	// / <summary>
	// / 反序列化服务器信息消�?
	// / </summary>
	// / <param name="iData"></param>
	// / <param name="iStartIndex"></param>
	// / <returns></returns>
	public static MsgServerInfo DeserializationServerInfo(byte[] iData,
			int iStartIndex) throws UnsupportedEncodingException {
		MsgServerInfo serverInfoMsg = null;

		int msgType = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;

		int serverIpLenght = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;

		String serverIP = CommonFunction.ByteArrayToString(iData, iStartIndex,
				serverIpLenght);
		iStartIndex += serverIpLenght;

		int serverPort = CommonFunction.ByteArrayToInt(iData, iStartIndex);
		iStartIndex += 4;

		int serverNetworkCardNumberLenght = CommonFunction.ByteArrayToInt(
				iData, iStartIndex);
		iStartIndex += 4;

		String serverNetworkCardNumber = CommonFunction.ByteArrayToString(
				iData, iStartIndex, serverNetworkCardNumberLenght);
		iStartIndex += serverNetworkCardNumberLenght;

		int viceServerNameLenght = CommonFunction.ByteArrayToInt(iData,
				iStartIndex);
		iStartIndex += 4;

		String viceServerName = CommonFunction.ByteArrayToString(iData,
				iStartIndex, viceServerNameLenght);
		iStartIndex += viceServerNameLenght;

		serverInfoMsg = new MsgServerInfo(msgType, serverIP, serverPort,
				serverNetworkCardNumber, viceServerName);

		return serverInfoMsg;
	}
}
