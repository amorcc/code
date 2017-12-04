
using ESFT.Common;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
namespace ESFT.Server
{
    public class ServiceGetTaskInfo : ClientSocket
    {
        string mMasterServerIP;
        int mMasterPort;

        public ServiceGetTaskInfo(string iMasterServerIP, int iMasterPort)
        {
            this.mReceiveTimeOut = 20000;
            this.mSendTimeOut = 2000;

            this.mMasterServerIP = iMasterServerIP;
            this.mMasterPort = iMasterPort;
        }

        public bool GetServiceTaskInfo(ref string iReturnStr)
        {
            try
            {
                this.InitSocket(this.mMasterServerIP, this.mMasterPort);

                //开始与服务器通讯
                MsgCommand tellServerRequestTaskInfo = new MsgCommand(EMessageType.C_GetServerTransferInfo, "");
                this.SendMsg(tellServerRequestTaskInfo);

                EsftMsg msg = this.ReveiceMsg();
                if (msg != null
                    && (msg is MsgCommand)
                    && msg.MsgType == EMessageType.M_SendServerTransferInfo)
                {
                    iReturnStr = ((MsgCommand)msg).Command;
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
