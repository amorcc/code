using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESFT_FormatTool
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            if (args != null && args.Length == 4)
            {
                // 文件类型
                FileType fileType = FormatFileType(args[0]);

                // 要转换的源文件路径+文件名
                string sourceFileFullPath = args[1];

                // 转换后保存的文件位置
                string targetFilePath = args[2];

                string targetFileName = args[3];

                log4net.LogManager.GetLogger(typeof(Program)).Debug(fileType.ToString() + " " + sourceFileFullPath + " " + targetFilePath + " " + args[3]);
                Format.FormatFile(fileType, sourceFileFullPath, targetFilePath, targetFileName);
                log4net.LogManager.GetLogger(typeof(Program)).Info("转换完成！" + fileType.ToString() + " " + sourceFileFullPath + " " + targetFilePath + " " + args[3]);
            }
            else
            {
                log4net.LogManager.GetLogger(typeof(Program)).Debug("转换程序开始：");
                foreach (var s in args)
                {
                    log4net.LogManager.GetLogger(typeof(Program)).Debug(s);
                }
                log4net.LogManager.GetLogger(typeof(Program)).Debug("转换程序结束。");
                Application.Run(new FormatTool());
            }
        }

        /// <summary>
        /// 将字符串转换为FileType格式
        /// </summary>
        /// <param name="iExtension"></param>
        /// <returns></returns>
        static FileType FormatFileType(string iExtension)
        {
            FileType fileType = FileType.Other;
            if (iExtension.ToUpper() == ".JPG"
                || iExtension.ToUpper() == ".PNG"
                || iExtension.ToUpper() == ".JPEG"
                || iExtension.ToUpper() == ".BMP")
            {
                fileType = FileType.Image;
            }
            else if (iExtension.ToUpper() == ".MP4"
                    || iExtension.ToUpper() == ".AVI"
                    || iExtension.ToUpper() == ".FLV"
                    || iExtension.ToUpper() == ".WMV"
                    || iExtension.ToUpper() == ".3GP"
                    || iExtension.ToUpper() == ".RMVB"
                    || iExtension.ToUpper() == ".MOV"
                    )
            {
                fileType = FileType.Video;
            }
            else
            {
                fileType = FileType.Other;
            }

            return fileType;
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            ILog log = log4net.LogManager.GetLogger(typeof(Program));
            log.Debug(t.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ILog log = log4net.LogManager.GetLogger(typeof(Program));
            log.Debug(e.ExceptionObject);
        }
    }
}
