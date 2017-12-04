using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Collections;
using ESFT.Message;
using ESFT.Common.Log;
using ESFT.Common;

namespace ESFT.Server.FileManage
{
    public sealed class ServerFileWrite : MyDisposable
    {
        #region  Manage
        private static Dictionary<string, List<ServerFileWrite>> mServerFileWrite = new Dictionary<string, List<ServerFileWrite>>();


        /// <summary>
        /// 新建一个FileWrite对象并返回，如果已存在相同MD5的 FileWrite ，则返回空，避免相同文件有多个线程同时接收。
        /// </summary>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        public static ServerFileWrite Add(ClientFileUpload fileUpload)
        {
            lock (mServerFileWrite)
            {
                if (!mServerFileWrite.ContainsKey(fileUpload.FileMD5))
                {
                    mServerFileWrite.Add(fileUpload.FileMD5, new List<ServerFileWrite>());
                    mServerFileWrite[fileUpload.FileMD5].Add(new ServerFileWrite(fileUpload));
                    return mServerFileWrite[fileUpload.FileMD5][0];
                }
                else
                {
                    return null;
                }
            }
        }

        public static int FileWriteCount
        {
            get { return mServerFileWrite.Count; }
        }

        public static string FileWriteInfo
        {
            get
            {
                string t = string.Empty;
                lock (mServerFileWrite)
                {
                    foreach (var v in mServerFileWrite)
                    {
                        t += v.Key + "=" + v.Value.Count + ",";
                    }
                }
                return t;
            }
        }

        protected override void DisposeManaged()
        {
            this.StopAndWait();
            lock (mServerFileWrite)
            {
                if (mServerFileWrite.ContainsKey(this.mFileMD5))
                {
                    mServerFileWrite[this.mFileMD5].Remove(this);
                    if (mServerFileWrite[this.mFileMD5].Count == 0)
                    {
                        mServerFileWrite.Remove(this.mFileMD5);
                    }
                }
            }

            log4net.LogManager.GetLogger(typeof(ServerFileWrite)).Debug("DisposeManaged " + this.mLocalFileFullName);
            base.DisposeManaged();
        }
        #endregion

        string mFileMD5;
        string mLocalFileFullName;

        FileStream mFileStream = null;
        Thread mThread;

        /// <summary>
        /// 实际文件长度
        /// </summary>
        long mFileLenght;

        /// <summary>
        /// 最大队列长度
        /// </summary>
        int mQueueMaxSize = 50;

        /// <summary>
        /// 已经接收文件长度
        /// </summary>
        long mReceiveFileLenght = 0;

        /// <summary>
        /// 已经完成写操作的文件长度
        /// </summary>
        long mCompletedFileLenght = 0;

        /// <summary>
        /// 最后一个包到达时间 
        /// </summary>
        DateTime? mLastPacketTime;

        /// <summary>
        /// 文件块缓冲区队列
        /// </summary>
        private Queue<MsgFileBlock> mQueue;

        /// <summary>
        /// 接收数据信号量
        /// </summary>
        private Semaphore mSemaphoreReveive;

        /// <summary>
        /// 写数据信号量
        /// </summary>
        private Semaphore mSemaphoreWrite;

        /// <summary>
        /// 返回当前已经接收数据长度
        /// </summary>
        public long ReceiveFileLenght
        {
            get
            {
                return this.mReceiveFileLenght;
            }
        }

        #region 写文件完成事件
        /// <summary>
        /// 写文件完成
        /// </summary>
        public event ESFT.Common.TypeDefinitions.FileComplete EventFileComplete;

        private void OnFileComplelte(string iLocalFileFullName)
        {
            if (this.EventFileComplete != null)
            {
                this.EventFileComplete(iLocalFileFullName);
            }
        }
        #endregion

        #region 写文件进度发生改变事件
        /// <summary>
        /// 进度发生改变
        /// </summary>
        public event ESFT.Common.TypeDefinitions.FileProgressChange EventFileProgressChange;

