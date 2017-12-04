using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESFT.Server
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            try
            {
                bool bCreatedNew;
                string mutexName = "AutoTransmission";
                Mutex m = new Mutex(false, mutexName, out bCreatedNew);

                if (bCreatedNew)
                {
                    log4net.LogManager.GetLogger(typeof(Program)).Error("程序运行……");
                    Application.Run(new FormServer());
                }
                else
                {
                    log4net.LogManager.GetLogger(typeof(Program)).Error("程序已经在运行中……");
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(Program)).Error(ex.Message, ex);
            }
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            log4net.LogManager.GetLogger(typeof(Program)).Error(t.Exception.Message, t.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            log4net.LogManager.GetLogger(typeof(Program)).Error(ex == null ? e.ToString() : ex.Message, ex);
        }
    }
}
