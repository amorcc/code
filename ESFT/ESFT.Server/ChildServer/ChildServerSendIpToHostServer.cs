using ESFT.Common;
using ESFT.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESFT.Server
{
    class ChildServerSendIpToHostServer : ClientSocket
    {
          string mMasterServerIP;
        int mMasterPort;

        public ChildServerSendIpToHostServer(string iMasterServerIP, int iMasterPort)
        {
            this.mReceiveTimeOut = 2000;
            this.mSendTimeOut = 2000;

            this.mMasterServerIP = iMasterServerIP;
            this.mMasterPort = iMasterPort;
        }

        /// <summary>
        /// 获取服务器的可用连接数等基础信息
        /// </summary>
        /// <param name="iAvailableSocketNum">可用socket数目</param>
        /// <param name="iUsedNum">已用socket数目</param>
        /// <param name="iWriteThreadNum">写文件进程数目</param>
        /// <returns></returns>
        public void SendIpToHostServer(MsgCommand iMsg)
        {
            try
            {
                this.InitSocket(this.mMasterServerIP, this.mMasterPort);

                //开始与服务器通讯
                this.SendMsg(iMsg);
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(ServiceGetTaskInfo)).Error(ex.Message, ex);
            }
            finally
            {
                this.CloseSocket();
            }
        }
    }
}
