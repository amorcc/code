using ESFT.Common;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESFT.Server.Service
{
    public class ServiceGetChildServerInfo : ClientSocket
    {
        string mMasterServerIP;
        int mMasterPort;

        public ServiceGetChildServerInfo(string iMasterServerIP, int iMasterPort)
        {
            this.mReceiveTimeOut = 2000;
            this.mSendTimeOut = 2000;

            this.mMasterServerIP = iMasterServerIP;
            this.mMasterPort = iMasterPort;
        }

        /// <summary>
        /// 获取子服务器信息
        /// </summary>
        /// <param name="iChildServerInfoStr"></param>
        /// <returns></returns>

        public bool GetChildServerInfo(ref string iChildServerInfoStr)
        {
            try
            {
                this.InitSocket(this.mMasterServerIP, this.mMasterPort);

                //开始与服务器通讯
                MsgCommand tellServerRequestChildInfo = new MsgCommand(EMessageType.M_ServiceRequestChildInfo, "");
                this.SendMsg(tellServerRequestChildInfo);

                EsftMsg msg = this.ReveiceMsg();
                if (msg != null
                    && (msg is MsgCommand)
                    && msg.MsgType == EMessageType.M_ServiceRequestChildInfo)
                {
                    iChildServerInfoStr = (msg as MsgCommand).Command;
                    return true;
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(ServiceGetTaskInfo)).Error(ex.Message, ex);
            }
            finally
            {
                this.CloseSocket();
            }

            return false;
        }
    }
}
