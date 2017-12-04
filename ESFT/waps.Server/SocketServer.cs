using ESFT.Common.SystemInfo;
using ESFT.Common.TypeDefinitions;
using ESFT.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ESFT.Common;
using System.IO;

using Newtonsoft.Json.Linq;

namespace waps_server
{
    class SocketServer
    {
        ServerListener server;
        int numConnections;
        int receiveBufferSize;

        public bool StartSocket()
        {
            try
            {
                numConnections = SystemInfo.m_ServerNumConnections;
                receiveBufferSize = SystemInfo.m_ReceiveBufferSize;
                int port = SystemInfo.m_LocalPort;
                if (port <= 1000)
                {
                    port = 8000;
                }
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, port);
                if (server == null)
                {
                    server = new ServerListener(numConnections, receiveBufferSize);
                    server.Init();

                    server.EventAddTransferTask += server_EventAddTransferTask;
                    server.EventReceiveCommandPacket += server_EventReceiveCommandPacket;
                    server.EventTransferTaskProgressChange += server_EventTransferTaskProgressChange;
                }
                server.Start(iep);
                return true;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Error(ex.Message, ex);
                return false;
            }
        }

        void server_EventReceiveCommandPacket(object iAsyncUserToken, ESFT.Message.MsgCommand iMsg)
        {
            try
            {

                log4net.LogManager.GetLogger(this.GetType()).Debug("上传完成，开始复制和解压，msg=" + iMsg.Command);
                JObject jo = JObject.Parse(iMsg.Command);

                int type = jo.GetValueExt<int>("Type", 0);

                if (type == 1)
                {
                    string serverSitePath = jo.GetValueExt<string>("ServerSitePath", "");
                    string serverNoDelete = jo.GetValueExt<string>("ServerNoDelete", "");
                    string serverReceiveFileName = jo.GetValueExt<string>("ServerReceiveFileName", "");

                    if (!Directory.Exists(serverSitePath))
                    {
                        this.Error((iAsyncUserToken as AsyncUserToken), "站点的目录不存在");
                    }

                    if (!File.Exists(SystemInfo.m_ServerSaveFilePath + "\\" + serverReceiveFileName))
                    {
                        this.Error((iAsyncUserToken as AsyncUserToken), "上传的压缩文件未找到");
                    }

                    //1.删除站点内的文件
                    this.DeleteSiteFile(serverSitePath, serverNoDelete);
                    //2.解压文件
                    //".\7-Zip\7z.exe"
                    ESFT.Common.CmdHelper.Execute(string.Format("\"7z.exe\" x \"{0}\" -o{1} -aoa", SystemInfo.m_ServerSaveFilePath + "\\" + serverReceiveFileName, serverSitePath));
                }

                (iAsyncUserToken as AsyncUserToken).SendMsg(new ESFT.Message.MsgCommand(EMessageType.M_Server_SendCommandInfo, "全部完成"));
            }
            catch (Exception ex)
            {
                (iAsyncUserToken as AsyncUserToken).SendMsg(new ESFT.Message.MsgCommand(EMessageType.M_Server_SendCommandInfo, "出错了" + ex.ToString()));
            }
        }

        /// <summary>
        /// 删除站点的文件
        /// </summary>
        /// <returns></returns>
        public void DeleteSiteFile(string iFolderFullName, string iNoDeleteFiles)
        {
            try
            {
                iNoDeleteFiles = string.IsNullOrEmpty(iNoDeleteFiles) ? "" : iNoDeleteFiles;
                List<string> noDeleteList = iNoDeleteFiles.Split('|').ToList();
                noDeleteList = noDeleteList == null ? new List<string>() : noDeleteList;

                List<String> list = new List<string>();
                //遍历文件夹
                DirectoryInfo theFolder = new DirectoryInfo(iFolderFullName);
                FileInfo[] thefileInfo = theFolder.GetFiles("*.*", SearchOption.TopDirectoryOnly);
                foreach (FileInfo NextFile in thefileInfo)  //遍历文件
                {
                    bool isDelete = (from t in noDeleteList
                                     where t == NextFile.Name
                                     select t).Count() == 0 ? true : false;

                    if (isDelete)
                    {
                        File.Delete(NextFile.FullName);
                    }
                }
                //遍历子文件夹
                DirectoryInfo[] dirInfo = theFolder.GetDirectories();
                foreach (DirectoryInfo NextFolder in dirInfo)
                {
                    list.Add(iFolderFullName + "\\" + NextFolder.ToString());

                    bool isDelete = (from t in noDeleteList
                                     where t == NextFolder.ToString()
                                     select t).Count() == 0 ? true : false;

                    if (isDelete)
                    {
                        Directory.Delete(NextFolder.FullName, true);
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(this.GetType()).Debug("上传完成删除文件时出错，msg=" + ex.ToString());
            }
        }

        void Error(AsyncUserToken iAsynUserToken, string iMsg)
        {
            iAsynUserToken.SendMsg(new ESFT.Message.MsgCommand(EMessageType.M_Server_SendCommandInfo, iMsg));
        }

        void server_EventTransferTaskProgressChange(object iTask)
        {
        }

        //void server_EventReceiveCommandPacket(ESFT.Message.MsgCommand iMsg)
        //{
        //    Console.WriteLine(iMsg.Command);

        //}

        void server_EventAddTransferTask(object iTask)
        {
        }
    }
}
