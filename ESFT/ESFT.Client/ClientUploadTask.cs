using ESFT.Common.EsftThreadsPool;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESFT.Client
{
    public class ClientUploadTask
    {
        static EsftThreadPool mEsftThreadsPool = new EsftThreadPool(1);
        public static Dictionary<string, ClientUploadFile> m_UploadFileTasks = new Dictionary<string, ClientUploadFile>();

        #region 客户端上传文件成功事件
        public event ClientUploadSuccess Evnet_ClientUploadSuccess;

        public virtual void OnClientUploadSuccess(object sender, ClientUploadSuccessArgs e)
        {
            if (this.Evnet_ClientUploadSuccess != null)
            {
                this.Evnet_ClientUploadSuccess(sender, e);
            }
        }
        #endregion

        #region 客户端上传文件发生错误事件
        public event ClientUploadError Evnet_ClientUploadError;

        public virtual void OnClientUploadError(object sender, ClientUploadErrorArgs e)
        {
            if (this.Evnet_ClientUploadError != null)
            {
                this.Evnet_ClientUploadError(sender, e);
            }
        }
        #endregion

        public ClientUploadTask()
        {

        }

        public string AddUploadTask(string iFileFullName
           , string iServerPath, string iServerFileName
           , string iMasterServerIP, int iMasterPort
           , ESFTParameter[] iParameters)
        {

            ClientUploadFile uploadFileTask = new ClientUploadFile(iFileFullName, iServerPath, iServerFileName
                , iMasterServerIP, iMasterPort, iParameters);

            uploadFileTask.Evnet_ClientUploadSuccess += uploadFileTask_Evnet_ClientUploadSuccess;
            uploadFileTask.Evnet_ClientUploadError += uploadFileTask_Evnet_ClientUploadError;

            m_UploadFileTasks.Add(uploadFileTask.Key, uploadFileTask);

            //ThreadPool.QueueUserWorkItem(uploadFileTask.StartSendFile);
            mEsftThreadsPool.QueueUserWorkItem(uploadFileTask.StartSendFile, null);
            return uploadFileTask.Key;

        }

        void uploadFileTask_Evnet_ClientUploadError(object sender, ClientUploadErrorArgs e)
        {
            this.OnClientUploadError(sender, e);
            //ThreadPool.QueueUserWorkItem(uploadFileTask.StartSendFile);
        }

        void uploadFileTask_Evnet_ClientUploadSuccess(object sender, ClientUploadSuccessArgs e)
        {
            this.OnClientUploadSuccess(sender, e);
        }

        public static void DisposeThread()
        {
            foreach (KeyValuePair<string, ClientUploadFile> item in m_UploadFileTasks)
            {
                item.Value.IsStop = true;
                item.Value.Stop();
            }

            if (mEsftThreadsPool != null)
            {
                mEsftThreadsPool.Dispose();
            }
        }
    }
}
