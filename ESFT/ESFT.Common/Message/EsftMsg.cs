using ESFT.Common.TypeDefinitions;

namespace ESFT.Message
{
    /// <summary>
    /// 消息类型的基类
    /// </summary>
    public class EsftMsg
    {
        protected EPackageType m_packetType;
        protected int m_msgType;

        /// <summary>
        /// 数据包的类型
        /// </summary>
        public EPackageType PacketType
        {
            get { return this.m_packetType; }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        public int MsgType
        {
            get { return this.m_msgType; }
        }

        public EsftMsg()
        {
            this.m_packetType = EPackageType.Unknown;
        }

        public EsftMsg(EPackageType iPacketType)
        {
            this.m_packetType = iPacketType;
        }
    }
}
