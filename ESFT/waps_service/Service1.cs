using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using waps_server;

namespace waps_service
{
    public partial class waps_service : ServiceBase
    {
        public waps_service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            log4net.LogManager.GetLogger(typeof(Program)).Debug("sdfasdfasdfasdfasdfdas");
            SocketServer server = new SocketServer();

            server.StartSocket();

            Console.WriteLine("按任意键退出");
            Console.ReadLine();
        }

        protected override void OnStop()
        {
        }
    }
}
