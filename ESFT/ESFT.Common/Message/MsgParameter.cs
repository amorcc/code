using ESFT.Common.TypeDefinitions;

namespace ESFT.Message
{
    /// <summary>
    /// 参数型消息
    /// </summary>
    public class MsgParameter : EsftMsg
    {
        protected ESFTParameter[] m_parameters;

        /// <summary>
        /// 参数列表
        /// </summary>
        public ESFTParameter[] Parameters
        {
            get { return this.m_parameters; }
        }

        public MsgParameter()
        {
        }

        public MsgParameter(int iMsgType, ESFTParameter[] iParameters)
        {
            this.m_packetType = EPackageType.ParameterMsg;
            this.m_msgType = iMsgType;
            this.m_parameters = iParameters;
        }
    }
}
