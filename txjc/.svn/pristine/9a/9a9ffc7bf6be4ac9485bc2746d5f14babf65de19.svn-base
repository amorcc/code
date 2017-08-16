using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using cc.common.Utility;
using cc.common.Sys;

namespace cc.log
{
    /// <summary>
    /// 给哨兵发日志的日志池
    /// 使用Semaphore信号量+ThreadPool线程池控制发送日志的线程数目
    /// </summary>
    public class LogPool
    {
        static bool mIsSend = false;
        static int mThreadMaxNum = 15;

        static Queue<RequestData> mQueue = new Queue<RequestData>();

        static Semaphore mSemaphore = new Semaphore(mThreadMaxNum, mThreadMaxNum);

        public LogPool()
        {
            InitLogPool();
        }

        /// <summary>
        /// 初始化线程池的数目
        /// </summary>
        public static void InitLogPool()
        {
            ThreadPool.SetMaxThreads(mThreadMaxNum, mThreadMaxNum);
        }

        /// <summary>
        /// 要发送的日志入队，并等待信号量和线程池空闲时发送
        /// </summary>
        /// <param name="iSiteWebAPI"></param>
        /// <param name="iLogType"></param>
        /// <param name="iPara"></param>
        /// <param name="iIsQueue">尚未启用</param>
        public static void EnQueue(LogType iLogType, JObject iPara, SiteWebAPI iSiteWebAPI = SiteWebAPI.SentinelWebAPI, bool iIsQueue = false)
        {
            try
            {
                RequestData rd = new RequestData(iSiteWebAPI, iLogType, iPara, iIsQueue);

                lock (mQueue)
                {
                    mQueue.Enqueue(rd);
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(Run));
            }
            catch (Exception ex)
            {
                Log.AddLog4netLog(typeof(LogPool), ex);
            }
        }

        /// <summary>
        /// 发送日志到哨兵
        /// </summary>
        /// <param name="para"></param>
        public static void Run(Object para)
        {
            mSemaphore.WaitOne();

            lock (mQueue)
            {
                try
                {
                    if (mQueue.Count > 0)
                    {
                        RequestData rd = mQueue.Dequeue();

                        string iErrorMsg = string.Empty;
                        string webAPIUrl = rd.GetWebAPIUrl(out iErrorMsg);

                        if (string.IsNullOrEmpty(iErrorMsg) && mIsSend == true)
                        {
                            JObject rt = cc.common.WebApiHelper.WebApiHelper.Post(webAPIUrl, rd.Para.ToString());

                            if (rt == null || rt.GetValueExt<int>("ResponseID") != 0)
                            {
                                string sentErrorMsg = rt.GetValueExt<string>("Message", "哨兵未返回错误信息");
                                string errormsg = string.Format("向哨兵写日志时出错:{3},SiteWebAPI={0},ActionUrl={1},Para={2}", rd.SiteWebAPI.ToString(), rd.ActionUrl, rd.Para.ToString(), sentErrorMsg);

                                Log.AddLog4netLog(typeof(LogPool), new Exception(errormsg));
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Log.AddLog4netLog(typeof(LogPool), ex);
                }
            }

            int currCount = mSemaphore.Release();
        }

    }
}
