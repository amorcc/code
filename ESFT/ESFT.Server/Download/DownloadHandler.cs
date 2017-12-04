using ESFT.Common.SystemInfo;
using ESFT.Common.TypeDefinitions;

namespace ESFT.Server.Download
{
    public class DownloadHandler
    {
        public static bool ServerFileExist(string iServerFileName)
        {
            return true;
        }

        public static string ServerRealFileFullName(DownloadFilePathType iServerFilePathType, string iServerFileName)
        {
            string real = string.Empty;
            switch (iServerFilePathType)
            {
                case DownloadFilePathType.RelativePath:
                    real = SystemInfo.m_ServerRealFilePath + "\\" + iServerFileName;
                    break;
                case DownloadFilePathType.AbsolutePath:
                    real = iServerFileName;
                    break;
                default:
                    real = "";
                    break;
            }

            return real;
        }
    }
}
