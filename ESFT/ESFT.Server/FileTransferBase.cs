using ESFT.Common.SystemInfo;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;

namespace ESFT.Server
{
    /// <summary>
    /// 文件传输的抽象类
    /// 该类有2个子类继承，2个子类实现具体的文件上传和文件下载功能
    /// 该类中定义接收消息进度反馈等功能
    /// </summary>
    public abstract class FileTransferBase
    {
        #region 属性定义
        public AsyncUserToken mUserToken;

        public TransferType mTransferType;

        /// <summary>
        /// 文件MD5
        /// </summary>
        protected string mFileMD5;

        /// <summary>
        /// 文件扩展名
        /// </summary>
        protected string mFileExtension;

        /// <summary>
        /// 本地文件路径
        /// </summary>
        protected string mServerFilePath;

        /// <summary>
        /// 本地文件名
        /// </summary>
        public string mServerFileName;

        /// <summary>
        /// 客户端文件路径
        /// </summary>
        protected string mClientFilePath;

        /// <summary>
        /// 客户端文件名
        /// </summary>
        protected string mClientFileName;

        /// <summary>
        /// 客户端传递的文件参数信息
        /// </summary>
        protected ESFTParameter[] mParameter = null;

        /// <summary>
        /// 文件长度
        /// </summary>
        protected long mFileLenght;

        /// <summary>
        /// 当前处理长度
        /// </summary>
        protected long mCurrentLenght;

        /// <summary>
        /// 传输状态
        /// </summary>
        protected TransferState mTransferState;

        /// <summary>
        /// 开始时间
        /// </summary>
        protected DateTime? mBeginTime;

        /// <summary>
        /// 结束时间
        /// </summary>
        protected DateTime? mEndTime;

        /// <summary>
        /// 最后接收数据包时间
        /// </summary>
        protected DateTime? mLastReceivePacketTime;
        #endregion

        #region 属性封装

        /// <summary>
        /// 传输状态
        /// </summary>
        public TransferState State
        {
            get
            {
                return this.mTransferState;
            }
        }

        /// <summary>
        /// 传输状态字符串信息
        /// </summary>
        public string StateStr
        {
            get
            {
                string stateStr = string.Empty;

                switch (this.mTransferState)
                {
                    case TransferState.Connecting:
                        stateStr = "正在连接";
                        break;
                    case TransferState.GetParameter:
                        stateStr = "正在获取文件参数信息";
                        break;
                    case TransferState.GetFileInfo:
                        stateStr = "正在获取文件信息";
                        break;
                    case TransferState.Transferring:
                        stateStr = "正在传输";
                        break;
                    case TransferState.VerifyingFile:
                        stateStr = "正在验证文件有效性";
                        break;
                    case TransferState.Finish:
                        stateStr = "传输完成";
                        break;
                    case TransferState.ClientDisconnectInitiative:
                        stateStr = "客户端主动断开";
                        break;
                    case TransferState.CreateMD5Hash:
                        stateStr = "正在获取文件MD5值";
                        break;
                    case TransferState.ReceiveFinish:
                        stateStr = "文件接收完成";
                        break;
                    case TransferState.VerifyingError:
                        stateStr = "传输完成但验证错误";
                        break;
                    case TransferState.Error:
                        stateStr = "发生错误";
                        break;
                    case TransferState.ClientTimeOut:
                        stateStr = "客户端超时被回收";
                        break;
                    default:
                        stateStr = "Unknown";
                        break;
                }

                return stateStr;
            }
        }

        /// <summary>
        /// 返回服务器文件全路径
        /// </summary>
        public string ServerFileFullName
        {
            get
            {
                string t = SystemInfo.ServerSaveFilePath + "\\" + this.mServerFilePath + "/" + this.mServerFileName;
                t = System.IO.Path.GetFullPath(t);
                return t;
            }
        }

        /// <summary>
        /// 返回客户端文件路径
        /// </summary>
        public string ClientFileFullName
        {
            get
            {
                return this.mClientFilePath + "\\" + this.mClientFileName;
            }
        }

        /// <summary>
        /// 本次传输的GUID编号
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        public string FileMD5
        {
            get
            {
                return this.mFileMD5;
            }
        }

        /// <summary>
        /// 文件长度
        /// </summary>
        public long FileLenght
        {
            get
            {
                return this.mFileLenght;
            }
        }

        public long CurrentReceviceOrSendLenght
        {
            get
            {
                return this.GetCurrentReceviceOrSendLenght();
            }
        }

