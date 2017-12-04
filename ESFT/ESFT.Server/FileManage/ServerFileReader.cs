using System;
using System.IO;

namespace ESFT.Server.FileManage
{
    public class ServerFileReader
    {
        string mFileFullName;
        long mFileLenght;
        FileStream mFileStream;

        /// <summary>
        /// 一次读取文件的大小
        /// </summary>
        protected int m_OnceReadSize = 1024 * 32;

        public ServerFileReader(string iFileFullName,long iFileLenght)
        {
            this.mFileFullName = iFileFullName;
            this.mFileLenght = iFileLenght;

            if (File.Exists(this.mFileFullName))
            {
                this.mFileStream = new FileStream(this.mFileFullName, FileMode.Open, FileAccess.Read,FileShare.ReadWrite);
            }
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
                if (lastLenght > 0)
                {
                    byte[] data = new byte[lastLenght];
                    this.mFileStream.Seek(iStartIndex, SeekOrigin.Begin);
                    this.mFileStream.Read(data, 0, lastLenght);

                    return data;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Dispose()
        {
            if (this.mFileStream != null)
            {
                this.mFileStream.Close();
                this.mFileStream.Dispose();
                this.mFileStream = null;
            }
        }
    }
}
