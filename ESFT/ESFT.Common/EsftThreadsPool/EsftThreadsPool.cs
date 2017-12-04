using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ESFT.Common.EsftThreadsPool
{
    struct WaitCallbackData
    {
        public string key;
        /// <summary>
        /// 等待执行的方法
        /// </summary>
        public WaitCallback callback;

        /// <summary>
        /// 执行方法时需要的参数
        /// </summary>
        public object obj;
    }

    public class EsftThreadPool : IDisposable
    {
        object o = new object();

        /// <summary>
        /// 最大可同时执行的线程数
        /// </summary>
        int mMaxThreadNum = 2;

        /// <summary>
        /// 当前正在执行的线程数
        /// </summary>
        int mCurrentRunNum = 0;

        /// <summary>
        /// 等待执行方法的队列
        /// </summary>
        Queue<WaitCallbackData> mThreadQueue = new Queue<WaitCallbackData>();

        ArrayList mThreadRunning = new ArrayList();

        public EsftThreadPool(int iMaxThreadNum)
        {
            this.mMaxThreadNum = iMaxThreadNum;
            this.mCurrentRunNum = 0;
        }

        /// <summary>
        /// 将方法排入队列以便执行。 此方法在有线程池线程变得可用时执行。
        /// </summary>
        /// <param name="iCallback">方法</param>
        /// <param name="iObj">参数</param>
        /// <returns></returns>
        public bool QueueUserWorkItem(WaitCallback iCallback, object iObj)
        {
            lock (o)
            {
                mThreadQueue.Enqueue(new WaitCallbackData() { callback = iCallback, obj = iObj, key = System.Guid.NewGuid().ToString() });

                RunThread();
            }
            return false;
        }

        /// <summary>
        /// 如果有空闲则执行新的线程
        /// </summary>
        private void RunThread()
        {

            if (mCurrentRunNum < mMaxThreadNum && mThreadQueue.Count > 0)
            {
                WaitCallbackData waitCallbackData = mThreadQueue.Dequeue();
                Thread t = new Thread(Run);
                t.Name = waitCallbackData.key;

                mThreadRunning.Add(t);

                if (new Random().Next(1) == 0)
                    t.Priority = ThreadPriority.Lowest;
                else
                    t.Priority = ThreadPriority.Normal;
                t.Start(waitCallbackData);
                mCurrentRunNum++;
            }
        }

        /// <summary>
        /// 线程执行的函数，先执行队列中的方法，然后执行回调方法，通知pool可以进入新的线程了
        /// </summary>
        /// <param name="callback"></param>
        void Run(object callback)
        {
            ((WaitCallbackData)callback).callback(((WaitCallbackData)callback).obj);
            this.Callback(((WaitCallbackData)callback).key);
        }

        /// <summary>
        /// 线程执行完成时的回调函数
        /// </summary>
        void Callback(string iThreadKey)
        {
            lock (o)
            {
                mCurrentRunNum--;

                //从正在运行的arraylist中删除
                foreach (Thread item in this.mThreadRunning)
                {
                    if (item.Name == iThreadKey)
                    {
                        this.mThreadRunning.Remove(item);
                        break;
                    }
                } 

                RunThread();
            }
        }

      

        ~EsftThreadPool()
        {
            this.Dispose();
        }

        public virtual void Dispose()
        {
            lock (o)
            {
                if (this.mThreadQueue != null)
                {
                    this.mThreadQueue.Clear();
                }

                foreach (Thread item in this.mThreadRunning)
                {
                    item.Abort();
                }
            }
        }
    }
}
