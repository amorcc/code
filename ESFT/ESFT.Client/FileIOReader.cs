using ESFT.Common.Log;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace ESFT.Client
{
    public class FileIOReader : FileIO
    {
        /// <summary>
        /// 读数据信号量
        /// </summary>
        public Semaphore m_SemaphoreRead;

        /// <summary>
        /// 发送数据信号量
        /// </summary>
        public Semaphore m_SemaphoreSend;

        public int SemaphoreRead = 50;
        public int SemaphoreSend = 0;

        /// <summary>
        /// 一次读取文件的大小
        /// </summary>
        protected int m_OnceReadSize = 1024 * 32;

        public FileIOReader(string iLocalFilePath, string iLocalFileName
            , string iRemoteFilePath, string iRemoteFileName
            , long iFileLenght)
        {
            this.mLocalFileName = iLocalFileName;
            this.mLocalFilePath = iLocalFilePath;
            this.mRemoteFileName = iRemoteFileName;
            this.mRemoteFilePath = iRemoteFilePath;
            this.mFileLenght = iFileLenght;

            this.mStartTime = DateTime.Now;
            this.mQueueMaxSize = 50;
            this.mQueue = new Queue<MsgFileBlock>();

            this.m_SemaphoreRead = new System.Threading.Semaphore(mQueueMaxSize, mQueueMaxSize);
            this.m_SemaphoreSend = new System.Threading.Semaphore(0, mQueueMaxSize);

            this.SemaphoreRead = mQueueMaxSize;
            this.SemaphoreSend = 0;
        }

        public void SetStartIndex(long iStartIndex)
        {
            this.mReceiveOrReadFileLenght = iStartIndex;
            this.mCompletedFileLenght = iStartIndex;
        }

        public void StartReadFile()
        {
            while (this.mReceiveOrReadFileLenght < this.mFileLenght)
            {
                byte[] fileData = this.ReadFileData();

                MsgFileBlock fileBlockMsg = new MsgFileBlock(EMessageType.M_ClientSendFileData, this.mReceiveOrReadFileLenght - fileData.Length, fileData);

                //// 压入队列 
                this.m_SemaphoreRead.WaitOne();
                this.SemaphoreRead--;

                lock (this.mQueue)
                {
                    this.mQueue.Enqueue(fileBlockMsg);
                }
                this.m_SemaphoreSend.Release();
                this.SemaphoreSend++;

                MyLogManage.Debug("000000000000000000000000", "00000", "读取文件：" + SemaphoreRead.ToString() + "/" + SemaphoreSend.ToString());
            }
        }

        /// <summary>
        /// 在当前偏移位置，读取指定大小的数据
        /// </summary>
        /// <returns></returns>
        public byte[] ReadFileData()
        {
            byte[] data = null;

            try
            {
                if (this.mReceiveOrReadFileLenght < this.mFileLenght)
                {
                    long readSize = -1;

                    if (this.mReceiveOrReadFileLenght + this.m_OnceReadSize < this.mFileLenght)
                    {
                        //文件可以读取_OnceReadSize的数据
                        readSize = this.m_OnceReadSize;
                    }
                    else
                    {
                        //文件最后不够读取_OnceReadSize的数据
                        readSize = this.mFileLenght - this.mReceiveOrReadFileLenght;
                    }

                    if (readSize > 0)
                    {
                        data = new byte[readSize];

                        this.mFileStream.Seek(this.mReceiveOrReadFileLenght, System.IO.SeekOrigin.Begin);
                        this.mFileStream.Read(data, 0, (int)readSize);
                        this.mReceiveOrReadFileLenght += readSize;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        /// <summary>
        /// 从开始位置读取一个文件块
        /// </summary>
        /// <param name="iStartIndex"></param>
        /// <returns></returns>
        public byte[] ReadFileBlock(long iStartIndex)
        {
            if (iStartIndex + this.m_OnceReadSize <= this.mFileLenght)
            {
                byte[] data = new byte[this.m_OnceReadSize];
                this.mFileStream.Seek(iStartIndex, System.IO.SeekOrigin.Begin);
                this.mFileStream.Read(data, 0, this.m_OnceReadSize);

                return data;
            }
            else
            {
                //超出文件范围
                int lastLenght = Convert.ToInt32(this.mFileLenght - iStartIndex);
                byte[] data = new byte[lastLenght];
                this.mFileStream.Seek(iStartIndex, SeekOrigin.Begin);
                this.mFileStream.Read(data, 0, lastLenght);

                return data;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitFileIOReader()
        {
            if (!(this.mLocalFilePath == null
                || this.mLocalFilePath == ""
                || this.mLocalFileName == null
                || this.mLocalFileName == ""))
            {

                if (!File.Exists(this.LocalFileFullName))
                {
                    throw new Exception("FileReader.cs : " + this.LocalFileFullName + "读取的文件不存在！");
                }
                else
                {
                    this.mFileStream = new FileStream(this.LocalFileFullName, FileMode.Open, FileAccess.Read);
                }

                this.mFileStream.Seek(this.mReceiveOrReadFileLenght, SeekOrigin.Begin);
            }
            else
            {
                throw new Exception("FileReader.cs : 本地文件不存在读取的文件不存在！");
            }
        }

        public override string GetLocalFullName()
        {
            string fullName = this.mLocalFilePath + "/" + this.mLocalFileName;
            fullName = System.IO.Path.GetFullPath(fullName);
            return fullName;
        }

        public void ReleaseFileIORead()
        {
            lock (this)
            {
                if (this.mFileStream != null)
                {
                    this.mFileStream.Close();
                    this.mFileStream.Dispose();
                    this.mFileStream = null;
                }
            }
        }

        public override void ReleaseFileHandler()
        {
            ReleaseFileIORead();
        }
    }
}

