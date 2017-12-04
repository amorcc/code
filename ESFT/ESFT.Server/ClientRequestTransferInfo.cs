using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using ESFT.Server.FileManage;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ESFT.Server
{
    /// <summary>
    /// 客户端获取服务器传输信息
    /// </summary>
    public class ClientRequestTransferInfo : FileTransferBase
    {
        public ClientRequestTransferInfo(AsyncUserToken iUserToken)
        {
            this.mUserToken = iUserToken;
        }

        #region 处理消息
        protected override void HandleCommandMsg(MsgCommand iMsg)
        {
            ///获取服务器可用连接数等基础信息
            if (iMsg.MsgType == EMessageType.C_GetServerBaseInfo)
            {
                HandleGetServerBaseInfo(iMsg);
            }

            ///获取服务器正在传输的任务信息
            if (iMsg.MsgType == EMessageType.C_GetServerTransferInfo)
            {
                HandleSendServerTransferInfo(iMsg);
            }

            ///获取子服务器信息
            if (iMsg.MsgType == EMessageType.M_ServiceRequestChildInfo)
            {
                HandleSendChildServerInfo(iMsg);
            }
        }

        protected override void HandleParameterMsg(MsgParameter iMsg)
        {
        }

        protected override void HandleFileInfoMsg(MsgFileInfo iMsg)
        {
        }

        protected override void HandleFileBlockMsg(MsgFileBlock iMsg)
        {
        }
        #endregion

        /// <summary>
        /// 获取服务器可用连接数等基础信息
        /// </summary>
        void HandleGetServerBaseInfo(MsgCommand iMsg)
        {
            ESFTParameter[] parameter = new ESFTParameter[3];
            parameter[0] = new ESFTParameter();
            parameter[0].ParaName = "AvailableSocketNum";
            parameter[0].ParaContent = ServerListener.mThisListener.mSocketAsyncPool.Count.ToString();

            parameter[1] = new ESFTParameter();
            parameter[1].ParaName = "UsedSockeNum";
            parameter[1].ParaContent = ServerListener.mThisListener.mSocketAsyncPool.UsedCount.ToString();

            parameter[2] = new ESFTParameter();
            parameter[2].ParaName = "WriteThreadNum";
            parameter[2].ParaContent = "0";

            MsgParameter msg = new MsgParameter(EMessageType.M_SendServerBaseInfo, parameter);
            this.SendMsg(msg);
        }

        /// <summary>
        /// 获取服务器正在传输的文件信息
        /// </summary>
        /// <param name="iMsg"></param>
        void HandleSendServerTransferInfo(MsgCommand iMsg)
        {
            //foreach (FileTransferBase item in ServerListener.mTransferTask)
            //{

            //}
            string transferInfo = "";
            if (ServerListener.mTransferTask != null
                && ServerListener.mTransferTask.Count > 0)
            {

                foreach (DictionaryEntry item in ServerListener.mTransferTask)
                {
                    FileTransferBase task = (FileTransferBase)item.Value;
                    // key
                    string key = task.Key;
                    // 传输类型 ： none,UploadFile,DownloadFile
                    TransferType transferType = task.mTransferType;
                    //ServerPath
                    string serverPath = task.ServerFileFullName;

                    //FileLenght
                    long fileLenght = task.FileLenght;

                    //CurrentCompleteLenght
                    long currentLenght = task.CurrentLenght;

                    //RecevieOrSendLenght
                    long recevieOrSendLenght = task.CurrentReceviceOrSendLenght;

                    //TransferState
                    string stateStr = task.StateStr;

                    //TransferTime
                    string transferTimeSecond = Convert.ToInt32(task.TransferTimeSecond) + "s";

                    //TSpeed
                    string speed = Convert.ToInt32(task.TransferSpeed / 1024) + "K/s";

                    //LastPacketTime
                    string lastPacketTime = "";
                    if (task.LastReceivePacketTime.HasValue == true)
                    {
                        lastPacketTime = task.LastReceivePacketTime.Value.ToString("MM-dd HH:mm:ss");
                    }
                    else
                    {
                        lastPacketTime = "---";
                    }

                    string beginTime = "";
                    if (task.BeginTime.HasValue == true)
                    {
                        beginTime = task.BeginTime.Value.ToString("MM-dd HH:mm:ss");
                    }
                    else
                    {
                        beginTime = "---";
                    }

                    transferInfo += key + ",";
                    transferInfo += Convert.ToInt32(transferType) + ",";
                    transferInfo += serverPath + ",";
                    transferInfo += fileLenght + ",";
                    transferInfo += currentLenght + ",";
                    transferInfo += recevieOrSendLenght + ",";
                    transferInfo += stateStr + ",";
                    transferInfo += transferTimeSecond + ",";
                    transferInfo += speed + ",";
                    transferInfo += lastPacketTime + ",";
                    transferInfo += beginTime + ";";
                }
            }

            MsgCommand msg = new MsgCommand(EMessageType.M_SendServerTransferInfo, transferInfo);
            this.SendMsg(msg);
        }

        /// <summary>
        /// 获取子服务器信息
        /// </summary>
        /// <param name="iMsg"></param>
        void HandleSendChildServerInfo(MsgCommand iMsg)
        {
            string childServerInfoStr = ChlidServerSendIp.GetAllServer();

            MsgCommand msg = new MsgCommand(EMessageType.M_ServiceRequestChildInfo, childServerInfoStr);
            this.SendMsg(msg);
        }
    }
}
