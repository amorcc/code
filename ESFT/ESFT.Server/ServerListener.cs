using ESFT.Server.ViceServer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ESFT.Common.Log;
using System.Collections;
using ESFT.Message;
using ESFT.Common.TypeDefinitions;
using ESFT.Common.SystemInfo;

namespace ESFT.Server
{
    public class ServerListener : ESFT.Common.MyDisposable
    {
        /// <summary>
        /// 最大并发量
        /// </summary>
        int mNumConnections;

        /// <summary>
        /// 接收数据的Buffer大小
        /// </summary>
        int mReceiveBufferSize;

        /// <summary>
        /// Buffer管理类
        /// </summary>
        BufferManager mBufferManage;

        /// <summary>
        /// 连接到服务器的总数
        /// </summary>
        public int mNumConnectedSocketClients;

        /// <summary>
        /// 主侦听socket挂起连接队列的最大长度
        /// </summary>
        int mBacklog = 1000;

        /// <summary>
        /// 主监听socket
        /// </summary>
        Socket mListenerSocket;

        /// <summary>
        /// SocketAsyncEventArgs池
        /// </summary>
        public SocketAsyncEventArgsPool mSocketAsyncPool;

        Semaphore mMaxNumAcceptedClients;

        public static ServerListener mThisListener;

        /// <summary>
        /// 定时关闭未使用的Socket
        /// </summary>
        System.Timers.Timer mCloseSocketTimer = null;

        /// <summary>
        /// 定时通知主服务器本机的IP地址
        /// </summary>
        System.Timers.Timer mSendIpToHostServerTimer = null;

        /// <summary>
        /// 超时毫秒数设置，超过这个时间未响应的连接，将被回收
        /// </summary>
        double mTimeOutSecond = 60 * 1000;

        /// <summary>
        /// 所有传输任务
        /// </summary>
        //public static Dictionary<string, FileTransferBase> mTransferTask = new Dictionary<string, FileTransferBase>();
        public static Hashtable mTransferTask = new Hashtable();


        #region 定义事件
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
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iNumConnections">最大并发量(1000以上)</param>
        /// <param name="iReceiveBufferSize">每个连接的缓冲区大小（1024*32以上）</param>
        public ServerListener(int iNumConnections, int iReceiveBufferSIze)
        {
            this.mNumConnections = iNumConnections;
            this.mReceiveBufferSize = iReceiveBufferSIze;
            this.mBufferManage = new BufferManager(iReceiveBufferSIze * iNumConnections * 2, iReceiveBufferSIze);
            this.mSocketAsyncPool = new SocketAsyncEventArgsPool(this.mNumConnections);
            this.mMaxNumAcceptedClients = new Semaphore(this.mNumConnections, this.mNumConnections);

            mThisListener = this;

            //this.mCloseSocketTimer = new System.Timers.Timer();
            //this.mCloseSocketTimer.Enabled = true;
            //this.mCloseSocketTimer.Interval = mTimeOutSecond;
            //this.mCloseSocketTimer.Elapsed += mCloseSocketTimer_Tick;
            //this.mCloseSocketTimer.Start();

            this.mSendIpToHostServerTimer = new System.Timers.Timer();
            this.mSendIpToHostServerTimer.Enabled = false;
            this.mSendIpToHostServerTimer.Interval = 3000;//10 * 60 * 1000;//十分钟通知一次
            this.mSendIpToHostServerTimer.Elapsed += mSendIpToHostServer_Elapsed;
            //this.mSendIpToHostServerTimer.Start();
        }

        public void Init()
        {
            this.mBufferManage.InitBuffer();

            //创建m_NumConnections个SocketAsyncEventArgs对象，并放到m_SocketAsynPool缓冲池中
            SocketAsyncEventArgs saea;

            for (int i = 0; i < this.mNumConnections; i++)
            {
                saea = new SocketAsyncEventArgs();
                saea.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                AsyncUserToken userToken = new AsyncUserToken();
                //userToken.Evnet_ServerNewReceive += userToken_Evnet_ServerNewReceive;
                userToken.EventAddTransferTask += userToken_EventAddTransferTask;
                userToken.EventTransferTaskProgressChange += userToken_EventTransferTaskProgressChange;
                userToken.EventReceiveCommandPacket += userToken_EventReceiveCommandPacket;
                saea.UserToken = userToken;
                userToken.mId = i.ToString();
                userToken.mSocketAsyncEventArgs = saea;

                //分配一个缓冲区给SocketAsyncEventArg对象
                mBufferManage.SetBuffer(saea);

                this.mSocketAsyncPool.Push(saea);
            }
        }

