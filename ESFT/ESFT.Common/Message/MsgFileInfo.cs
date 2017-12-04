using ESFT.Common.TypeDefinitions;

namespace ESFT.Message
{
    /// <summary>
    /// 文件信息消息
    /// </summary>
    public class MsgFileInfo : EsftMsg
    {
        long m_FileLenght;

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileLenght
        {
            get { return m_FileLenght; }
            set { m_FileLenght = value; }
        }

        string m_ClientFileName;

        /// <summary>
        /// 客户端文件名
        /// </summary>
        public string ClientFileName
        {
            get { return m_ClientFileName; }
            set { m_ClientFileName = value; }
        }

        string m_ClietnDirectoryName;

        /// <summary>
        /// 客户端路径
        /// </summary>
        public string ClietnDirectoryName
        {
            get { return m_ClietnDirectoryName; }
            set { m_ClietnDirectoryName = value; }
        }

        string m_ServerFileName;

        /// <summary>
        /// 服务器文件名
        /// </summary>
        public string ServerFileName
        {
            get { return m_ServerFileName; }
            set { m_ServerFileName = value; }
        }

        string m_ServerDirectoryName;

        /// <summary>
        /// 服务器路径
        /// </summary>
        public string ServerDirectoryName
        {
            get { return m_ServerDirectoryName; }
            set { m_ServerDirectoryName = value; }
        }

        string m_Extension;

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string Extension
        {
            get { return m_Extension; }
            set { m_Extension = value; }
        }

        string m_FileMD5;

        /// <summary>
        /// 文件MD5值
        /// </summary>
        public string FileMD5
        {
            get { return m_FileMD5; }
            set { m_FileMD5 = value; }
        }

        public MsgFileInfo(int iMsgType, long iFileLenght, string iClientFileName
            , string iClietnDirectoryName, string iServerFileName
            , string iServerDirectoryName, string iExtension
            , string iFileMD5)
        {
            this.m_packetType = EPackageType.FileInfoMsg;
            this.m_msgType = iMsgType;
            this.m_FileLenght = iFileLenght;
            this.m_ClientFileName = iClientFileName;
            this.m_ClietnDirectoryName = iClietnDirectoryName;
            this.m_ServerFileName = iServerFileName;
            this.m_ServerDirectoryName = iServerDirectoryName;
            this.m_Extension = iExtension;
            this.m_FileMD5 = iFileMD5;
        }
    }
}
