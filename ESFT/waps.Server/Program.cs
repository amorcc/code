using ESFT.Common.SystemInfo;
using ESFT.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace waps_server
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            log4net.LogManager.GetLogger(typeof(Program)).Debug("sdfasdfasdfasdfasdfdas");
            SocketServer server = new SocketServer();

            server.StartSocket();

            Console.WriteLine("按任意键退出");
            Console.ReadLine();
        }


    }
}