        private void OnFileProgressChange(long iCompleteLength)
        {
            if (this.EventFileProgressChange != null)
            {
                this.EventFileProgressChange(iCompleteLength);
            }
        }
        #endregion

        #region 写文件出错事件定义
        /// <summary>
        /// 写文件出错事件
        /// </summary>
        public event ESFT.Common.TypeDefinitions.FileError EventFileError;

        private void OnFileError(string iLocalFileFullName, Exception ex)
        {
            if (this.EventFileError != null)
            {
                this.EventFileError(iLocalFileFullName, ex);
            }
        }

        /// <summary>
        /// 手动回收ServerFileWrite
        /// </summary>
        public event ESFT.Common.TypeDefinitions.ServerFileWriteManualDispose EventServerFileWriteManualDispose;

        private void OnServerFileWriteManualDispose(object e)
        {
            if (this.EventServerFileWriteManualDispose != null)
            {
                this.EventServerFileWriteManualDispose(e);
            }
        }
        #endregion

        /// <summary>
        /// 请使用 Add 方法
        /// </summary>
        /// <param name="iClientFileUpload"></param>
        private ServerFileWrite(ClientFileUpload iClientFileUpload)
        {
            this.mFileMD5 = iClientFileUpload.FileMD5;
            this.mFileLenght = iClientFileUpload.FileLenght;
            this.mLocalFileFullName = ESFT.Common.SystemInfo.SystemInfo.ServerSaveFilePath + "\\" + iClientFileUpload.mServerFileName;

            long iStartIndex = 0;
            if (System.IO.File.Exists(this.mLocalFileFullName))
            {
                // 如果存在MD5 文件，则表示文件传输已完成，有完整文件，无需再次传输。
                iStartIndex = new FileInfo(this.mLocalFileFullName).Length - 1;
                if (iStartIndex == -1)
                {
                    iStartIndex = 0;
                }
            }
            else if (System.IO.File.Exists(this.mLocalFileFullName + ".tmp"))
            {
                this.mLocalFileFullName += ".tmp";
                // 如果存在MD5 的临时文件，则表示文件传输未完成，从断点开始继续传输。
                iStartIndex = new FileInfo(this.mLocalFileFullName + ".tmp").Length;
            }

            log4net.LogManager.GetLogger(typeof(ServerFileWrite)).Debug("新建" + this.mLocalFileFullName + "写进程");
            this.mCompletedFileLenght = iStartIndex;
            this.mReceiveFileLenght = iStartIndex;

            this.mQueue = new Queue<MsgFileBlock>();
            mSemaphoreReveive = new Semaphore(mQueueMaxSize, mQueueMaxSize);
            mSemaphoreWrite = new Semaphore(0, mQueueMaxSize);

            // 开始运行写进程
            this.mThread = new System.Threading.Thread(StartWrite);
            this.mThread.Priority = ThreadPriority.Lowest;
            this.mThread.Name = "ServerFileWrite Thread:" + this.mLocalFileFullName;
            this.mThread.Start();
        }

        /// <summary>
        /// 接收文件块信息并压入队列
        /// </summary>
        /// <param name="iFileBlockMsg"></param>
        public void ReceiveData(MsgFileBlock iFileBlockMsg)
        {
            if (this.IsStop)
            {
                return;
            }

            this.mSemaphoreReveive.WaitOne();

            this.mQueue.Enqueue(iFileBlockMsg);
            this.mReceiveFileLenght += iFileBlockMsg.FileBlockData.Length;

            if (this.mQueue.Count == this.mQueueMaxSize
                || this.mReceiveFileLenght == this.mFileLenght)
            {
                this.mSemaphoreWrite.Release();
            }

            //if (this.mReceiveFileLenght == this.mFileLenght
            //    && this.mTransferState != TransferState.Finish)
            //{
            //    this.mTransferState = TransferState.ReceiveFinish;
            //}

            this.mLastPacketTime = DateTime.Now;
        }

