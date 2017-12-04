using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ESFT.Common.TypeDefinitions;
using ESFT.Common.EsftThreadsPool;

namespace ESFT.Client
{
    public class ClientDownloadTask
    {
        public ClientDownloadTask()
        {

        }

        static EsftThreadPool mEsftThreadsPool = new EsftThreadPool(2);
        static Hashtable mDownloadTasks = new Hashtable();

        #region 客户端下载文件成功事件
        public event ClientDownloadSuccess Evnet_ClientDownloadSuccess;

        public virtual void OnClientDownloadSuccess(ClientDownloadSuccessArgs e)
        {
            if (this.Evnet_ClientDownloadSuccess != null)
            {
                this.Evnet_ClientDownloadSuccess(e);
            }
        }
        #endregion

        #region 客户端下载文件不存在事件
        public event ClientDownloadNonExistent Evnet_ClientDownloadNonExistent;

        public virtual void OnClientDownloadNonExistent(ClientDownloadNonExistentArgs e)
        {
            if (this.Evnet_ClientDownloadNonExistent != null)
            {
                this.Evnet_ClientDownloadNonExistent(e);
            }
        }
        #endregion

        public ClientDownload AddDownload(DownloadFilePathType iServerFilePathType, string iServerFileName
            , string iLocalPath, string iLocalFileName
            , string iMasterServerIP, int iMasterPort)
        {
            if (mDownloadTasks.ContainsKey(iServerFilePathType) == false)
            {
                ClientDownload download = new ClientDownload(iServerFilePathType, iServerFileName, iLocalPath, iLocalFileName, iMasterServerIP, iMasterPort);

                download.Evnet_ClientDownloadNonExistent += download_Evnet_ClientDownloadNonExistent;
                download.Evnet_ClientDownloadSuccess += download_Evnet_ClientDownloadSuccess;


                mDownloadTasks.Add(Guid.NewGuid().ToString(), download);

                //ThreadPool.QueueUserWorkItem(download.StartDownload);
                mEsftThreadsPool.QueueUserWorkItem(download.StartDownload, null);
                return download;
            }
            else
            {
                return (ClientDownload)mDownloadTasks[iServerFilePathType];
            }

        }

        void download_Evnet_ClientDownloadSuccess(ClientDownloadSuccessArgs e)
        {
            this.OnClientDownloadSuccess(e);
        }

        void download_Evnet_ClientDownloadNonExistent(ClientDownloadNonExistentArgs e)
        {
            this.OnClientDownloadNonExistent(e);
        }

        public static void DisposeThread()
        {
            foreach (DictionaryEntry item in mDownloadTasks)
            {
                ((ClientDownload)item.Value).IsStop = true;
            }

            if (mEsftThreadsPool != null)
            {
                mEsftThreadsPool.Dispose();
            }
        }
    }
}
