using ESFT.Common.Log;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.Net;
using System.Net.Sockets;

namespace ESFT.Server
{
    public class AsyncUserToken
    {
        public string mId;
        public Socket mSocket;
        public SocketAsyncEventArgs mSocketAsyncEventArgs;

        /// <summary>
        /// 客户端请求的文件传输类型enum:Download、Upload、none
        /// </summary>
        public TransferType mClientTransferType = TransferType.None;

        /// <summary>
        /// 传输状态
        /// </summary>
        public TransferState mTransferState;

        /// <summary>
        /// 文件传输处理
        /// </summary>
        FileTransferBase mFileTransferHandler = null;

        /// <summary>
        /// 最后一个包的到达时间
        /// </summary>
        public DateTime? mLastPacketTime;

        /// <summary>
        /// 本次连接客户端IP和端口
        /// </summary>
        public EndPoint mClientEndPoint;

        #region 组包分包属性
        /// <summary>
        /// 上次接收MSG是否成功
        /// </summary>
        bool m_GetMsg_Success = true;

        /// <summary>
        /// 上次未接收完成的MSG的包的长度
        /// </summary>
        int m_GetMsg_PackeLenght = 0;

        /// <summary>
        /// 上次接收包长度是否成功
        /// </summary>
        bool m_GetPacketLenght_Success = true;

        /// <summary>
        /// 缓存的上次接收包长度的数据
        /// </summary>
        byte[] m_GetPacketLenght_CacheBytes = new byte[4];

        /// <summary>
        /// 上次接收包长度缓存的字节数
        /// </summary>
        int m_GetPacketLenght_CacheLenght = 0;

        /// <summary>
        /// 目前使用了多少位的cache
        /// </summary>
        int m_GetMsg_CacheLenght = 0;

        /// <summary>
        /// 上次未接收完成的数据缓存
        /// </summary>
        byte[] m_GetMsg_Cache = new byte[256 * 1024];
        #endregion

        #region 事件定义

        /// <summary>
        /// 接受到数据包
        /// </summary>
        public event ESFT.Common.TypeDefinitions.ReceiveCommandPacket EventReceiveCommandPacket;

        protected virtual void OnReceiveCommandPacket(object iAsyncUserToken, MsgCommand iMsg)
        {
            if (this.EventReceiveCommandPacket != null)
            {
                this.EventReceiveCommandPacket(iAsyncUserToken, iMsg);
            }
        }

        /// <summary>
        /// 新增传输任务
        /// </summary>
        public event ESFT.Common.TypeDefinitions.AddTransferTask EventAddTransferTask;

        /// <summary>
        /// 传输任务进度发生改变
        /// </summary>
        public event ESFT.Common.TypeDefinitions.TransferTaskProgressChange EventTransferTaskProgressChange;

        protected virtual void OnAddTransferTask(FileTransferBase iTask)
        {
            if (this.EventAddTransferTask != null)
            {
                this.EventAddTransferTask(iTask);
            }
        }

        protected virtual void OnTransferTaskProgressChange(FileTransferBase iTask)
        {
            if (this.EventTransferTaskProgressChange != null)
            {
                this.EventTransferTaskProgressChange(iTask);
            }
        }
        #endregion

        /// <summary>
        /// 服务器收到数据事件，由监听程序触发
        /// </summary>
        /// <param name="iReceiveData"></param>
        public void ReceiveData(byte[] iReceiveData, EndPoint iClientEndPoint)
        {
            //try
            //{
            this.mLastPacketTime = DateTime.Now;
            this.mClientEndPoint = iClientEndPoint;
            this.GetPacket(iReceiveData);
            //}
            //catch (Exception ex)
            //{
            //    Error(ex);
            //}
        }

        #region 组包
        /// <summary>
        /// 解析服务器接收的文件BYTE[]数据
        /// </summary>
        /// <param name="iReceiveData"></param>
        public void GetPacket(byte[] iReceiveData)
        {
            if (iReceiveData != null && iReceiveData.Length > 0)
            {
                if (this.m_GetMsg_Success == true)
                {
                    ////重新接收一个数据包
                    int usedLenght = 0; ////已经读取了多少长度的数据
                    int packetLenght = this.GetPacketLenght(iReceiveData, ref usedLenght);

                    if (packetLenght == -1)
                    {
                        return;
                    }
                    else
                    {
                        this.GetReceiveMsg(packetLenght, iReceiveData, usedLenght);
                    }
                }
                else
                {
                    ////继续上次的数据接收数据包
                    this.GetReceiveMsg(this.m_GetMsg_PackeLenght, iReceiveData, 0);
                }
            }
        }

