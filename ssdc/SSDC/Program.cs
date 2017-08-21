using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace SSDC
{
    static class Program
    {
        private static System.Threading.Mutex mutex;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            mutex = new System.Threading.Mutex(true, "OnlyRun");
            if (mutex.WaitOne(0, false))
            {
                #region 启动数据库
                OpenDBHint dbHint = new OpenDBHint();
                dbHint.Show();
                ExecuteCom("net start mysql", 0);
                MainForm mainForm = new MainForm();
                System.Threading.Thread.Sleep(1200);
                dbHint.Close();
                #endregion
                if (SSDC.Common.Register.CheckRegister() == false)
                {
                    MessageBox.Show("软件尚未注册，请联系管理员进行注册后使用！");
                }

                Application.Run(mainForm);
            }
            else
            {
                MessageBox.Show("程序已经在运行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }

            //Application.Run(new ImportDataMulti());
        }

        /// <summary>    
        /// 执行DOS命令，返回DOS命令的输出  //转载请注明来自 http://www.shang11.com  
        /// </summary>    
        /// <param name="dosCommand">dos命令</param>    
        /// <param name="milliseconds">等待命令执行的时间（单位：毫秒），    
        /// 如果设定为0，则无限等待</param>    
        /// <returns>返回DOS命令的输出</returns>    
        public static string ExecuteCom(string command, int seconds)
        {
            string output = ""; //输出字符串    
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象    
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令    
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出    
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动    
                startInfo.RedirectStandardInput = false;//不重定向输入    
                startInfo.RedirectStandardOutput = true; //重定向输出    
                startInfo.CreateNoWindow = true;//不创建窗口    
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程    
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束    
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒    
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出 
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            return output;
        }
    }
}
