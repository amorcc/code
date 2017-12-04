using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ESFT.Client;
using ESFT.Common.TypeDefinitions;
using ESFT.Message;
using Newtonsoft.Json.Linq;
using ESFT.Common;

namespace waps.client
{
    public class Publish
    {
        PublishInfo mPInfo;

        SocketClient mClient = null;

        public Publish(PublishInfo iPInfo)
        {
            this.mPInfo = iPInfo;
        }

        public void Start()
        {
            if (this.AutoPublistDotNet() == false)
            {
                return;
            }

            if (this.ZipPublishTarget() == false)
            {
                return;
            }

            if (this.UploadFile() == false)
            {
                return;
            }
        }

        /// <summary>
        /// step1： 自动发布.net
        /// </summary>
        bool AutoPublistDotNet()
        {
            if (this.mPInfo.mIsAutoPublish == true)
            {
                Output.Hint("\n\n");
                Output.Hint("*********************************");
                Output.Hint("***** 开始自动发布.NET   ********");
                Output.Hint("*********************************");
                Output.Hint("\n\n");

                if (!Directory.Exists(this.mPInfo.mSlnPath))
                {
                    Output.Error("解决方案目录不存在!");
                    return false;
                }

                if (!File.Exists(this.mPInfo.GetBuildXmlFile()))
                {
                    Output.Error("MSBuild的配置文件信息不存在!");
                    return false;
                }

                if (!File.Exists(this.mPInfo.mMSBuildFile))
                {
                    Output.Error(@"MSBuild文件不存在!一般是C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild");
                    return false;

                }

                CmdHelper.Execute(string.Format("\"{1}\" \"{0}\"", this.mPInfo.GetBuildXmlFile(), this.mPInfo.mMSBuildFile));

            }
            else
            {
                Output.Hint("\n\n");
                Output.Hint("*********************************");
                Output.Hint("***** 无需自动发布.NET   ********");
                Output.Hint("*********************************");
            }

            return true;
        }

        /// <summary>
        /// step2：压缩发布目标文件夹
        /// </summary>
        /// <returns></returns>
        bool ZipPublishTarget()
        {
            if (this.mPInfo.mIsZipPublishTarget == true)
            {
                Output.Hint("\n\n");
                Output.Hint("*********************************");
                Output.Hint("***** 开始压缩发布目标   ********");
                Output.Hint("*********************************");
                Output.Hint("\n\n");

                if (!Directory.Exists(this.mPInfo.mPublishTargetDirectory))
                {
                    Output.Error("发布目标文件夹不存在!");
                    return false;
                }

                if (File.Exists(this.mPInfo.mZipFile))
                {
                    File.Delete(this.mPInfo.mZipFile);
                }

                CmdHelper.Execute(string.Format("\"{0}\" a \"{1}\" {2}", this.mPInfo.mFile7Z, this.mPInfo.mZipFile, this.mPInfo.GetZipFileContent()));

            }
            else
            {
                Output.Hint("\n\n");
                Output.Hint("*********************************");
                Output.Hint("***** 无需压缩发布目标   ********");
                Output.Hint("*********************************");
            }

            return true;
        }

        /// <summary>
        /// step3：上传文件
        /// </summary>
        /// <returns></returns>
        bool UploadFile()
        {
            if (this.mPInfo.mIsUploadFile == true)
            {
                Output.Hint("\n\n");
                Output.Hint("*********************************");
                Output.Hint("***** 开始上传文件到服务器   ****");
                Output.Hint("*********************************");
                Output.Hint("\n\n");

                if (!File.Exists(this.mPInfo.mZipFile))
                {
                    Output.Error(string.Format("要上传的压缩包不存在:{0}", this.mPInfo.mZipFile));
                    return false;
                }

                //CmdHelper.Execute(string.Format("\"{0}\" a \"{1}\" \"{2}\"", this.mPInfo.mFile7Z, this.mPInfo.mZipFile, this.mPInfo.mPublishTargetDirectory));

                if (this.mClient != null)
                {
                    this.mClient.CloseSocket();
                    this.mClient = null;
                }

                try
                {
                    this.mClient = new SocketClient(this.mPInfo.mServerIP, this.mPInfo.mServerPort);

                    this.mClient.Evnet_ClientUploadError += mClient_Evnet_ClientUploadError;

                    this.mClient.EventClientReceiveCommandPacket += mClient_EventClientReceiveCommandPacket;
                    this.mClient.Evnet_ClientUploadSuccess += mClient_Evnet_ClientUploadSuccess;

                    this.mClient.ClientUploadFile(this.mPInfo.mZipFile, "/", this.mPInfo.mServerZipFileName, null);

                    this.mClient.StartSendFile(null);
                }
                catch (Exception ex)
                {
                    Output.Error(string.Format("Socket连接服务器出错"));
                    Output.Error(ex.ToString());
                }
            }
            else
            {
                Output.Hint("\n\n");
                Output.Hint("*********************************");
                Output.Hint("***** 无需上传文件到服务器   ********");
                Output.Hint("*********************************");
            }

            return true;
        }

        void mClient_Evnet_ClientUploadSuccess(object sender, ClientUploadSuccessArgs e)
        {
            Output.Hint("\n\n");
            Output.Hint("*********************************");
            Output.Hint("***** 上传服务器成功   ********");
            Output.Hint("*********************************");

            Output.Hint(string.Format("\n文件路径:{0}", this.mPInfo.mServerZipFileName));

            //上传文件成功了
            //开始其他处理
            //告诉服务器站点目录和不删除的目录
            JObject para = new JObject()
            {
                {"Type",1},
                {"ServerSitePath",this.mPInfo.mServerSitePath},
                {"ServerNoDelete",this.mPInfo.mServerNoDelete},
                {"ServerReceiveFileName",this.mPInfo.mServerZipFileName},
            };

            MsgCommand msg = new MsgCommand(EMessageType.M_Client_SendCommandInfo, para.ToString());
            this.mClient.SendMsg(msg);
            EsftMsg reveiceMsg = this.mClient.ReveiceMsg();

            if (reveiceMsg is MsgCommand)
            {

                Output.Result((reveiceMsg as MsgCommand).Command);
            }
        }

        void mClient_Evnet_ClientUploadError(object sender, ClientUploadErrorArgs e)
        {
            Output.Error("\n\n");
            Output.Error("*********************************");
            Output.Error("***** 上传服务器成功   ********");
            Output.Error("*********************************");
        }

        void mClient_EventClientReceiveCommandPacket(MsgCommand iMsg)
        {
            Output.Hint(iMsg.Command);
        }

    }
}
