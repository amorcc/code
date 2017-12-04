using ESFT.Common;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;

namespace ESFT.Server
{
    /// <summary>
    /// 获取传输的基础信息
    /// </summary>
    public class ServiceGetBaseInfo : ClientSocket
    {
        string mMasterServerIP;
        int mMasterPort;

        public ServiceGetBaseInfo(string iMasterServerIP, int iMasterPort)
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
        public bool GetServiceBaseInfo(ref int iAvailableSocketNum, ref int iUsedNum, ref int iWriteThreadNum)
        {
            try
            {
                this.InitSocket(this.mMasterServerIP, this.mMasterPort);

                //开始与服务器通讯
                MsgCommand tellServerRequestBaseInfo = new MsgCommand(EMessageType.C_GetServerBaseInfo, "");
                this.SendMsg(tellServerRequestBaseInfo);

                EsftMsg msg = this.ReveiceMsg();
                if (msg != null
                    && (msg is MsgParameter)
                    && msg.MsgType == EMessageType.M_SendServerBaseInfo)
                {
                    MsgParameter para = (MsgParameter)msg;

                    if (para.Parameters.Length == 3)
                    {
                        if (para.Parameters[0].ParaName == "AvailableSocketNum")
                        {
                            int.TryParse(para.Parameters[0].ParaContent, out iAvailableSocketNum);
                        }

                        if (para.Parameters[1].ParaName == "UsedSockeNum")
                        {
                            int.TryParse(para.Parameters[1].ParaContent, out iUsedNum);
                        }

                        if (para.Parameters[2].ParaName == "WriteThreadNum")
                        {
                            int.TryParse(para.Parameters[2].ParaContent, out iWriteThreadNum);
                        }
                    }
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
