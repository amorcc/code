using ESFT.Common.Log;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.Net;
using System.Net.Sockets;

namespace ESFT.Common
{
    public class ClientSocket
    {
        protected Socket m_ClientSocket;
        public bool IsStop = false;

        protected int mSendTimeOut = 3000;
        protected int mReceiveTimeOut = 5000;

        /// <summary>
        /// 接受到数据包
        /// </summary>
        public event ESFT.Common.TypeDefinitions.ClientReceiveCommandPacket EventClientReceiveCommandPacket;

        protected virtual void OnEventClientReceiveCommandPacket(MsgCommand iMsg)
        {
            if (this.EventClientReceiveCommandPacket != null)
            {
                this.EventClientReceiveCommandPacket(iMsg);
            }
        }

        protected bool IsSocketConnected()
        {
            if (this.m_ClientSocket == null)
            {
                return false;
            }

            bool part1 = m_ClientSocket.Poll(1000, SelectMode.SelectRead);
            bool part2 = (m_ClientSocket.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }

        protected void InitSocket(string iMasterServerIP, int iMasterPort)
        {
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse(iMasterServerIP), iMasterPort);
                this.m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                this.m_ClientSocket.SendTimeout = this.mSendTimeOut;
                this.m_ClientSocket.ReceiveTimeout = this.mReceiveTimeOut;

                this.m_ClientSocket.Connect(ipEndPoint);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error(ex.ToString());
                throw ex;
            }
        }

        public bool SendMsg(EsftMsg iMsg)
        {
            try
            {
                if (this.m_ClientSocket != null && this.IsSocketConnected()
                    && this.IsStop == false)
                {
                    byte[] msgData = EMessage.Serialization(iMsg);
                    if (msgData == null)
                    {
                        EMessage.Serialization(iMsg);
                    }
                    this.m_ClientSocket.Send(msgData);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error(ex.ToString());
                return false;
            }
        }

        public EsftMsg ReveiceMsg()
        {
            EsftMsg msg = null;
            try
            {
                //System.Threading.Thread.Sleep(1);
                if (this.m_ClientSocket != null && this.IsSocketConnected()
                    && this.IsStop == false)
                {
                    byte[] packetLenghtBytes = new byte[4];
                    this.m_ClientSocket.Receive(packetLenghtBytes, 0, 4, SocketFlags.None);
                    int packetLenght = BitConverter.ToInt32(packetLenghtBytes, 0);

                    byte[] msgDataBytes = new byte[packetLenght];
                    int receiveLenght = this.m_ClientSocket.Receive(msgDataBytes, 0, msgDataBytes.Length, SocketFlags.None);

                    while (receiveLenght < packetLenght && this.IsSocketConnected())
                    {
                        int againReceiveLenght = this.m_ClientSocket.Receive(msgDataBytes, receiveLenght, packetLenght - receiveLenght, SocketFlags.None);
                        receiveLenght += againReceiveLenght;
                    }

                    if (packetLenght != receiveLenght)
                    {
                        MyLogManage.Debug("", "&&&&&&&&&&&&&&&", receiveLenght.ToString());
                    }

                    msg = EMessage.DeserializationPacket(msgDataBytes, 0);
                    if (msg != null && (msg is MsgCommand) && (msg as MsgCommand).MsgType == EMessageType.M_Server_SendCommandInfo)
                    {
                        this.OnEventClientReceiveCommandPacket(msg as MsgCommand);
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error(ex.ToString());
            }

            return msg;
        }

        public void CloseSocket()
        {
            try
            {
                if (this.m_ClientSocket != null)
                {
                    this.m_ClientSocket.Close();
                    this.m_ClientSocket.Dispose();
                    this.m_ClientSocket = null;
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error(ex.ToString());
            }
        }
    }
}