        void userToken_EventReceiveCommandPacket(object iAsyncUserToken, MsgCommand iMsg)
        {
            this.OnReceiveCommandPacket(iAsyncUserToken, iMsg);
        }



        void userToken_EventTransferTaskProgressChange(object iTask)
        {
            this.OnTransferTaskProgressChange((FileTransferBase)iTask);
        }

        void userToken_EventAddTransferTask(object iTask)
        {
            mTransferTask.Add(((FileTransferBase)iTask).Key, (FileTransferBase)iTask);
            this.OnAddTransferTask((FileTransferBase)iTask);
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="iLocalEndPoint">ip和端口号</param>
        public void Start(object iLocalEndPoint)
        {
            IPEndPoint localEndPoint = (IPEndPoint)iLocalEndPoint;
            this.mListenerSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.mListenerSocket.Bind(localEndPoint);
            this.mListenerSocket.Listen(this.mBacklog);

            StartAccept(null);
        }

        public void Stop()
        {
            if (this.mListenerSocket != null)
            {
                this.mListenerSocket.Close();
                this.mListenerSocket.Dispose();
                this.mListenerSocket = null;
            }
        }

        void StartAccept(SocketAsyncEventArgs iAcceptEnentArg)
        {
            if (iAcceptEnentArg == null)
            {
                iAcceptEnentArg = new SocketAsyncEventArgs();
                iAcceptEnentArg.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_Completed);
            }
            else
            {
                //此AcceptEventArgs为重用的，清除掉上次的内容
                iAcceptEnentArg.AcceptSocket = null;
            }

            mMaxNumAcceptedClients.WaitOne();

            if (this.mListenerSocket != null)
            {
                bool willRaiseEvent = this.mListenerSocket.AcceptAsync(iAcceptEnentArg);

                if (!willRaiseEvent)
                {
                    ProcessAccept(iAcceptEnentArg);
                }
            }
        }

        void ProcessAccept(SocketAsyncEventArgs e)
        {
            //连接到服务器的总数增加一个
            Interlocked.Increment(ref this.mNumConnectedSocketClients);

            //从pool中取出一个SocketAsyncEventArg对象，用于这个连接
            SocketAsyncEventArgs readEventArgs = this.mSocketAsyncPool.Pop();
            ((AsyncUserToken)readEventArgs.UserToken).mSocket = e.AcceptSocket;

            if (e.AcceptSocket != null)
            {
                bool willRaiseEvent = e.AcceptSocket.ReceiveAsync(readEventArgs);

                if (!willRaiseEvent)
                {
                    ProcessAccept(readEventArgs);
                }

                StartAccept(e);
            }
        }

        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        /// <summary>
        /// 如果此方法被调用的时候，表明一个异步的接收操作完成
        /// 如果远程主机关闭了连接，那么套接字将被关闭
        /// </summary>
        /// <param name="e"></param>
        void ProcessReceive(SocketAsyncEventArgs e, EndPoint clientEndPoint)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;