        /// <summary>
        /// 接收一个包的长度
        /// </summary>
        /// <param name="iReceuveData"></param>
        /// <returns></returns>
        int GetPacketLenght(byte[] iReceuveData, ref int iUsedLenght)
        {
            if (this.m_GetPacketLenght_Success == true)
            {
                ////重新接收一个新的packet长度
                if (iReceuveData.Length >= 4)
                {
                    ////本次读取的数据够一个int
                    int packetLenght = BitConverter.ToInt32(iReceuveData, 0);

                    iUsedLenght = 4;
                    if (packetLenght > 0 && packetLenght < 65535)
                    {
                        return packetLenght;
                    }
                    else
                    {
                        // 如果获取到的包长度在非正常范围内，有可能是数据垃圾，不处理。
                        return -1;
                    }
                }
                else
                {
                    ////本次读取的数据不够一个int，将本次的数据存储，等待后面的数据
                    iReceuveData.CopyTo(this.m_GetPacketLenght_CacheBytes, 0);
                    this.m_GetPacketLenght_CacheLenght = iReceuveData.Length;
                    this.m_GetPacketLenght_Success = false;

                    return -1;
                }
            }
            else
            {
                ////继续接收Packet长度
                if (iReceuveData.Length >= 4 - this.m_GetPacketLenght_CacheLenght)
                {
                    ////本次接收的数据+上次接收的数据够一个int
                    Array.Copy(iReceuveData, 0, this.m_GetPacketLenght_CacheBytes, this.m_GetPacketLenght_CacheLenght, 4 - this.m_GetPacketLenght_CacheLenght);
                    int packetLenght = BitConverter.ToInt32(this.m_GetPacketLenght_CacheBytes, 0);
                    Array.Copy(iReceuveData, 4 - this.m_GetPacketLenght_CacheLenght, iReceuveData, 4 - this.m_GetPacketLenght_CacheLenght, iReceuveData.Length - 4 + this.m_GetPacketLenght_CacheLenght);
                    this.m_GetPacketLenght_Success = true;
                    iUsedLenght = 4 - this.m_GetPacketLenght_CacheLenght;
                    return packetLenght;
                }
                else
                {
                    ////本次接收的数据+上次接收的数据还是不够一个int
                    iReceuveData.CopyTo(this.m_GetPacketLenght_CacheBytes, this.m_GetPacketLenght_CacheLenght);
                    this.m_GetPacketLenght_CacheLenght += iReceuveData.Length;

                    return -1;
                }
            }
        }

