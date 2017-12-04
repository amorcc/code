using ESFT.Common.TypeDefinitions;

namespace ESFT.Message
{
    /// <summary>
    /// 文件块消息
    /// </summary>
    public class MsgFileBlock : EsftMsg
    {
        /// <summary>
        /// 文件块在文件中的偏移位置
        /// </summary>
        long m_Offset;
        /// <summary>
        /// 文件块信息
        /// </summary>
        byte[] m_fileBlockData;

        /// <summary>
        /// 文件块信息
        /// </summary>
        public byte[] FileBlockData
        {
            get { return this.m_fileBlockData; }
        }

        /// <summary>
        /// 文件块在文件中的偏移位置
        /// </summary>
        public long Offset
        {
            get { return this.m_Offset; }
        }

        public MsgFileBlock(int iMsgType, long iOffset, byte[] iData)
        {
            this.m_packetType = EPackageType.FileBlockMsg;
            this.m_msgType = iMsgType;
            this.m_Offset = iOffset;
            this.m_fileBlockData = iData;
        }
    }
}
