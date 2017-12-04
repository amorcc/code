using ESFT.Common.SystemInfo;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFT.Client
{
    /// <summary>
    /// 文件处理
    /// </summary>
    public class FileIO
    {
        #region 属性定义
        
        /// <summary>
        /// 文件传输类型
        /// </summary>
        protected TransferState mTransferState;
        
        /// <summary>
        /// 本地文件路径
        /// </summary>
        protected string mLocalFilePath;

        /// <summary>
        /// 本地文件名
        /// </summary>
        protected string mLocalFileName;

        /// <summary>
        /// 远程文件路径
        /// </summary>
        protected string mRemoteFilePath;

        /// <summary>
        /// 远程文件名
        /// </summary>
        protected string mRemoteFileName;

        /// <summary>
        /// 文件大小
        /// </summary>
        protected long mFileLenght;

        /// <summary>
        /// 接收或者读取的文件大小
        /// </summary>
        protected long mReceiveOrReadFileLenght;

        /// <summary>
        /// 已经完成的文件大小
        /// </summary>
        protected long mCompletedFileLenght;

        /// <summary>
        /// 文件的参数信息
        /// </summary>
        protected ESFTParameter[] mParameters;

        /// <summary>
        /// 文件扩展名
        /// </summary>
        protected string mExtension;

        /// <summary>
        /// 远程文件的MD5值
        /// </summary>
        protected string mRemoteFileMD5;

        /// <summary>
        /// 最后一次发送文件的时间
        /// </summary>
        protected DateTime mLastPacketTime;

        /// <summary>
        /// 开始的传输时间
        /// </summary>
        protected DateTime mStartTime;

        /// <summary>
        /// 结束传输时间
        /// </summary>
        protected DateTime mEndTime;

        /// <summary>
        /// 文件块缓冲区队列
        /// </summary>
        public Queue<MsgFileBlock> mQueue;

        /// <summary>
        /// 文件块容缓冲区的最大长度
        /// </summary>
        protected int mQueueMaxSize = 50;

        protected System.IO.FileStream mFileStream = null;

        #endregion

        #region 属性封装

        /// <summary>
        /// 远程服务器的文件路径 
        /// </summary>
        public string RemoteFilePath
        {
            get
            {
                return this.mRemoteFilePath;
            }
            set
            {
                this.mRemoteFilePath = value;
            }
        }

        /// <summary>
        /// 远程服务器的文件名
        /// </summary>
        public string RemoteFileName
        {
            get
            {
                return this.mRemoteFileName;
            }
            set
            {
                this.mRemoteFileName = value;
            }
        }

        /// <summary>
        /// 返回本地文件路径
        /// </summary>
        public string LocalFileFullName
        {
            get
            {
                //string localfullname = SystemInfo.ServerSaveFilePath + this.mLocalFilePath + "/" + this.mLocalFileName;
                //localfullname = System.IO.Path.GetFullPath(localfullname);
                return GetLocalFullName();
            }
        }

        public virtual string GetLocalFullName()
        {
            string localfullname = SystemInfo.ServerSaveFilePath + this.mLocalFilePath + "/" + this.mLocalFileName;
            localfullname = System.IO.Path.GetFullPath(localfullname);
            return localfullname;
        }

        /// <summary>
        /// 传输的参数信息
        /// </summary>
        public ESFTParameter[] Parameters
        {
            get
            {
                return this.mParameters;
            }
            set
            {
                this.mParameters = value;
            }
        }

        /// <summary>
        /// 远程文件的MD5值
        /// </summary>
        public string RemoteFileMD5
        {
            get { return this.mRemoteFileMD5; }
        }

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileLenght
        {
            get
            {
                return this.mFileLenght;
            }
        }

        /// <summary>
        /// 已经接收的数据长度
        /// </summary>
        public long ReceiveOrReadFileLenght
        {
            get
            {
                return this.mReceiveOrReadFileLenght;
            }
            //set
            //{
            //    this.mReceiveOrReadFileLenght = value;
            //}
        }

        /// <summary>
        /// 已经完成的大小（字节）
        /// </summary>
        public long CompletedFileLenght
        {
            get
            {
                return this.mCompletedFileLenght;
            }
            set
            {
                this.mCompletedFileLenght = value;
            }
        }

        /// <summary>
        /// 最后包到达时间
        /// </summary>
        public DateTime LastPacketTime
        {
            get
            {
                return this.mLastPacketTime;
            }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this.mStartTime;
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return this.mEndTime;
            }
            set
            {
                this.mEndTime = value;
            }
        }

        /// <summary>
        /// 返回传输时间
        /// </summary>
        public TimeSpan TransferTime
        {
            get
            {
                if (this.mTransferState != TransferState.Finish)
                {
                    return DateTime.Now - this.mStartTime;
                }
                else if (this.mTransferState == TransferState.Error
                            || this.mTransferState == TransferState.VerifyingError)
                {
                    return this.EndTime - this.StartTime;
                }
                else
                {
                    return this.EndTime - this.StartTime;
                }
            }
        }

        /// <summary>
        /// 传输状态
        /// </summary>
        public TransferState State
        {
            get
            {
                return this.mTransferState;
            }
            set
            {
                this.mTransferState = value;
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorInfo
        {
            get;
            set;
        }

        public double Speed
        {
            get
            {
                double time = this.TransferTime.TotalSeconds;
                double completeLenght = Convert.ToDouble(this.mCompletedFileLenght);
                if (time > 0 && completeLenght > 0)
                {
                    return completeLenght / time;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string Extension
        {
            get
            {
                return this.mExtension;
            }
        }

        /// <summary>
        /// 客户端要求的本地相对路径
        /// </summary>
        public string LocalPath
        {
            get
            {
                return this.mLocalFilePath;
            }
        }

        public string LocalFileName
        {
            get
            {
                return this.mLocalFileName;
            }
        }
        #endregion

        public FileIO()
        {
        }

        /// <summary>
        /// 释放文件读写控制 
        /// </summary>
        public virtual void ReleaseFileHandler()
        {
        }
    }
}