        /// <summary>
        /// 当前已经完成的长度
        /// </summary>
        public long CurrentLenght
        {
            get
            {
                return this.mCurrentLenght;
            }
        }

        /// <summary>
        /// 返回已经传输了多少秒
        /// </summary>
        public double TransferTimeSecond
        {
            get
            {
                return this.GetTransferTimeSecond();
            }
        }

        /// <summary>
        /// 返回传输速度
        /// </summary>
        public double TransferSpeed
        {
            get
            {
                return this.GetTranserSpeed();
            }
        }

        /// <summary>
        /// 最后一个数据包到达时间
        /// </summary>
        public DateTime? LastReceivePacketTime
        {
            get
            {
                return this.mLastReceivePacketTime;
            }
        }

        public DateTime? BeginTime
        {
            get
            {
                return this.mBeginTime;
            }
        }

        public string FileExtension
        {
            get
            {
                return this.mFileExtension;
            }
        }
        #endregion

        #region 事件定义
        /// <summary>
        /// 传输任务进度发生改变
        /// </summary>
        public event ESFT.Common.TypeDefinitions.TransferTaskProgressChange EventTransferTaskProgressChange;

        protected void OnTransferTaskProgressChange(FileTransferBase iTask)
        {
            if (this.EventTransferTaskProgressChange != null)
            {
                this.EventTransferTaskProgressChange(iTask);
            }
        }

        public event ESFT.Common.TypeDefinitions.TransferTaskError EventTransferTaskError;

        protected void OnTransferTaskError(Exception ex)
        {
            if (this.EventTransferTaskError != null)
            {
                this.EventTransferTaskError.BeginInvoke(ex, null, null);
            }
        }
        #endregion

        /// <summary>
        /// 客户端消息到达
        /// </summary>
        public void MessageArrive(EsftMsg iMsg)
        {
            this.mLastReceivePacketTime = DateTime.Now;
            if (iMsg is MsgCommand && iMsg.PacketType == EPackageType.CommandMsg)
            {
                this.HandleCommandMsg((MsgCommand)iMsg);
            }
            else if (iMsg is MsgFileInfo && iMsg.PacketType == EPackageType.FileInfoMsg)
            {
                this.HandleFileInfoMsg((MsgFileInfo)iMsg);
            }
            else if (iMsg is MsgFileBlock && iMsg.PacketType == EPackageType.FileBlockMsg)
            {
                this.HandleFileBlockMsg((MsgFileBlock)iMsg);
            }
            else if (iMsg is MsgParameter && iMsg.PacketType == EPackageType.ParameterMsg)
            {
                this.HandleParameterMsg((MsgParameter)iMsg);
            }
        }

        #region 处理消息
        protected abstract void HandleCommandMsg(MsgCommand iMsg);

        protected abstract void HandleFileInfoMsg(MsgFileInfo iMsg);

        protected abstract void HandleParameterMsg(MsgParameter iMsg);

        protected abstract void HandleFileBlockMsg(MsgFileBlock iMsg);
        #endregion

        public virtual void SetTransferState(TransferState iTransferState)
        {
            this.mTransferState = iTransferState;
            this.OnTransferTaskProgressChange(this);
        }

        public virtual long GetCurrentReceviceOrSendLenght()
        {
            return 0;
        }

        /// <summary>
        /// 返回已经传输了多少秒
        /// </summary>
        /// <returns></returns>
        public virtual double GetTransferTimeSecond()
        {
            return 0;
        }

        public virtual double GetTranserSpeed()
        {
            double timeSecond = this.GetTransferTimeSecond();

            if (this.mTransferState == TransferState.Finish)
            {
            }

            if (timeSecond == 0 || timeSecond == -1 || this.CurrentLenght == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(this.CurrentLenght / timeSecond);
            }
        }

        public virtual void Dispose()
        {
        }

        public virtual void Error(Exception ex)
        {
            log4net.LogManager.GetLogger(this.GetType()).Error(ex.ToString());
            this.OnTransferTaskError(ex);
        }

        #region 给客户端传递消息
        protected void SendBytes(byte[] iData)
        {
            if (this.mUserToken != null)
            {
                this.mUserToken.SendBytes(iData);
            }
        }

        protected void SendMsg(EsftMsg iMsg)
        {
            if (this.mUserToken != null)
            {
                this.mUserToken.SendMsg(iMsg);
            }
        }
        #endregion
    }
}
