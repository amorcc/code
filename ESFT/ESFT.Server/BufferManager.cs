using System.Collections.Generic;
using System.Net.Sockets;

namespace ESFT.Server
{
    /// <summary>
    /// 这个类创建一个单一的大缓冲区，可以划分并分配给每个插槽I / O操作使用SocketAsyncEventArgs对象。
    /// 这使得可以很容易地重用和防碎片堆内存bufffers。
    /// </summary>
    public class BufferManager
    {
        int mNumBytes;                 //控制的缓冲池的总的字节数 the total number of bytes controlled by the buffer pool
        byte[] mBuffer;                //缓冲区管理维护基础字节数组 the underlying byte array maintained by the Buffer Manager
        Stack<int> mFreeIndexPool;     // 
        int mCurrentIndex;
        int mBufferSize;

        public BufferManager(int totalBytes, int bufferSize)
        {
            mNumBytes = totalBytes;
            mCurrentIndex = 0;
            mBufferSize = bufferSize;
            mFreeIndexPool = new Stack<int>();
        }

        ///<summary>
        /// Allocates buffer space used by the buffer pool
        /// 使用缓冲池分配缓冲空间
        /// </summary>
        public void InitBuffer()
        {
            // create one big large buffer and divide that 
            // out to each SocketAsyncEventArg object
            // 创建一个大的大的缓冲区和鸿沟每个SocketAsyncEventArg对象
            mBuffer = new byte[mNumBytes];
        }

        // Assigns a buffer from the buffer pool to the 
        // specified SocketAsyncEventArgs object
        // 从缓冲池分配一个缓冲到指定的SocketAsyncEventArgs对象
        // <returns>true if the buffer was successfully set, else false</returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {

            if (mFreeIndexPool.Count > 0)
            {
                args.SetBuffer(mBuffer, mFreeIndexPool.Pop(), mBufferSize);
            }
            else
            {
                if ((mNumBytes - mBufferSize) < mCurrentIndex)
                {
                    return false;
                }
                args.SetBuffer(mBuffer, mCurrentIndex, mBufferSize);
                mCurrentIndex += mBufferSize;
            }
            return true;
        }

        // Removes the buffer from a SocketAsyncEventArg object.  
        // This frees the buffer back to the buffer pool
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            mFreeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

    }
}
