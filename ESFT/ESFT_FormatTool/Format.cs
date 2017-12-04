using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFT_FormatTool
{
    public class Format
    {
        /// <summary>
        /// 转换文件格式
        /// </summary>
        /// <param name="iFileType">文件类型</param>
        /// <param name="iSourceFileFullPath">源文件路径+文件名</param>
        /// <param name="iTargetFilePath">目标文件路径</param>
        public static void FormatFile(FileType iFileType, string iSourceFileFullPath, string iTargetFilePath, string iTargetFileName)
        {
            iSourceFileFullPath = Path.GetFullPath(iSourceFileFullPath);
            iTargetFilePath = Path.GetFullPath(iTargetFilePath);

            // 判断源文件是否存在
            if (!File.Exists(iSourceFileFullPath))
            {
                log4net.LogManager.GetLogger(typeof(Format)).Error("源文件不存在，源文件：" + iSourceFileFullPath);
                Console.WriteLine("源文件不存在，源文件：" + iSourceFileFullPath);
                return;
            }

            // 创建保存转换后文件的路径
            CreateDirectory(iTargetFilePath);

            // 判断类型进行转换
            if (iFileType == FileType.Image)
            {
                FormatImage.FormatImageAndThumbnail(iSourceFileFullPath, iTargetFilePath, iTargetFileName);
            }
            else if (iFileType == FileType.Video)
            {
                FormatVideo.FormatVideoAndThumbnail(iSourceFileFullPath, iTargetFilePath, iTargetFileName);
            }
            else
            {
                try
                {
                    System.IO.File.Copy(iSourceFileFullPath, iTargetFilePath + "\\" + iTargetFileName, true);

                    string targetThumbnailPath = Path.GetFullPath(iTargetFilePath + "\\original");
                    if (!File.Exists(targetThumbnailPath))
                    {
                        Directory.CreateDirectory(targetThumbnailPath);
                    }

                    System.IO.File.Copy(iSourceFileFullPath, iTargetFilePath + "\\original\\" + iTargetFileName, true);

                }
                catch (Exception ex)
                {
                    log4net.LogManager.GetLogger(typeof(Format)).Error(ex.Message, ex);
                }

                Console.WriteLine("无须转换，直接复制，源文件：" + iSourceFileFullPath);
                log4net.LogManager.GetLogger(typeof(Format)).Info("无须转换，直接复制，源文件：" + iSourceFileFullPath);
            }
        }


        public static bool CreateDirectory(string iDirName)
        {
            if (!File.Exists(iDirName))
            {
                Directory.CreateDirectory(iDirName);
            }
            return false;
        }
    }
}
