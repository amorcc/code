using System;

namespace ESFT.Common.SystemInfo
{
    public class SystemInfo
    {
        /// <summary>
        /// ini文件全路径
        /// </summary>
        static string m_IniFileFullName;

        static IniFile m_INIHelper;

        /// <summary>
        /// 服务端最大并发量
        /// </summary>
        public static int m_ServerNumConnections = 200;
        /// <summary>
        /// 服务器每个连接的缓冲区大小（1024*32以上）
        /// </summary>
        public static int m_ReceiveBufferSize = 64 * 1024;

        /// <summary>
        /// 文件在传输的时候，保存的地址
        /// </summary>
        public static string m_ServerSaveFilePath;

        public static string ServerSaveFilePath
        {
            get
            {
                m_ServerSaveFilePath = System.IO.Path.GetFullPath(m_ServerSaveFilePath);
                return m_ServerSaveFilePath;
            }
        }

        /// <summary>
        /// 真实的服务器文件存放地址，这个是文件传输完成后，转换结束，保存的地址
        /// </summary>
        public static string m_ServerRealFilePath;

        /// <summary>
        /// 保存未完成任务信息的XML文件名
        /// </summary>
        public static string m_SaveUnfinishXmlFileName = "unfinish.xml";

        /// <summary>
        /// 服务器名称
        /// </summary>
        public static string m_ServerName = "";

        /// <summary>
        /// 是否是主服务器
        /// </summary>
        public static bool m_IsMasterServer = true;

        public static string m_MasterServerIP = "";
        public static int m_MasterPort = 8000;
        public static int m_LocalPort = 8000;
        public static string m_LocalIP = "";

        static SystemInfo()
        {
            m_IniFileFullName = AppDomain.CurrentDomain.BaseDirectory + "sys.ini";

            if (!System.IO.File.Exists(m_IniFileFullName))
            {
                System.IO.File.Create(m_IniFileFullName);
            }
            m_INIHelper = new IniFile(m_IniFileFullName);

            m_ServerNumConnections = Convert.ToInt32(m_INIHelper.ReadString("服务端最大并发量", "ServerNumConnections", "200"));
            m_ReceiveBufferSize = Convert.ToInt32(m_INIHelper.ReadString("服务端每个连接的接收缓冲区大小", "ReceiveBufferSize", "65535"));
            m_ServerSaveFilePath = m_INIHelper.ReadString("服务器保存文件的路径", "ServerSaveFilePath", "");
            m_ServerRealFilePath = m_INIHelper.ReadString("服务器真实文件的路径", "ServerRealFilePath", "");
            m_ServerName = m_INIHelper.ReadString("服务器名称", "ServerName", "");
            m_MasterServerIP = m_INIHelper.ReadString("主服务器IP", "MasterServerIP", "116.255.248.67");
            m_MasterPort = Convert.ToInt32(m_INIHelper.ReadString("主服务器Port", "MasterServerPort", "8000"));
            m_IsMasterServer = Convert.ToBoolean(m_INIHelper.ReadString("是否是主服务器", "IsMasterServer", "false"));
            m_LocalPort = Convert.ToInt32(m_INIHelper.ReadString("本机端口号", "LocalPort", "8000"));
            m_LocalIP = m_INIHelper.ReadString("本机IP", "LocalIP", "218.29.67.198");

            try
            {
                if (System.IO.Directory.Exists(m_ServerRealFilePath) == false)
                {
                    System.IO.Directory.CreateDirectory(m_ServerRealFilePath);
                }

                if (m_ServerSaveFilePath == null
                    || m_ServerSaveFilePath == "")
                {
                    if (System.IO.Directory.Exists("d:/") == true)
                    {
                        if (System.IO.Directory.Exists("d:/uploadfile") == false)
                        {
                            System.IO.Directory.CreateDirectory("d:/uploadfile");
                            m_ServerSaveFilePath = "d:/uploadfile";
                            SetServerSaveFilePath("d:/uploadfile");
                        }
                    }
                    else
                    {
                        throw new Exception("服务器保存文件路径不存在，检测不存在D盘，请手动修改ini文件。'服务器保存文件的路径''ServerSaveFilePath'");
                    }
                }
                else
                {
                    if (!System.IO.Directory.Exists(m_ServerSaveFilePath))
                    {
                        System.IO.Directory.CreateDirectory(m_ServerSaveFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("SystemInfo.cs").Error("创建保存文件的文件夹" + m_ServerSaveFilePath + "失败!" + ex.ToString());
            }
        }

        public static void SetServerNumConnections(int iServerNumConnections)
        {
            m_INIHelper.WriteString("服务端最大并发量", "ServerNumConnections", iServerNumConnections.ToString());
        }

        public static void SetReceiveBufferSize(int iReceiveBufferSize)
        {
            m_INIHelper.WriteString("服务端每个连接的接收缓冲区大小", "ReceiveBufferSize", iReceiveBufferSize.ToString());
        }

        public static void SetServerSaveFilePath(string iServerFilePath)
        {
            if (System.IO.Directory.Exists(iServerFilePath) == true)
            {
                m_INIHelper.WriteString("服务器保存文件的路径", "ServerSaveFilePath", iServerFilePath);
            }
        }
    }
}