            if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success && this.mListenerSocket != null)
            {
                //将接收的数据拷贝到receiveData中
                byte[] receiveData = new byte[e.BytesTransferred];
                Buffer.BlockCopy(e.Buffer, e.Offset, receiveData, 0, e.BytesTransferred);
                //将接收的数据放入AysncUserToken中处理
                token.ReceiveData(receiveData, clientEndPoint);

                if (token.mSocket.Connected)
                {
                    bool willRaiseEvent = token.mSocket.ReceiveAsync(e);
                    if (!willRaiseEvent)
                    {
                        ProcessReceive(e, clientEndPoint);
                    }
                }
                else
                {
                    log4net.LogManager.GetLogger(typeof(ServerListener)).Debug(clientEndPoint.ToString() + " mSocket 已断开");
                }
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            EndPoint clientEndPoint = null;
            try
            {
                clientEndPoint = ((Socket)sender).RemoteEndPoint;
            }
            catch
            {
            }
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e, clientEndPoint);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        /// <summary>
        /// 异步发送操作完成时，调用此方法
        /// 另一个接收的方法问题在Socket读取任何数据从客户端发送
        /// The method issues another receive on the socket to read any additional 
        // data sent from the client
        /// </summary>
        /// <param name="e"></param>
        void ProcessSend(SocketAsyncEventArgs e)
        {
            MyLogManage.Debug("ServerListener", "ProcessSend", "");
            if (e.SocketError == SocketError.Success)
            {
                AsyncUserToken token = (AsyncUserToken)e.UserToken;

                bool willRaiseEvent = token.mSocket.ReceiveAsync(e);
                if (!willRaiseEvent)
                {
                    ProcessReceive(e, null);
                }
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="e"></param>
        public void CloseClientSocket(SocketAsyncEventArgs e)
        {
            AsyncUserToken token = (AsyncUserToken)e.UserToken;
            token.SocketClosed();
            try
            {
                if (e.LastOperation == SocketAsyncOperation.Send)
                {
                    token.mSocket.Shutdown(SocketShutdown.Send);
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Debug(ex.ToString());
            }

            token.mSocket.Close();

            if (this.mSocketAsyncPool.Contains(e) == false)
            {
                Interlocked.Decrement(ref this.mNumConnectedSocketClients);

                this.mSocketAsyncPool.Push(e);
                this.mMaxNumAcceptedClients.Release();
            }
        }

        ///// <summary>
        ///// 添加一个副服务器信息
        ///// </summary>
        ///// <param name="iViceInfo"></param>
        //public static void AddViceServerInfo(ViceServerInfo iViceInfo)
        //{
        //    if (m_ViceServerInfo.ContainsKey(iViceInfo.ServerIP))
        //    {
        //        m_ViceServerInfo[iViceInfo.ServerIP].Port = iViceInfo.Port;
        //        m_ViceServerInfo[iViceInfo.ServerIP].ServerNetworkCardNumber = iViceInfo.ServerNetworkCardNumber;
        //        m_ViceServerInfo[iViceInfo.ServerIP].ViceServerName = iViceInfo.ViceServerName;
        //        m_ViceServerInfo[iViceInfo.ServerIP].UpdateTime = iViceInfo.UpdateTime;
        //    }
        //    else
        //    {
        //        m_ViceServerInfo.Add(iViceInfo.ServerIP, iViceInfo);
        //    }
        //}

        /// <summary>
        /// 给客户端分配服务器
        /// </summary>
        /// <returns></returns>
        public static ViceServerInfo AllocationServer()
        {
            return null;
        }

        ///// <summary>
        ///// 给客户端分配服务器
        ///// </summary>
        ///// <returns></returns>
        //public static ViceServerInfo AllocationServer()
        //{
        //    if (m_ViceServerInfo == null
        //        || m_ViceServerInfo.Count == 0)
        //    {
        //        //没有副服务器
        //        return null;
        //    }
        //    else
        //    {
        //        lock (ServerListener.m_ViceServerInfo)
        //        {
        //            foreach (KeyValuePair<string, ViceServerInfo> item in ServerListener.m_ViceServerInfo)
        //            {
        //                return item.Value;
        //            }
        //        }
        //        return null;
        //    }
        //}

        /// <summary>
        /// 心跳处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mCloseSocketTimer_Tick(object sender, EventArgs e)
        {
            int socketnullnum = 0;
            int errornum = 0;
            this.mCloseSocketTimer.Stop();
            log4net.LogManager.GetLogger(typeof(ServerListener)).Info("心跳处理:  " + this.mSocketAsyncPool.Count + "/" + this.mSocketAsyncPool.UsedCount);

            try
            {
                for (int i = this.mSocketAsyncPool.mUsedPool.Count - 1; i >= 0; i--)
                {
                    SocketAsyncEventArgs item = this.mSocketAsyncPool.GetUsedPoolByIndex(i);
                    AsyncUserToken userToken = (AsyncUserToken)item.UserToken;

                    DateTime? lastTime = userToken.mLastPacketTime;

                    TimeSpan? ts = DateTime.Now - lastTime;
                    if ((ts.HasValue != false && ts.Value.TotalMilliseconds >= mTimeOutSecond)
                        || lastTime == null)
                    {
                        try
                        {
                            item.Completed -= new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                            if (userToken.mSocket != null)
                            {
                                userToken.mSocket.Close();
                            }
                            else
                            {
                                socketnullnum++;
                            }
                            userToken.SocketClosed();

                            if (this.mSocketAsyncPool.Contains(item) == false)
                            {
                                Interlocked.Decrement(ref this.mNumConnectedSocketClients);

                                this.mSocketAsyncPool.Push(item);
                                this.mMaxNumAcceptedClients.Release();
                                item.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                            }
                        }
                        catch (Exception ex)
                        {
                            errornum++;
                            log4net.LogManager.GetLogger(this.GetType()).Error("心跳出现错误：" + ex.ToString(), ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error("心跳出现错误：" + ex.ToString(), ex);
            }
            finally
            {
                log4net.LogManager.GetLogger(this.GetType()).Info("心跳处理结束：" + errornum);
                this.mCloseSocketTimer.Enabled = true;
                this.mCloseSocketTimer.Start();
            }
        }

        /// <summary>
        /// 定时通知主服务器本机信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mSendIpToHostServer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.mSendIpToHostServerTimer.Stop();
            try
            {
                IPHostEntry localhostIP = Dns.GetHostEntry(Dns.GetHostName());
                string cmdStr = SystemInfo.m_ServerName + ";" + SystemInfo.m_LocalPort + ";" + GetLocalIp();

                MsgCommand sendIpCommand = new MsgCommand(EMessageType.M_SendServerIPToHostServer, cmdStr);

                // 通知主服务器我的信息
                if (SystemInfo.m_MasterServerIP != null && SystemInfo.m_MasterServerIP.Length > 0 && !(SystemInfo.m_MasterServerIP == SystemInfo.m_LocalIP && SystemInfo.m_MasterServerIP == SystemInfo.m_LocalIP))
                {
                    ChildServerSendIpToHostServer sendMsg = new ChildServerSendIpToHostServer(SystemInfo.m_MasterServerIP, SystemInfo.m_MasterPort);
                    sendMsg.SendIpToHostServer(sendIpCommand);
                }

                // 通知自己我的信息
                ChildServerSendIpToHostServer sendMsg2 = new ChildServerSendIpToHostServer(SystemInfo.m_LocalIP, SystemInfo.m_LocalPort);
                sendMsg2.SendIpToHostServer(sendIpCommand);

                this.mSendIpToHostServerTimer.Interval = 3 * 1000;

                // 查询是否有超时的信息
                double timeOutMinuter = Convert.ToDouble(this.mSendIpToHostServerTimer.Interval * 2) / 1000 / 60;
                ChlidServerSendIp.RemoveTimeOutChildServer(timeOutMinuter);

            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType().ToString()).Debug(ex.ToString());
            }
            finally
            {
                this.mSendIpToHostServerTimer.Enabled = true;
                this.mSendIpToHostServerTimer.Start();
            }
        }

        /// <summary>
        /// 获取本机IP地址
        /// </summary>
        /// <returns></returns>
        string GetLocalIp()
        {
            string ip = string.Empty;
            IPHostEntry localhostIP = Dns.GetHostEntry(Dns.GetHostName());

            for (int i = 0; i < localhostIP.AddressList.Length; i++)
            {
                // 过滤ipv6地址，只获取ipv4地址
                if (localhostIP.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    ip += localhostIP.AddressList[i].ToString();
                    ip += ";";
                }
            }

            return ip;
        }

        protected override void DisposeUnManaged()
        {
            Stop();
            base.DisposeUnManaged();
        }
    }
}
