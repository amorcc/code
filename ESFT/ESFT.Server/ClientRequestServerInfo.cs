using ESFT.Common.Log;
using ESFT.Common.SystemInfo;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using ESFT.Server.ViceServer;

namespace ESFT.Server
{
    public class ClientRequestServerInfo : FileTransferBase
    {
        public ClientRequestServerInfo(AsyncUserToken iUserToken)
        {
            this.mUserToken = iUserToken;
        }

        #region 处理消息
        protected override void HandleCommandMsg(MsgCommand iMsg)
        {
            //客户端请求分配服务器
            if (iMsg.MsgType == EMessageType.M_ClientRequestServerInfo)
            {
                ViceServerInfo viceInfo = ServerListener.AllocationServer();
                if (viceInfo == null)
                {
                    MsgServerInfo viceServerMsg = new MsgServerInfo(EMessageType.M_ServerTellClientServerInfo
                        , SystemInfo.m_LocalIP, SystemInfo.m_LocalPort, "", "主服务器");
                    this.SendMsg(viceServerMsg);
                }
                else
                {
                    MsgServerInfo viceServerMsg = new MsgServerInfo(EMessageType.M_ServerTellClientServerInfo
                        , viceInfo.ServerIP, viceInfo.Port, viceInfo.ServerNetworkCardNumber, viceInfo.ViceServerName);
                    this.SendMsg(viceServerMsg);
                }
            }
        }

        protected override void HandleFileInfoMsg(MsgFileInfo iMsg)
        {
        }

        protected override void HandleParameterMsg(MsgParameter iMsg)
        {
        }

        protected override void HandleFileBlockMsg(MsgFileBlock iMsg)
        {
        }
        #endregion
    }
}
