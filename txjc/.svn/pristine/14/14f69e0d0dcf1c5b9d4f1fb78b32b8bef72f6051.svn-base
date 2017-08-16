using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.log
{
    public class ReadLog4net
    {
        public string GetLogFileList()
        {
            JArray lst = new JArray();
            string folderName = AppDomain.CurrentDomain.BaseDirectory + "\\log\\";

            if (System.IO.Directory.Exists(folderName))
            {
                List<string> fileList = FindFile2(folderName);

                if (fileList != null && fileList.Count > 0)
                {
                    var fileListTemp = from t in fileList
                                       orderby t descending
                                       select t;
                    fileList = fileListTemp.ToList();

                    foreach (string fileFullName in fileList)
                    {
                        string filename = fileFullName.Substring(fileFullName.ToString().LastIndexOf('\\') + 1); ;
                        string sitePath = fileFullName.Substring(folderName.Length - 1, fileFullName.Length - folderName.Length + 1);

                        lst.Add(new JObject()
                            {
                                {"FileName" , filename},
                                {"SitePath" , sitePath},
                            });
                    }

                }
            }

            JObject result = new JObject()
            {
                {"success",true},
                {"ResponseID",0},
                {"Message",""},
                {"Data",lst},
            };

            return result.ToString();//返回操作结果
        }

        public List<string> FindFile2(string sSourcePath)
        {
            List<String> list = new List<string>();
            //遍历文件夹
            DirectoryInfo theFolder = new DirectoryInfo(sSourcePath);
            FileInfo[] thefileInfo = theFolder.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
            foreach (FileInfo NextFile in thefileInfo)  //遍历文件
            {
                list.Add(NextFile.FullName);
            }
            //遍历子文件夹
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                //list.Add(NextFolder.ToString());
                FileInfo[] fileInfo = NextFolder.GetFiles("*.txt", SearchOption.AllDirectories);
                foreach (FileInfo NextFile in fileInfo)  //遍历文件
                    list.Add(NextFile.FullName);
            }

            //list降序排列一下
            if (list != null && list.Count > 0)
            {
                var listSorted = from t in list
                                 orderby t descending
                                 select t;

                return listSorted.ToList();
            }
            return list;
        }

        public string GetLogFileInfo(string filePath)
        {
            JArray lst = new JArray();
            var fileName = filePath;
            string fileFullName = AppDomain.CurrentDomain.BaseDirectory + "\\log\\" + fileName;

            if (System.IO.File.Exists(fileFullName))
            {
                FileStream fs = null;
                StreamReader sr = null;

                try
                {
                    fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    sr = new StreamReader(fs, System.Text.Encoding.Default);


                    using (DataTable dt = this.CreatTable())
                    {
                        #region 读取文件
                        string logLine = "";


                        while ((logLine = sr.ReadLine()) != null)
                        {
                            #region 定义
                            //日期
                            DateTime logDate = DateTime.Now;

                            //日志类型：debug、info、error
                            string loggerType = string.Empty;

                            //日志调用类
                            string loggerClass = string.Empty;

                            //站点
                            string siteName = string.Empty;

                            //业务代码
                            string businessCode = string.Empty;

                            //调试信息
                            string info = string.Empty;

                            //用户名
                            string userId = string.Empty;

                            //ip
                            string clientIp = string.Empty;

                            //参数入口
                            string paraIn = string.Empty;

                            //参数出口
                            string paraOut = string.Empty;

                            //表的主键
                            string tableCode = string.Empty;
                            #endregion

                            #region 解析日志
                            if (!string.IsNullOrWhiteSpace(logLine))
                            {
                                //获取日志时间
                                if (logLine.Length > 19)
                                {
                                    string dateStr = logLine.Substring(0, 19);
                                    logDate = DateTime.Parse(dateStr);
                                }

                                string[] logSplit = logLine.Split(new string[] { ";;;;" }, StringSplitOptions.None);
                                if (logSplit != null && logSplit.Length > 1)
                                {
                                    string infoStr = logSplit[0];
                                    string logStr = logSplit[1];

                                    #region 解析前面的信息
                                    //获取日志类型：debug、info、error
                                    string[] infoStrArray = infoStr.Split(new string[] { "--" }, StringSplitOptions.None);
                                    if (infoStrArray != null && infoStrArray.Length > 1)
                                    {
                                        loggerType = infoStrArray[1].Trim();
                                    }

                                    if (infoStrArray != null && infoStrArray.Length > 2)
                                    {
                                        loggerClass = infoStrArray[2].Trim();
                                    }

                                    #endregion

                                    JObject logJo = JObject.Parse(logStr);

                                    businessCode = logJo.GetValue("businesscode").ToString() + "|" + logJo.GetValue("nodename").ToString(); ;
                                    info = logJo.GetValue("description").ToString();
                                    clientIp = logJo.GetValue("ip").ToString();
                                    userId = "" + logJo.GetValue("userid").ToString() + "|" + logJo.GetValue("usersn").ToString() + "|" + logJo.GetValue("roleid").ToString();
                                    paraIn = logJo.GetValue("iparas").ToString();
                                    paraOut = logJo.GetValue("oparas").ToString();
                                    tableCode = logJo.GetValue("ordercode").ToString() + "|" + logJo.GetValue("batchid").ToString();
                                    siteName = logJo.GetValue("siteid").ToString();

                                    string logtype = logJo.GetValue("logtype").ToString();
                                    switch (logtype)
                                    {
                                        case "1":
                                            loggerType = "DEBUG";
                                            break;
                                        case "2":
                                            loggerType = "INFO";
                                            break;
                                        case "3":
                                            loggerType = "WARN";
                                            break;
                                        case "4":
                                            loggerType = "ERROR";
                                            break;
                                        case "5":
                                            loggerType = "FATAL";
                                            break;
                                        case "6":
                                            loggerType = "OrderLog";
                                            break;
                                        default:
                                            break;
                                    }

                                }

                            }
                            else
                            {
                                info = logLine;
                            }
                            #endregion

                            #region 新增一行

                            lst.Add(new JObject()
                                {
                                    {"date" , logDate},
                                    {"loggerType" , loggerType},
                                    {"loggerClass" ,loggerClass},
                                    {"siteName" , siteName},
                                    {"businessCode" , businessCode},
                                    {"info" , info},
                                    {"userId" , userId},
                                    {"clientIp" , clientIp},
                                    {"paraIn" , paraIn},
                                    {"paraOut" , paraOut},
                                    {"tableCode" , tableCode},
                                });
                            #endregion
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    sr.Close();
                    fs.Close();
                    fs = null;
                    sr = null;
                }
            }
            else
            {

            }

            JObject result = new JObject()
            {
                {"success",true},
                {"ResponseID",0},
                {"Message",""},
                {"Data",lst},
            };

            return result.ToString();//返回操作结果
        }

        DataTable CreatTable()
        {
            return null;
            //DataTable dt = new DataTable();
            //dt.Columns.Add("date", typeof(DateTime));
            //dt.Columns.Add("loggerType", typeof(string));
            //dt.Columns.Add("loggerClass", typeof(string));
            //dt.Columns.Add("siteName", typeof(string));
            //dt.Columns.Add("businessCode", typeof(string));
            //dt.Columns.Add("info", typeof(string));
            //dt.Columns.Add("userId", typeof(string));
            //dt.Columns.Add("clientIp", typeof(string));
            //dt.Columns.Add("paraIn", typeof(string));
            //dt.Columns.Add("paraOut", typeof(string));
            //dt.Columns.Add("tableCode", typeof(string));

            //return CreatTable();
        }
    }
}
