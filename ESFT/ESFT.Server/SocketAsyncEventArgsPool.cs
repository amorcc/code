using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

namespace ESFT.Server
{
    /// <summary>
    /// SocketAsyncEventArgs池
    /// 有新socket到达时，从池中pop一个SocketAsyncEventArgs使用，避免重复创建SocketAsyncEventArgs，实现SocketAsyncEventArgs的重用性
    /// </summary>
    public class SocketAsyncEventArgsPool
    {

        Stack<SocketAsyncEventArgs> mPool;

        public ArrayList mUsedPool;

        // Initializes the object pool to the specified size
        //
        // The "capacity" parameter is the maximum number of 
        // SocketAsyncEventArgs objects the pool can hold
        public SocketAsyncEventArgsPool(int capacity)
        {
            mPool = new Stack<SocketAsyncEventArgs>(capacity);
            mUsedPool = new ArrayList();
        }

        // Add a SocketAsyncEventArg instance to the pool
        //
        //The "item" parameter is the SocketAsyncEventArgs instance 
        // to add to the pool
        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null) { throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null"); }
            lock (mPool)
            {
                mUsedPool.Remove(item);
                mPool.Push(item);
            }
        }

        public bool Contains(SocketAsyncEventArgs e)
        {
            return this.mPool.Contains(e);
        }

        // Removes a SocketAsyncEventArgs instance from the pool
        // and returns the object removed from the pool
        public SocketAsyncEventArgs Pop()
        {
            lock (mPool)
            {
                SocketAsyncEventArgs item = mPool.Pop();
                mUsedPool.Add(item);
                return item;
            }
        }

        // The number of SocketAsyncEventArgs instances in the pool
        /// <summary>
        /// 池中的未使用的SocketAsyncEventArgs数量
        /// </summary>
        public int Count
        {
            get { return mPool.Count; }
        }

        /// <summary>
        /// 已经使用的SocketAsyncEventArgs数量
        /// </summary>
        public int UsedCount
        {
            get { return mUsedPool.Count; }
        }

        /// <summary>
        /// 获取UsedPool的值在指定的index里
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public SocketAsyncEventArgs GetUsedPoolByIndex(int iIndex)
        {
            return (SocketAsyncEventArgs)mUsedPool[iIndex];
        }
    }
}
