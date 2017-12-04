using ESFT.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFT_FormatTool
{
    public class FormatVideo
    {
        public static void FormatVideoAndThumbnail(string iSourceFileFullPath, string iTargetFilePath, string iTargetFileName)
        {
            try
            {
                if (!File.Exists(iSourceFileFullPath))
                {
                    Console.WriteLine("源文件不存在，源文件：" + iSourceFileFullPath);
                    log4net.LogManager.GetLogger(typeof(FormatVideo)).Debug("源文件不存在，源文件：" + iSourceFileFullPath);
                }

                iSourceFileFullPath = Path.GetFullPath(iSourceFileFullPath);
                iTargetFilePath = Path.GetFullPath(iTargetFilePath);

                string fileNameNoExtension = Path.GetFileNameWithoutExtension(iTargetFileName);
                string extension = Path.GetExtension(iTargetFileName);

                string mp4TempFullName = Path.GetDirectoryName(iSourceFileFullPath) + "\\" + Path.GetFileName(iSourceFileFullPath) + ".mp4";
                string flvTempFullName = Path.GetDirectoryName(iSourceFileFullPath) + "\\" + Path.GetFileName(iSourceFileFullPath) + ".flv";

                string mp4TargetFullName = iTargetFilePath + "\\" + fileNameNoExtension + ".mp4";
                string flvTargetFullName = iTargetFilePath + "\\" + fileNameNoExtension + ".flv";

                string picFullName = iTargetFilePath + "\\img\\" + fileNameNoExtension + ".jpg";

                if (!File.Exists(iTargetFilePath + "\\img\\"))
                {
                    Directory.CreateDirectory(iTargetFilePath + "\\img\\");
                }

                // ------------------------------------------------------------------
                // 保存原图
                SaveOriginalVideo(iSourceFileFullPath, iTargetFilePath, iTargetFileName);

                // ------------------------------------------------------------------
                // 转换为MP4格式
                FormatVideoMp4(iSourceFileFullPath, mp4TempFullName, mp4TargetFullName, extension);

                // ------------------------------------------------------------------
                // 转换为FLV格式
                FormatVideoFlv(iSourceFileFullPath, flvTempFullName, flvTargetFullName, extension);

                // ------------------------------------------------------------------
                // 生成缩略图
                FormatThumbnail(iSourceFileFullPath, picFullName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                log4net.LogManager.GetLogger("转换程序").Debug(ex.ToString());
            }
        }

        /// <summary>
        /// 保存源文件
        /// </summary>
        /// <param name="iSourceFileFullPath"></param>
        /// <param name="iTargetFilePath"></param>
        static void SaveOriginalVideo(string iSourceFileFullPath, string iTargetFilePath, string iTargetFileName)
        {
            if (File.Exists(iSourceFileFullPath))
            {
                // 缩略图保存的目标路径
                string targetThumbnailPath = Path.GetFullPath(iTargetFilePath + "\\original");

                // 缩略图文件名
                string targetThumbnailFileName = Path.GetFileName(iTargetFileName);

                //缩略图保存的文件全路径
                string targetThumbnailFullName = Path.GetFullPath(targetThumbnailPath + "\\" + targetThumbnailFileName);

                if (!File.Exists(targetThumbnailPath))
                {
                    Directory.CreateDirectory(targetThumbnailPath);
                }

                if (File.Exists(targetThumbnailFullName))
                {
                    File.Delete(targetThumbnailFullName);
                }
                File.Copy(iSourceFileFullPath, targetThumbnailFullName);
            }
        }

        /// <summary>
        /// 转换为MP4格式
        /// </summary>
        /// <param name="iSourceFileFullPath">源文件全路径</param>
        /// <param name="iTargetFilePath">保存路径</param>
        static void FormatVideoMp4(string iSourceFileFullPath, string iTempFullName, string iTargetFullName, string iExtension)
        {
            log4net.LogManager.GetLogger("转换程序").Debug("开始转换mp4格式的文件");
            if (File.Exists(iTargetFullName))
            {
                File.Delete(iTargetFullName);
            }

            if (!File.Exists(iTempFullName))
            {
                CreateProcess(iSourceFileFullPath, "libx264", iTempFullName);
            }

            log4net.LogManager.GetLogger("转换程序").Debug("mp4文件转换完成，开始移动文件到指定位置");
            File.Copy(iTempFullName, iTargetFullName);
        }

        /// <summary>
        /// 转换为FLV格式
        /// </summary>
        /// <param name="iSourceFileFullPath">源文件全路径</param>
        /// <param name="iTargetFilePath">保存路径</param>
        static void FormatVideoFlv(string iSourceFileFullPath, string iTempFullName, string iTargetFullName, string iExtension)
        {
            log4net.LogManager.GetLogger("转换程序").Debug("开始转换flv格式的文件");
            if (File.Exists(iTargetFullName))
            {
                File.Delete(iTargetFullName);
            }

            if (!File.Exists(iTempFullName))
            {
                CreateProcess(iSourceFileFullPath, "flv", iTempFullName);
            }

            log4net.LogManager.GetLogger("转换程序").Debug("flv文件转换完成，开始移动文件到指定位置");
            File.Copy(iTempFullName, iTargetFullName);
        }

        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="iSourceFileFullPath"></param>
        /// <param name="iTargetFilePath"></param>
        static void FormatThumbnail(string iSourceFileFullPath, string iTargetFullName)
        {
            if (File.Exists(iTargetFullName))
            {
                File.Delete(iTargetFullName);
            }
            CreateProcess(iSourceFileFullPath, iTargetFullName);
        }

        #region 视频格式转换并截图
        /// <summary>
        /// 创建格式转换进程
        /// </summary>
        /// <param name="iInPath">原文件路径</param>
        /// <param name="iFormat">转换格式参数</param>
        /// <param name="iOutPath">输出路径</param>
        private static void CreateProcess(string iInPath, string iFormat, string iOutPath)
        {
            string format = string.Empty;
            if (iFormat == "flv")
            {
                format = string.Format(@"-i {0} -ab 128 -acodec libmp3lame -ac 1 -ar 22050 -r 29.97 -qscale 6 -y -s 640x480 {1}", "\"" + iInPath + "\"", "\"" + iOutPath + "\"");
            }
            else
            {
                format = string.Format(@"-i {0} -vcodec libx264 -s 640x480 -y -ar 22050 {1}", "\"" + iInPath + "\"", "\"" + iOutPath + "\"");
            }

            RunCommandUtils sqlplus = new RunCommandUtils(System.Windows.Forms.Application.StartupPath + "\\ffmpeg.exe", format, "", false);
            sqlplus.OutputDataReceived += sqlplus_OutputDataReceived;
            sqlplus.ErrorDataReceived += sqlplus_ErrorDataReceived;
            sqlplus.WaitExit();
        }

        /// <summary>
        /// 创建截图进程
        /// </summary>
        /// <param name="iInPath">原文件路径</param>
        /// <param name="iOutPath">输出路径</param>
        private static void CreateProcess(string iInPath, string iOutPath)
        {
            RunCommandUtils sqlplus = new RunCommandUtils(System.Windows.Forms.Application.StartupPath + "\\ffmpeg.exe", string.Format(@"-i {0} -y  -f  image2  -ss 2 -vframes 1 -s 400x300 {1}", "\"" + iInPath + "\"", "\"" + iOutPath + "\""), string.Empty, false);
            sqlplus.OutputDataReceived += sqlplus_OutputDataReceived;
            sqlplus.ErrorDataReceived += sqlplus_ErrorDataReceived;
            sqlplus.WaitExit();
        }

        private static void sqlplus_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
           // log4net.LogManager.GetLogger(typeof(FormatVideo)).Info(e.Data);
            //File.AppendAllText(baseDir + logDirName, "日志信息:" + e.Data + Environment.NewLine);
        }

        private static void sqlplus_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            //log4net.LogManager.GetLogger(typeof(FormatVideo)).Info(e.Data);
            //File.AppendAllText(baseDir + logDirName, "日志信息:" + e.Data + Environment.NewLine);
        }
        #endregion
    }
}
