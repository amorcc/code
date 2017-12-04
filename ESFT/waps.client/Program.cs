using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waps.client
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = GetIniFileList();

            if (list == null || list.Count == 0)
            {
                Console.WriteLine("没有找到配置文件,回车退出");
                Console.ReadLine();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("**************************************************************");
            Console.WriteLine("** 请选择您要上传的服务器");

            for (int i = 0; i < list.Count; i++)
            {
                string item = list[i];
                string fileName = item.Substring(System.Environment.CurrentDirectory.Length + 1, item.Length - System.Environment.CurrentDirectory.Length - 1);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("** {0}. {1} ", i + 1, fileName);
            }

            int index = -1;

            do
            {
                Console.Write("\n\n** 请输入您选择的序号：  ");

                string userInput = Console.ReadLine();


                if (int.TryParse(userInput, out index))
                {
                    if (index - 1 >= 0 && index - 1 < list.Count)
                    {
                        index = index - 1;

                        string iniFileFullName = list[index];

                        //Console.WriteLine("选择正确" + iniFileFullName);


                        if (!File.Exists(iniFileFullName))
                        {
                            Output.Error("ini文件不存在!");
                            return;
                        }

                        PublishInfo pinfo = new PublishInfo(iniFileFullName);

                        pinfo.ReadIniFile();

                        pinfo.WriteIniFile();

                        Publish publish = new Publish(pinfo);

                        publish.Start();


                        Output.Hint("程序完成,回车退出:)");

                        Console.ReadLine();

                        return;
                    }
                    else
                    {
                        Console.WriteLine("输入错误，请重新选择");
                    }
                }
            } while (!(index - 1 >= 0 && index - 1 < list.Count));


        }

        /// <summary>
        /// 获取压缩包里要压缩的文件
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIniFileList()
        {
            List<String> list = new List<string>();
            //遍历文件夹
            DirectoryInfo theFolder = new DirectoryInfo(System.Environment.CurrentDirectory);
            FileInfo[] thefileInfo = theFolder.GetFiles("*.ini", SearchOption.TopDirectoryOnly);
            foreach (FileInfo NextFile in thefileInfo)  //遍历文件
            {
                list.Add(NextFile.FullName);
            }

            return list;
        }
    }
}
