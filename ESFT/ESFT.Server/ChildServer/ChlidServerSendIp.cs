using ESFT.Common;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESFT.Server
{
    public class ChildServerInfo
    {
        /// <summary>
        /// 根据MsgCommandStr生成的MD5值
        /// </summary>
        public string Key;
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName;
        /// <summary>
        /// 端口号
        /// </summary>
        public string Port;
        /// <summary>
        /// 连接时使用的IP
        /// </summary>
        public string IP;
        /// <summary>
        /// IP1 
        /// </summary>
        public string IP1;
        public string IP2;
        public string IP3;
        /// <summary>
        /// 子服务器发送的Command字符串:服务器名称;端口号;IP1;IP2;IP3;
        /// </summary>
        public string MsgCommandStr;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime;

        public ChildServerInfo()
        {
        }
    }
    public class ChlidServerSendIp : FileTransferBase
    {
        private static Dictionary<string, ChildServerInfo> mChlidServerInfo = new Dictionary<string, ChildServerInfo>();

        private static object obj = new object();

        public ChlidServerSendIp(AsyncUserToken iUserToken)
        {
            this.mUserToken = iUserToken;
            this.mTransferType = TransferType.ChildServerRegistServerInfo;
        }

        public void AddChildServerInfo(MsgCommand iMsg)
        {
            lock (obj)
            {
                if (iMsg.Command != null)
                {
                    ChildServerInfo childServerInfo = new ChildServerInfo();
                    string[] infos = iMsg.Command.Split(';');
                    if (infos != null)
                    {
                        childServerInfo.MsgCommandStr = iMsg.Command;
                        childServerInfo.Key = FileHash.Encry(iMsg.Command);
                        childServerInfo.LastUpdateTime = DateTime.Now;
                        //childServerInfo.IP = this.mUserToken.mClientEndPoint.AddressFamily
                        if (this.mUserToken.mClientEndPoint != null)
                        {
                            System.Net.IPEndPoint iep = this.mUserToken.mClientEndPoint as System.Net.IPEndPoint;
                            if (iep.Address != null)
                            {
                                childServerInfo.IP = iep.Address.ToString();
                            }
                        }
                        if (infos.Length > 0)
                        {
                            childServerInfo.ServerName = infos[0];
                        }
                        if (infos.Length > 1)
                        {
                            childServerInfo.Port = infos[1];
                        }
                        if (infos.Length > 2)
                        {
                            childServerInfo.IP1 = infos[2];
                        }
                        if (infos.Length > 3)
                        {
                            childServerInfo.IP2 = infos[3];
                        }
                        if (infos.Length > 4)
                        {
                            childServerInfo.IP3 = infos[4];
                        }

                        // 添加到Hashtable中
                        if (mChlidServerInfo.ContainsKey(childServerInfo.Key) == false)
                        {
                            // Hashtable不存在，新增
                            mChlidServerInfo.Add(childServerInfo.Key, childServerInfo);
                        }
                        else
                        {
                            mChlidServerInfo[childServerInfo.Key] = childServerInfo;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 移除超时的子服务器信息
        /// </summary>
        /// <param name="iMinutes">超时分钟数</param>
        public static void RemoveTimeOutChildServer(double iMinutes)
        {
            lock (obj)
            {
                if (mChlidServerInfo != null && mChlidServerInfo.Count > 0)
                {
                    ArrayList alv = new ArrayList(mChlidServerInfo.Keys);
                    for (int i = alv.Count - 1; i >= 0; i--)
                    {
                        string key = alv[i] as string;
                        ChildServerInfo csinfo = mChlidServerInfo[key] as ChildServerInfo;

                        TimeSpan ts = DateTime.Now - csinfo.LastUpdateTime;
                        if (ts.TotalMinutes > iMinutes)
                        {
                            mChlidServerInfo.Remove(key);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前所有的服务器信息
        /// </summary>
        /// <returns></returns>
        public static string GetAllServer()
        {
            string childServerInfoStr = string.Empty;
            lock (obj)
            {
                if (ChlidServerSendIp.mChlidServerInfo != null)
                {
                    foreach (var item in ChlidServerSendIp.mChlidServerInfo)
                    {
                        ChildServerInfo childInfo = item.Value as ChildServerInfo;

                        childServerInfoStr += childInfo.Key + ",";
                        childServerInfoStr += childInfo.ServerName + ",";
                        childServerInfoStr += childInfo.IP + ",";
                        childServerInfoStr += childInfo.Port + ",";
                        childServerInfoStr += childInfo.IP1 + ",";
                        childServerInfoStr += childInfo.IP2 + ",";
                        childServerInfoStr += childInfo.IP3;
                        childServerInfoStr += ";";
                    }
                }
            }

            return childServerInfoStr;
        }

        protected override void HandleCommandMsg(MsgCommand iMsg)
        {
            if (iMsg.MsgType == EMessageType.M_SendServerIPToHostServer)
            {
                AddChildServerInfo(iMsg);
            }
        }

        protected override void HandleFileBlockMsg(MsgFileBlock iMsg)
        {
            throw new NotImplementedException();
        }

        protected override void HandleFileInfoMsg(MsgFileInfo iMsg)
        {
            throw new NotImplementedException();
        }

        protected override void HandleParameterMsg(MsgParameter iMsg)
        {
            throw new NotImplementedException();
        }
    }
}
