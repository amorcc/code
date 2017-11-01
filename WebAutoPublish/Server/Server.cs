using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Server
    {
        public void Start()
        {
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://192.168.31.37:7181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    allSockets.ToList().ForEach(s => s.Send("Echo: " + message));
                };
                socket.OnBinary = file =>
                {
                    string path = ("D:/test.jpg");
                    //创建一个文件流
                    FileStream fs = new FileStream(path, FileMode.Create);
                    //将byte数组写入文件中
                    fs.Write(file, 0, file.Length);
                    //所有流类型都要关闭流，否则会出现内存泄露问题
                    fs.Close();
                };
            });

            //string ss = Console.ReadLine();
            var input = File.Open("D://test.jpg",FileMode.Open);
            while (true)
            {
                Thread.Sleep(2000);
                byte[] s = new byte[input.Length];
                input.Read(s, 0, s.Length);
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(s);
                }
                input.Close();
              input = File.Open("D://test.jpg", FileMode.Open);
            }
        }
        }
    }
}
