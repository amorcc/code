using ESFT.Common.TypeDefinitions;

namespace ESFT.Message
{
    /// <summary>
    /// ServerInfoMsg 服务器信息消息
    /// </summary>
    public class MsgServerInfo : EsftMsg
    {
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        string m_ServerIP;

        public string ServerIP
        {
            get { return m_ServerIP; }
            set { m_ServerIP = value; }
        }

        /// <summary>
        /// 服务器端口号
        /// </summary>
        int m_Port;

        public int Port
        {
            get { return m_Port; }
            set { m_Port = value; }
        }

        /// <summary>
        /// 服务器网卡序列号
        /// </summary>
        string m_ServerNetworkCordNumber;

        public string ServerNetworkCordNumber
        {
            get { return m_ServerNetworkCordNumber; }
            set { m_ServerNetworkCordNumber = value; }
        }

        /// <summary>
        /// 副服务器名称
        /// </summary>
        string m_ViceServerName;

        public string ViceServerName
        {
            get { return m_ViceServerName; }
            set { m_ViceServerName = value; }
        }

        public MsgServerInfo(int iMsgType, string iServerIP, int iPort, string iServerNetworkCordNumber, string iViceServerName)
        {
            this.m_packetType = EPackageType.ServerInfoMsg;
            this.m_msgType = iMsgType;
            this.m_ServerIP = iServerIP;
            this.m_Port = iPort;
            this.m_ServerNetworkCordNumber = iServerNetworkCordNumber;
            this.m_ViceServerName = iViceServerName;
        }
    }
}
