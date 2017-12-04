using System;

namespace ESFT.Server.ViceServer
{

    public class ViceServerInfo
    {
        string m_ServerIP;

        public string ServerIP
        {
            get { return m_ServerIP; }
            set { m_ServerIP = value; }
        }

        int m_Port;

        public int Port
        {
            get { return m_Port; }
            set { m_Port = value; }
        }

        string m_ServerNetworkCardNumber;

        public string ServerNetworkCardNumber
        {
            get { return m_ServerNetworkCardNumber; }
            set { m_ServerNetworkCardNumber = value; }
        }

        string m_ViceServerName;

        public string ViceServerName
        {
            get { return m_ViceServerName; }
            set { m_ViceServerName = value; }
        }

        /// <summary>
        /// 更新副服务器时间
        /// </summary>
        DateTime m_UpdateTime;

        /// <summary>
        /// 更新副服务器时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return m_UpdateTime; }
            set { m_UpdateTime = value; }
        }

        public ViceServerInfo(string iServerIP, int iPort, string iServerNetworkCardNumber
                , string iViceServerName, DateTime iUpdateTime)
        {
            this.m_ServerIP = iServerIP;
            this.m_Port = iPort;
            this.m_ServerNetworkCardNumber = iServerNetworkCardNumber;
            this.m_ViceServerName = iViceServerName;
            this.m_UpdateTime = iUpdateTime;
        }
    }
}
