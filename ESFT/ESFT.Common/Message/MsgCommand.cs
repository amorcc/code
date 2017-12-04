using ESFT.Common.TypeDefinitions;

namespace ESFT.Message
{
    /// <summary>
    /// 命令型消息
    /// </summary>
    public class MsgCommand : EsftMsg
    {
        string m_command;

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Command
        {
            get { return this.m_command; }
        }

        public MsgCommand(int iMsgType, string iMsgCommand)
        {
            this.m_packetType = EPackageType.CommandMsg;
            this.m_msgType = iMsgType;
            this.m_command = iMsgCommand;
        }
    }

}