        /// <summary>
        /// 接收一个msg包
        /// </summary>
        /// <param name="iPacketLenght"></param>
        /// <param name="iReceiveData"></param>
        /// <returns></returns>
        EsftMsg GetReceiveMsg(int iPacketLenght, byte[] iReceiveData, int iUsedLenght)
        {
            EsftMsg msg = null;

            if (this.m_GetMsg_Success == true)
            {
                ////重新接收一个新的数据包
                if (iPacketLenght == iReceiveData.Length - iUsedLenght)
                {
                    ////正好够一个包
                    msg = EMessage.DeserializationPacket(iReceiveData, iUsedLenght);

                    ////立即处理这个包
                    OnReceivePacket(msg);
                    this.m_GetMsg_CacheLenght = 0;
                    this.m_GetMsg_Success = true;
                }
                else if (iPacketLenght < iReceiveData.Length - iUsedLenght)
                {
                    ////本次接收数据包含最低2个包的数据
                    byte[] currentPacketBytes = new byte[iPacketLenght];
                    Array.Copy(iReceiveData, iUsedLenght, currentPacketBytes, 0, currentPacketBytes.Length);
                    msg = EMessage.DeserializationPacket(currentPacketBytes, 0);

                    ////立即处理这个包
                    OnReceivePacket(msg);
                    this.m_GetMsg_CacheLenght = 0;

                    ////剩余的包数据
                    byte[] surplusReveiceData = new byte[iReceiveData.Length - iPacketLenght - iUsedLenght];
                    Array.Copy(iReceiveData, iUsedLenght + iPacketLenght, surplusReveiceData, 0, surplusReveiceData.Length);
                    this.m_GetMsg_Success = true;
                    this.GetPacket(surplusReveiceData);  ////继续处理剩下的数据
                }
                else
                {
                    ////本次接收的数据不够一个数据包
                    this.m_GetMsg_Success = false;
                    this.m_GetMsg_CacheLenght = iReceiveData.Length - iUsedLenght;
                    this.m_GetMsg_PackeLenght = iPacketLenght;
                    Array.Copy(iReceiveData, iUsedLenght, this.m_GetMsg_Cache, 0, iReceiveData.Length - iUsedLenght);
                }
            }
            else
            {
                if (this.m_GetMsg_PackeLenght == iReceiveData.Length + this.m_GetMsg_CacheLenght)
                {
                    ////至少本次数据包+上次缓存的数据包够一个msg
                    byte[] currentMsgData = new byte[this.m_GetMsg_PackeLenght];
                    Array.Copy(this.m_GetMsg_Cache, 0, currentMsgData, 0, this.m_GetMsg_CacheLenght);
                    Array.Copy(iReceiveData, 0, currentMsgData, this.m_GetMsg_CacheLenght, this.m_GetMsg_PackeLenght - this.m_GetMsg_CacheLenght);
                    msg = EMessage.DeserializationPacket(currentMsgData, iUsedLenght);

                    ////立即处理这个包
                    OnReceivePacket(msg);
                    this.m_GetMsg_CacheLenght = 0;
                    this.m_GetMsg_Success = true;
                }
                else if (this.m_GetMsg_PackeLenght < iReceiveData.Length + this.m_GetMsg_CacheLenght)
                {
                    byte[] currentMsgData = new byte[this.m_GetMsg_PackeLenght];
                    Array.Copy(this.m_GetMsg_Cache, 0, currentMsgData, 0, this.m_GetMsg_CacheLenght);
                    Array.Copy(iReceiveData, 0, currentMsgData, this.m_GetMsg_CacheLenght, this.m_GetMsg_PackeLenght - this.m_GetMsg_CacheLenght);
                    msg = EMessage.DeserializationPacket(currentMsgData, iUsedLenght);

                    ////立即处理这个包
                    OnReceivePacket(msg);
                    this.m_GetMsg_Success = true;

                    int surplusLenght = iReceiveData.Length - this.m_GetMsg_PackeLenght + this.m_GetMsg_CacheLenght;
                    byte[] surplusReveiceData = new byte[surplusLenght];
                    Array.Copy(iReceiveData, this.m_GetMsg_PackeLenght - this.m_GetMsg_CacheLenght, surplusReveiceData, 0, surplusLenght);
                    this.m_GetMsg_CacheLenght = 0;
                    this.GetPacket(surplusReveiceData);  ////继续处理剩下的数据
                }
                else
                {
                    ////上次缓存的数据+本次传输的数据 仍然不够一个包
                    this.m_GetMsg_Success = false;
                    Array.Copy(iReceiveData, 0, this.m_GetMsg_Cache, this.m_GetMsg_CacheLenght, iReceiveData.Length);
                    this.m_GetMsg_CacheLenght += iReceiveData.Length;
                }
            }

            return msg;
        }
        #endregion

