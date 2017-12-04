using ESFT.Common.TypeDefinitions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESFT.Message
{
    public class EMessage
    {
        public static System.Text.UTF8Encoding m_utf8Encoding = new UTF8Encoding();

        /// <summary>
        /// 序列化消息
        /// </summary>
        /// <param name="iMsg"></param>
        /// <returns></returns>
        public static byte[] Serialization(EsftMsg iMsg)
        {
            byte[] data = null;

            if (iMsg is MsgCommand && iMsg.PacketType == EPackageType.CommandMsg)
            {
                MsgCommand commandMsg = (MsgCommand)iMsg;

                data = SerializationCommandMsg(commandMsg.MsgType, commandMsg.Command);
            }
            else if (iMsg is MsgParameter && iMsg.PacketType == EPackageType.ParameterMsg)
            {
                MsgParameter paraMsg = (MsgParameter)iMsg;

                data = SerializationParameterMsg((int)paraMsg.PacketType, paraMsg.MsgType, paraMsg.Parameters);
            }
            else if (iMsg is MsgFileInfo && iMsg.PacketType == EPackageType.FileInfoMsg)
            {
                MsgFileInfo fileInfoMsg = (MsgFileInfo)iMsg;

                data = SerializationFileInfoMsg(fileInfoMsg.MsgType,
                    fileInfoMsg.FileLenght, fileInfoMsg.ClientFileName,
                    fileInfoMsg.ClietnDirectoryName, fileInfoMsg.ServerFileName,
                    fileInfoMsg.ServerDirectoryName, fileInfoMsg.Extension,
                    fileInfoMsg.FileMD5);
            }
            else if (iMsg is MsgFileBlock && iMsg.PacketType == EPackageType.FileBlockMsg)
            {
                MsgFileBlock fileBlockMsg = (MsgFileBlock)iMsg;

                data = SerializationFileBlockMsg(fileBlockMsg.MsgType, fileBlockMsg.Offset, fileBlockMsg.FileBlockData);
            }
            else if (iMsg is MsgServerInfo && iMsg.PacketType == EPackageType.ServerInfoMsg)
            {
                MsgServerInfo serverInfoMsg = (MsgServerInfo)iMsg;
                data = SerializationServerInfoMsg(serverInfoMsg.MsgType, serverInfoMsg.ServerIP, serverInfoMsg.Port, serverInfoMsg.ServerNetworkCordNumber, serverInfoMsg.ViceServerName);
            }
            else
            {
                data = null;
            }

            return data;
        }

        /// <summary>
        /// 反序列化消息
        /// </summary>
        /// <param name="iData"></param>
        /// <returns></returns>
        public static EsftMsg DeserializationPacket(byte[] iData, int iStartIndex)
        {
            EsftMsg msg = null;

            int packetType = BitConverter.ToInt32(iData, iStartIndex);
            //byte[] data = new byte[iData.Length - 4];
            //Buffer.BlockCopy(iData, 4, data, 0, data.Length);
            iStartIndex += 4;

            switch (packetType)
            {
                case (int)EPackageType.CommandMsg:
                    msg = DeserializationCommand(iData, iStartIndex);
                    break;
                case (int)EPackageType.ParameterMsg:
                    msg = DeserializationParameter(iData, iStartIndex);
                    break;
                case (int)EPackageType.FileInfoMsg:
                    msg = DeserializationFileInfo(iData, iStartIndex);
                    break;
                case (int)EPackageType.FileBlockMsg:
                    msg = DeserializationFileBlock(iData, iStartIndex);
                    break;
                case (int)EPackageType.ServerInfoMsg:
                    msg = DeserializationServerInfo(iData, iStartIndex);
                    break;

            }

            return msg;
        }

        #region 序列化命令型、参数类型、文件信息类型、文件块类型的消息
        /// <summary>
        /// 序列化命令型消息
        /// </summary>
        /// <param name="iEMessageType"></param>
        /// <returns></returns>
        public static byte[] SerializationCommandMsg(int iMsgType, string iCommand)
        {
            byte[] data = null;

            try
            {
                byte[] packetTypeByte = BitConverter.GetBytes((int)EPackageType.CommandMsg);
                //msg类型
                byte[] messageTypeByte = BitConverter.GetBytes(iMsgType);
                //command内容 
                byte[] commandByte = m_utf8Encoding.GetBytes(iCommand);
                //command byte[] 长度
                byte[] commandMsgLenght = BitConverter.GetBytes(commandByte.Length);

                int dataLenght = packetTypeByte.Length + messageTypeByte.Length
                                + commandMsgLenght.Length + commandByte.Length;
                data = new byte[dataLenght + 4];

                byte[] dataLenghtBytes = BitConverter.GetBytes(dataLenght);

                dataLenghtBytes.CopyTo(data, 0);
                packetTypeByte.CopyTo(data, dataLenghtBytes.Length);
                messageTypeByte.CopyTo(data, dataLenghtBytes.Length + packetTypeByte.Length);
                commandMsgLenght.CopyTo(data, dataLenghtBytes.Length + packetTypeByte.Length + messageTypeByte.Length);
                commandByte.CopyTo(data, dataLenghtBytes.Length + packetTypeByte.Length + messageTypeByte.Length + commandMsgLenght.Length);
            }
            catch (Exception ex)
            {

                log4net.LogManager.GetLogger("EMessage.cs").Error("序列化MsgCommand错误：" + ex.ToString());
            }

            return data;
        }

        /// <summary>
        /// 序列化参数类型的消息
        /// </summary>
        /// <param name="iMsgType"></param>
        /// <param name="iParameters"></param>
        /// <returns></returns>
        public static byte[] SerializationParameterMsg(int iPacketType, int iMsgType, ESFTParameter[] iParameters)
        {
            byte[] data = null;

            try
            {
                int dataBitNum = 0;
                List<byte[]> dataList = new List<byte[]>();

                //记录packetType
                byte[] packetTypeByte = BitConverter.GetBytes(iPacketType);
                dataList.Add(packetTypeByte);
                dataBitNum += packetTypeByte.Length;

                //记录msgType，消息类型
                byte[] msgTypeBytes = BitConverter.GetBytes(iMsgType);
                dataList.Add(msgTypeBytes);
                dataBitNum += msgTypeBytes.Length;

                //记录参数的个数
                byte[] parameterNum = BitConverter.GetBytes(iParameters.Length);
                dataList.Add(parameterNum);
                dataBitNum += parameterNum.Length;

                for (int i = 0; i < iParameters.Length; i++)
                {
                    string paraName = iParameters[i].ParaName;
                    string paraContent = iParameters[i].ParaContent;

                    byte[] paraNameBytes = m_utf8Encoding.GetBytes(paraName);
                    byte[] paraContentBytes = m_utf8Encoding.GetBytes(paraContent);

                    byte[] nameLenghtBytes = BitConverter.GetBytes(paraNameBytes.Length);
                    dataList.Add(nameLenghtBytes);
                    dataBitNum += nameLenghtBytes.Length;

                    dataList.Add(paraNameBytes);
                    dataBitNum += paraNameBytes.Length;

                    byte[] contentLenghtBytes = BitConverter.GetBytes(paraContentBytes.Length);
                    dataList.Add(contentLenghtBytes);
                    dataBitNum += contentLenghtBytes.Length;

                    dataList.Add(paraContentBytes);
                    dataBitNum += paraContentBytes.Length;
                }

                int dataLenght = dataBitNum;

                data = new byte[dataLenght + 4];

                byte[] dataLenghtBytes = BitConverter.GetBytes(dataLenght);

                dataLenghtBytes.CopyTo(data, 0);

                int index = 4;

                for (int i = 0; i < dataList.Count; i++)
                {
                    dataList[i].CopyTo(data, index);
                    index += dataList[i].Length;
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("序列化MsgParameter错误：" + ex.ToString());
            }

            return data;
        }

        /// <summary>
        /// 序列化文件信息类型的消息
        /// </summary>
        /// <param name="iMsgType"></param>
        /// <param name="iFileLenght"></param>
        /// <param name="iClientFileName"></param>
        /// <param name="iClietnDirectoryName"></param>
        /// <param name="iServerFileName"></param>
        /// <param name="iServerDirectoryName"></param>
        /// <param name="iExtension"></param>
        /// <param name="iFileMD5"></param>
        /// <returns></returns>
        public static byte[] SerializationFileInfoMsg(int iMsgType, long iFileLenght, string iClientFileName
                    , string iClietnDirectoryName, string iServerFileName
                    , string iServerDirectoryName, string iExtension
                    , string iFileMD5)
        {
            byte[] data = null;

            try
            {
                byte[] packetTypeBytes = BitConverter.GetBytes((int)EPackageType.FileInfoMsg);
                byte[] msgTypeBytes = BitConverter.GetBytes(iMsgType);
                byte[] fileLenghtBytes = BitConverter.GetBytes(iFileLenght);
                byte[] clientFileNameBytes = WriteStringToBytes(iClientFileName);
                byte[] clientDirectoryNameBytes = WriteStringToBytes(iClietnDirectoryName);
                byte[] serverFileNameBytes = WriteStringToBytes(iServerFileName);
                byte[] serverDirectoryNameBytes = WriteStringToBytes(iServerDirectoryName);
                byte[] extensionBytes = WriteStringToBytes(iExtension);
                byte[] fileMD5Bytes = WriteStringToBytes(iFileMD5);

                int dataLenght = packetTypeBytes.Length + msgTypeBytes.Length + fileLenghtBytes.Length
                    + clientFileNameBytes.Length + clientDirectoryNameBytes.Length
                    + serverFileNameBytes.Length + serverDirectoryNameBytes.Length
                    + extensionBytes.Length + fileMD5Bytes.Length;


                data = new byte[dataLenght + 4];

                byte[] dataLenghtBytes = BitConverter.GetBytes(dataLenght);

                dataLenghtBytes.CopyTo(data, 0);

                int index = 4;
                packetTypeBytes.CopyTo(data, index);
                index += packetTypeBytes.Length;
                msgTypeBytes.CopyTo(data, index);
                index += msgTypeBytes.Length;
                fileLenghtBytes.CopyTo(data, index);
                index += fileLenghtBytes.Length;
                clientFileNameBytes.CopyTo(data, index);
                index += clientFileNameBytes.Length;
                clientDirectoryNameBytes.CopyTo(data, index);
                index += clientDirectoryNameBytes.Length;
                serverFileNameBytes.CopyTo(data, index);
                index += serverFileNameBytes.Length;
                serverDirectoryNameBytes.CopyTo(data, index);
                index += serverDirectoryNameBytes.Length;
                extensionBytes.CopyTo(data, index);
                index += extensionBytes.Length;
                fileMD5Bytes.CopyTo(data, index);
                index += fileMD5Bytes.Length;

            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("序列化MsgFileInfo错误：" + ex.ToString());
            }

            return data;
        }

        /// <summary>
        /// 序列化文件信息类型的消息
        /// </summary>
        /// <param name="iMsgType"></param>
        /// <param name="iFileParameters"></param>
        /// <returns></returns>
        public static byte[] SerializationFileInfoMsg2(int iMsgType, ESFTParameter[] iFileParameters)
        {
            return SerializationParameterMsg((int)EPackageType.FileInfoMsg, iMsgType, iFileParameters);
        }

        /// <summary>
        /// 序列化文件块信息类型的消息
        /// </summary>
        /// <param name="iMsgType"></param>
        /// <param name="iFileBlockData"></param>
        /// <returns></returns>
        public static byte[] SerializationFileBlockMsg(int iMsgType, long iOffset, byte[] iFileBlockData)
        {
            byte[] data = null;

            try
            {
                byte[] packetTypeByte = BitConverter.GetBytes((int)EPackageType.FileBlockMsg);
                byte[] msgTypeBytes = BitConverter.GetBytes(iMsgType);

                byte[] offsetBytes = BitConverter.GetBytes(iOffset);

                byte[] fileBlockDataLenght = BitConverter.GetBytes(iFileBlockData.Length);



                int dataLenght = packetTypeByte.Length + msgTypeBytes.Length + offsetBytes.Length
                        + fileBlockDataLenght.Length + iFileBlockData.Length;

                data = new byte[dataLenght + 4];

                byte[] dataLenghtBytes = BitConverter.GetBytes(dataLenght);

                dataLenghtBytes.CopyTo(data, 0);

                int index = 4;

                packetTypeByte.CopyTo(data, index);
                index += packetTypeByte.Length;
                msgTypeBytes.CopyTo(data, index);
                index += msgTypeBytes.Length;
                offsetBytes.CopyTo(data, index);
                index += offsetBytes.Length;
                fileBlockDataLenght.CopyTo(data, index);
                index += fileBlockDataLenght.Length;
                iFileBlockData.CopyTo(data, index);

            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("序列化MsgFileBlock错误：" + ex.ToString());
            }

            return data;
        }

        /// <summary>
        /// 序列化服务器信息类型的消息
        /// </summary>
        /// <param name="iMsgType"></param>
        /// <param name="iServerIP"></param>
        /// <param name="iPort"></param>
        /// <param name="iServerNetworkCordNumber"></param>
        /// <returns></returns>
        public static byte[] SerializationServerInfoMsg(int iMsgType
            , string iServerIP, int iPort, string iServerNetworkCordNumber
            , string iViceServerName)
        {
            byte[] data = null;

            try
            {
                byte[] packetTypeBytes = BitConverter.GetBytes((int)EPackageType.ServerInfoMsg);
                byte[] msgTypeBytes = BitConverter.GetBytes(iMsgType);
                byte[] serverIpBytes = WriteStringToBytes(iServerIP);
                byte[] serverPortBytes = BitConverter.GetBytes(iPort);
                byte[] serverNetworkCardBytes = WriteStringToBytes(iServerNetworkCordNumber);
                byte[] viceServerNameBytes = WriteStringToBytes(iViceServerName);


                int dataLenght = packetTypeBytes.Length + msgTypeBytes.Length
                    + serverIpBytes.Length + serverPortBytes.Length
                    + serverNetworkCardBytes.Length + viceServerNameBytes.Length;

                data = new byte[dataLenght + 4];

                byte[] dataLenghtBytes = BitConverter.GetBytes(dataLenght);

                dataLenghtBytes.CopyTo(data, 0);

                int index = 4;
                packetTypeBytes.CopyTo(data, index);
                index += packetTypeBytes.Length;
                msgTypeBytes.CopyTo(data, index);
                index += msgTypeBytes.Length;
                serverIpBytes.CopyTo(data, index);
                index += serverIpBytes.Length;
                serverPortBytes.CopyTo(data, index);
                index += serverPortBytes.Length;
                serverNetworkCardBytes.CopyTo(data, index);
                index += serverNetworkCardBytes.Length;
                viceServerNameBytes.CopyTo(data, index);
                index += viceServerNameBytes.Length;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("序列化MsgServerInfo错误：" + ex.ToString());
            }



            return data;
        }
        #endregion

        #region 反序列化文件块、文件信息、参数信息、命令信息的消息
        /// <summary>
        /// 反序列化文件块消息
        /// </summary>
        /// <param name="iData"></param>
        /// <returns></returns>
        static MsgFileBlock DeserializationFileBlock(byte[] iData, int iStartIndex)
        {
            MsgFileBlock fileBlockMsg = null;

            try
            {
                int index = iStartIndex;
                int msgType = BitConverter.ToInt32(iData, index);
                index += 4;

                long offset = BitConverter.ToInt64(iData, index);
                index += 8;

                int dataLenght = BitConverter.ToInt32(iData, index);
                index += 4;

                byte[] data = new byte[dataLenght];

                Buffer.BlockCopy(iData, index, data, 0, dataLenght);

                fileBlockMsg = new MsgFileBlock(msgType, offset, data);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("反序列化MsgFileBlock错误：" + ex.ToString());
            }

            return fileBlockMsg;
        }

        static MsgFileInfo DeserializationFileInfo(byte[] iData, int iStartIndex)
        {
            MsgFileInfo fileInfoMsg = null;

            try
            {
                int index = iStartIndex;
                int msgType = BitConverter.ToInt32(iData, index);
                index += 4;
                long fileLenght = BitConverter.ToInt64(iData, index);
                index += 8;

                string clientFileName = ReadBytesToString(iData, ref index);
                string clientDirectoryName = ReadBytesToString(iData, ref index);
                string serverFileName = ReadBytesToString(iData, ref index);
                string serverDirectoryName = ReadBytesToString(iData, ref index);
                string extension = ReadBytesToString(iData, ref index);
                string fileMD5 = ReadBytesToString(iData, ref index);

                fileInfoMsg = new MsgFileInfo(msgType, fileLenght, clientFileName, clientDirectoryName
                    , serverFileName, serverDirectoryName, extension, fileMD5);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("反序列化MsgFileInfo错误：" + ex.ToString());
            }

            return fileInfoMsg;
        }

        /// <summary>
        /// 反序列化参数信息消息
        /// </summary>
        /// <param name="iData"></param>
        /// <returns></returns>
        static MsgParameter DeserializationParameter(byte[] iData, int iStartIndex)
        {
            MsgParameter paraMsg = null;

            try
            {
                int index = iStartIndex;
                int msgType = BitConverter.ToInt32(iData, index);
                index += 4;

                int paraLenght = BitConverter.ToInt32(iData, index);
                index += 4;

                ESFTParameter[] parameters = new ESFTParameter[paraLenght];

                for (int i = 0; i < paraLenght; i++)
                {
                    parameters[i] = new ESFTParameter();

                    int paraNameLenght = BitConverter.ToInt32(iData, index);
                    index += 4;

                    byte[] paraNameBytes = new byte[paraNameLenght];
                    Buffer.BlockCopy(iData, index, paraNameBytes, 0, paraNameLenght);
                    index += paraNameLenght;

                    string paraName = m_utf8Encoding.GetString(paraNameBytes);

                    int paraContentLenght = BitConverter.ToInt32(iData, index);
                    index += 4;

                    byte[] paraContentBytes = new byte[paraContentLenght];
                    Buffer.BlockCopy(iData, index, paraContentBytes, 0, paraContentLenght);
                    index += paraContentLenght;

                    string paraContent = m_utf8Encoding.GetString(paraContentBytes);

                    parameters[i].ParaName = paraName;
                    parameters[i].ParaContent = paraContent;
                }

                paraMsg = new MsgParameter(msgType, parameters);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("反序列化MsgParameter错误：" + ex.ToString());
            }

            return paraMsg;

        }

        /// <summary>
        /// 反序列化命令消息
        /// </summary>
        /// <param name="iData"></param>
        /// <returns></returns>
        static MsgCommand DeserializationCommand(byte[] iData, int iStartIndex)
        {
            MsgCommand commandMsg = null;

            try
            {
                int msgType = BitConverter.ToInt32(iData, iStartIndex);
                iStartIndex += 4;
                int commandMsgLength = BitConverter.ToInt32(iData, iStartIndex);
                iStartIndex += 4;
                //byte[] commandContentByte = new byte[commandMsgLength];
                //Buffer.BlockCopy(iData, 8, commandContentByte, 0, commandMsgLength);
                string commandContent = m_utf8Encoding.GetString(iData, iStartIndex, iData.Length - iStartIndex);

                commandMsg = new MsgCommand(msgType, commandContent);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("反序列化MsgCommand错误：" + ex.ToString());
            }

            return commandMsg;
        }

        /// <summary>
        /// 反序列化服务器信息消息
        /// </summary>
        /// <param name="iData"></param>
        /// <param name="iStartIndex"></param>
        /// <returns></returns>
        static MsgServerInfo DeserializationServerInfo(byte[] iData, int iStartIndex)
        {
            MsgServerInfo serverInfoMsg = null;

            try
            {
                int index = iStartIndex;
                int msgType = BitConverter.ToInt32(iData, index);
                index += 4;

                string serverIP = ReadBytesToString(iData, ref index);
                int serverPort = BitConverter.ToInt32(iData, index);
                index += 4;
                string serverNetworkCardNumber = ReadBytesToString(iData, ref index);
                string viceServerName = ReadBytesToString(iData, ref index);

                serverInfoMsg = new MsgServerInfo(msgType, serverIP, serverPort, serverNetworkCardNumber, viceServerName);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("反序列化MsgServerInfo错误：" + ex.ToString());
            }


            return serverInfoMsg;
        }
        #endregion

        /// <summary>
        /// 将string的byte[]长度及string的byte[]写入data数组，并返回
        /// </summary>
        /// <param name="iStr"></param>
        /// <returns></returns>
        static byte[] WriteStringToBytes(string iStr)
        {
            byte[] data = null;

            try
            {
                if (iStr == null)
                {
                    iStr = "";
                }
                byte[] strBytes = m_utf8Encoding.GetBytes(iStr);
                byte[] strLenghtBytes = BitConverter.GetBytes(strBytes.Length);

                data = new byte[strBytes.Length + strLenghtBytes.Length];

                strLenghtBytes.CopyTo(data, 0);
                strBytes.CopyTo(data, strLenghtBytes.Length);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("WriteStringToBytes错误：" + ex.ToString());
            }

            return data;
        }

        /// <summary>
        /// 在指定的字节数组偏移中，取出string的长度和string内容
        /// </summary>
        /// <param name="iData"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        static string ReadBytesToString(byte[] iData, ref int index)
        {
            string str = string.Empty;

            try
            {
                int strLength = BitConverter.ToInt32(iData, index);
                index += 4;
                byte[] strBytes = new byte[strLength];
                Buffer.BlockCopy(iData, index, strBytes, 0, strLength);
                index += strLength;

                str = m_utf8Encoding.GetString(strBytes);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("EMessage.cs").Error("ReadBytesToString错误：" + ex.ToString());
            }

            return str;
        }
    }
}