        private void StopAndWait()
        {
            this.IsStop = true;
            log4net.LogManager.GetLogger(typeof(ServerFileWrite)).Debug("Wait Thread " + this.mThread.Name);
            while (this.mThread != null && this.mThread.IsAlive)
            {
                if (this.mSemaphoreWrite != null)
                {
                    this.mSemaphoreWrite.Release();
                }
                Thread.Sleep(100);
            }

            log4net.LogManager.GetLogger(typeof(ServerFileWrite)).Debug("Stoped Thread " + this.mThread.Name);
        }

        private bool IsStop = false;

        public void StartWrite()
        {
            try
            {
                this.mFileStream = new FileStream(this.mLocalFileFullName, FileMode.OpenOrCreate, FileAccess.Write);
            }
            catch (Exception ex)
            {
                this.OnFileError(this.mLocalFileFullName, ex);
                return;
            }

            while (this.mCompletedFileLenght < this.mFileLenght && !IsStop)
            {
                if (!this.mSemaphoreWrite.WaitOne(30000))
                {
                    this.IsStop = true;
                }

                if (this.mQueue.Count == 0)
                {
                    continue;
                }

                long dataLenght = 0;
                ArrayList getQueueData = new ArrayList();
                int dequeueCount = 0;
                while (this.mQueue.Count > 0)
                {
                    MsgFileBlock fileBlockMsg = this.mQueue.Dequeue();
                    if (fileBlockMsg != null)
                    {
                        dataLenght += fileBlockMsg.FileBlockData.Length;
                        getQueueData.Add(fileBlockMsg);
                    }
                    dequeueCount++;
                }
                this.mSemaphoreReveive.Release(dequeueCount);

                int index = 0;
                byte[] data = new byte[dataLenght];
                for (int i = 0; i < getQueueData.Count; i++)
                {
                    MsgFileBlock fileBlockMsg = (MsgFileBlock)getQueueData[i];

                    Array.Copy(fileBlockMsg.FileBlockData, 0, data, index, fileBlockMsg.FileBlockData.Length);
                    index += fileBlockMsg.FileBlockData.Length;
                }

                // 避免异常。
                if (this.mCompletedFileLenght >= 0 && this.mCompletedFileLenght < 1000000000000)
                {
                    this.mFileStream.Seek(this.mCompletedFileLenght, SeekOrigin.Begin);
                    this.mFileStream.Write(data, 0, data.Length);
                }

                this.mCompletedFileLenght += data.Length;

                //通知写文件进度发生改变
                this.OnFileProgressChange(this.mCompletedFileLenght);
            }

            ////写文件完成了
            this.CloseFile();
            if (this.mFileLenght == this.mCompletedFileLenght && this.mLocalFileFullName.Contains(".tmp"))
            {
                System.IO.File.Delete(this.mLocalFileFullName.Replace(".tmp", string.Empty));
                System.IO.File.Move(this.mLocalFileFullName, this.mLocalFileFullName.Replace(".tmp", string.Empty));
                this.mLocalFileFullName = this.mLocalFileFullName.Replace(".tmp", string.Empty);
            }

            if (this.IsStop)
            {
                this.OnFileError(this.mLocalFileFullName, new Exception("Stop"));
            }
            else
            {
                //通知写文件完成
                this.OnFileComplelte(this.mLocalFileFullName);
            }

            MyLogManage.Debug(this.GetType(), "AsyncUserToken_Evnet_ServerWriteFileComplete", null, "线程结束");
        }

        public void CloseFile()
        {
            this.OnServerFileWriteManualDispose(this);
            if (this.mFileStream != null)
            {
                this.mFileStream.Close();
                this.mFileStream.Dispose();
                this.mFileStream = null;
            }

            if (this.mQueue != null)
            {
                this.mQueue.Clear();
            }

            if (this.mSemaphoreReveive != null)
            {
                this.mSemaphoreReveive.Close();
                this.mSemaphoreReveive.Dispose();
                this.mSemaphoreReveive = null;
            }

            if (this.mSemaphoreWrite != null)
            {
                this.mSemaphoreWrite.Close();
                this.mSemaphoreWrite.Dispose();
                this.mSemaphoreWrite = null;
            }
        }
    }
}