        /// <summary>
        /// 接收到一个完成的包数据
        /// </summary>
        /// <param name="iMsg"></param>
        public void OnReceivePacket(EsftMsg iMsg)
        {
            //////////////////////////////////////////
            // 如果mClientTransferType为None，证明当前连接是新开连接，
            // 记录当前传输类型，并根据传输类型创建对应的mFileTransferHandler实例
            if (iMsg is MsgCommand && iMsg.MsgType == EMessageType.M_Client_SendCommandInfo)
            {
                //服务器收到一个通讯数据包
                OnReceiveCommandPacket(this, iMsg as MsgCommand);
            }

            if (iMsg is MsgCommand
                && iMsg.PacketType == EPackageType.CommandMsg
                && this.mClientTransferType == TransferType.None)
            {
                string c = (iMsg as MsgCommand).Command;
                string key = Guid.NewGuid().ToString();// string.IsNullOrEmpty(c) ? Guid.NewGuid().ToString() : c;

                if (iMsg.MsgType == EMessageType.M_ClientRequestUploadFile)
                {
                    ////客户端请求上传文件
                    this.mClientTransferType = TransferType.UploadFile;
                    this.mFileTransferHandler = new ClientFileUpload(this) { Key = key };
                    this.mFileTransferHandler.EventTransferTaskProgressChange += mFileTransferHandler_EventTransferTaskProgressChange;
                    this.mFileTransferHandler.EventTransferTaskError += mFileTransferHandler_EventTransferTaskError;

                    OnAddTransferTask(this.mFileTransferHandler);
                }
                else if (iMsg.MsgType == EMessageType.M_ClientRequestServerInfo)
                {
                    ////客户端请求服务器信息
                    this.mClientTransferType = TransferType.RequestServerInfo;
                    this.mFileTransferHandler = new ClientRequestServerInfo(this);
                }
                else if (iMsg.MsgType == EMessageType.M_ClientRequestDownloadFile)
                {
                    ///客户端请求下载文件
                    this.mClientTransferType = TransferType.DownloadFile;
                    this.mFileTransferHandler = new ClientFileDownload(this) { Key = key };
                }
                else if (iMsg.MsgType == EMessageType.C_GetServerBaseInfo)
                {
                    ///客户端请求获取服务器可用连接数等基础信息
                    this.mClientTransferType = TransferType.RequestServerTaskInfo;
                    this.mFileTransferHandler = new ClientRequestTransferInfo(this);
                }
                else if (iMsg.MsgType == EMessageType.C_GetServerTransferInfo)
                {
                    ///客户端请求获取服务器正在传输的信息
                    this.mClientTransferType = TransferType.RequestServerTaskInfo;
                    this.mFileTransferHandler = new ClientRequestTransferInfo(this);
                }
                else if (iMsg.MsgType == EMessageType.M_SendServerIPToHostServer)
                {
                    ///分服务器向主服务器发送本机的IP地址
                    this.mClientTransferType = TransferType.ChildServerRegistServerInfo;
                    this.mFileTransferHandler = new ChlidServerSendIp(this);
                }
                else if (iMsg.MsgType == EMessageType.M_ServiceRequestChildInfo)
                {
                    ///service UI向服务器请求子服务器信息
                    this.mClientTransferType = TransferType.ChildServerRegistServerInfo;
                    this.mFileTransferHandler = new ClientRequestTransferInfo(this);
                }
                else
                {
                    ESFT.Common.Log.MyLogManage.Debug("AsyncUserToken.cs", "OnReceivePacket", "未知的连接类型");
                }

            }

            // 如果mFileTransferHandler已经实例化，那么让对应的mFileTransferHandler处理消息
            if (this.mFileTransferHandler != null)
            {
                this.mFileTransferHandler.MessageArrive(iMsg);
            }
        }

        /// <summary>
        /// 客户端上传文件发生错误
        /// </summary>
        /// <param name="ex"></param>
        void mFileTransferHandler_EventTransferTaskError(Exception ex)
        {
            this.m_GetMsg_Success = true;
            this.m_GetPacketLenght_Success = true;
            //this.mClientTransferType = TransferType.None;
            this.SocketClosed();
        }

        /// <summary>
        /// 客户端上传文件传输状态发生改变
        /// </summary>
        /// <param name="iTask"></param>
        void mFileTransferHandler_EventTransferTaskProgressChange(object iTask)
        {
            this.OnTransferTaskProgressChange((FileTransferBase)iTask);
        }

        public void SocketClosed()
        {
            //MyLogManage.Debug(this.GetType(), "SocketClosed", this.mClientEndPoint, "客户端主动断开传输");

            if (this.mFileTransferHandler != null)
            {
                this.mFileTransferHandler.Dispose();

                if (this.mClientTransferType == TransferType.UploadFile)
                {
                    if (this.mFileTransferHandler.State != TransferState.Finish
                       && this.mFileTransferHandler.State != TransferState.VerifyingError)
                    {
                        this.mFileTransferHandler.SetTransferState(TransferState.ClientTimeOut);
                        //保存断点信息
                    }
                }
            }

            this.m_GetMsg_Success = true;
            this.m_GetPacketLenght_Success = true;
            this.mClientTransferType = TransferType.None;
        }

        public void Error(Exception ex)
        {
            log4net.LogManager.GetLogger(this.GetType()).Error(ex.Message, ex);
        }

        #region 给客户端传递消息
        public void SendBytes(byte[] iData)
        {
            try
            {
                this.mSocket.Send(iData);
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }

        public void SendMsg(EsftMsg iMsg)
        {
            try
            {
                byte[] data = EMessage.Serialization(iMsg);
                this.mSocket.Send(data);
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }
        #endregion
    }
}
