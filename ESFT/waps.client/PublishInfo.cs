using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waps.client
{
    public class PublishInfo
    {
        ESFT.Common.IniFile mIniHelper = null;
        string mIniFile = string.Empty;

        /// <summary>
        /// 是否自动发布工程
        /// </summary>
        public bool mIsAutoPublish = false;

        /// <summary>
        /// 本地解决方案目录
        /// </summary>
        public string mSlnPath = string.Empty;

        /// <summary>
        /// MSBuild配置文件
        /// </summary>
        public string mBuildXmlFile = string.Empty;

        /// <summary>
        /// MSBuild文件路径
        /// </summary>
        public string mMSBuildFile = string.Empty;

        /// <summary>
        /// 发布目标位置目录
        /// </summary>
        public string mPublishTargetDirectory = string.Empty;

        /// <summary>
        /// 是否压缩发布目标
        /// </summary>
        public bool mIsZipPublishTarget = false;

        /// <summary>
        /// 7Z文件路径
        /// </summary>
        public string mFile7Z = string.Empty;

        /// <summary>
        /// 压缩保存文件路径
        /// </summary>
        public string mZipFile = string.Empty;

        /// <summary>
        /// 是否自动上传文件到服务器
        /// </summary>
        public bool mIsUploadFile = true;

        /// <summary>
        /// 服务器文件名称前缀
        /// </summary>
        public string mServerZipFileName = string.Empty;

        /// <summary>
        /// 服务器站点目录
        /// </summary>
        public string mServerSitePath = string.Empty;

        /// <summary>
        /// 服务器不删除的目录
        /// </summary>
        public string mServerNoDelete = string.Empty;

        /// <summary>
        /// 服务器IP
        /// </summary>
        public string mServerIP = string.Empty;
        /// <summary>
        /// 服务器端口号
        /// </summary>
        public int mServerPort = 8000;

        /// <summary>
        /// 获取压缩包里要压缩的文件
        /// </summary>
        /// <returns></returns>
        public string GetZipFileContent()
        {
            List<String> list = new List<string>();
            //遍历文件夹
            DirectoryInfo theFolder = new DirectoryInfo(this.mPublishTargetDirectory);
            FileInfo[] thefileInfo = theFolder.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo NextFile in thefileInfo)  //遍历文件
            {
                list.Add(NextFile.FullName);
            }
            //遍历子文件夹
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                list.Add(this.mPublishTargetDirectory + "\\" + NextFolder.ToString());
                //FileInfo[] fileInfo = NextFolder.GetFiles("*.*", SearchOption.AllDirectories);
                //foreach (FileInfo NextFile in fileInfo)  //遍历文件
                //    list.Add(NextFile.FullName);
            }

            //return string.Join(" ", list.ToArray());

            string rt = string.Empty;
            if (list != null)
            {
                foreach (var item in list)
                {
                    rt += "\"" + item + "\" ";
                }
            }

            return rt;
        }

        public string GetBuildXmlFile()
        {
            return this.mSlnPath + "\\" + this.mBuildXmlFile;
        }

        public PublishInfo(string iFileIniPath)
        {
            this.mIniFile = iFileIniPath;
        }

        public void ReadIniFile()
        {
            mIniHelper = new ESFT.Common.IniFile(this.mIniFile);

            this.mIsAutoPublish = Convert.ToBoolean(this.mIniHelper.ReadString("是否自动发布工程", "IsAutoPublish", "false"));
            this.mSlnPath = this.mIniHelper.ReadString("本地解决方案目录", "SlnPath", "");
            this.mBuildXmlFile = this.mIniHelper.ReadString("MSBuild配置文件", "BuildXmlFile", "");
            this.mMSBuildFile = this.mIniHelper.ReadString("MSBuild文件路径", "MSBuildFile", "");
            this.mIsZipPublishTarget = Convert.ToBoolean(this.mIniHelper.ReadString("是否压缩发布目标", "IsZipPublishTarget", "false"));
            this.mPublishTargetDirectory = this.mIniHelper.ReadString("发布目标位置目录", "PublishTargetDirectory", "");
            this.mZipFile = this.mIniHelper.ReadString("压缩保存文件路径", "ZipFile", "");
            this.mFile7Z = this.mIniHelper.ReadString("7Z文件路径", "File7Z", "");
            this.mIsUploadFile = Convert.ToBoolean(this.mIniHelper.ReadString("是否自动上传文件到服务器", "IsUploadFile", "false"));
            this.mServerIP = this.mIniHelper.ReadString("服务器IP", "ServerIP", "");
            this.mServerPort = Convert.ToInt32(this.mIniHelper.ReadString("服务器端口号", "ServerPort", "8000"));
            this.mServerZipFileName = this.mIniHelper.ReadString("服务器文件名称前缀", "ServerZipFileName", "servername") + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
            this.mServerSitePath = this.mIniHelper.ReadString("服务器站点目录", "ServerSitePath", "");
            this.mServerNoDelete = this.mIniHelper.ReadString("服务器不删除的目录", "ServerNoDelete", "");

        }

        public void WriteIniFile()
        {
            Output.Hint("\n\n");
            Output.Hint("***************************************");
            Output.Hint("*************配置文件信息**************");
            Output.Hint("***************************************");
            Output.Hint("\n");
            Output.Hint(string.Format("\t是否自动发布工程:\t\t{0}", this.mIsAutoPublish));
            Output.Hint(string.Format("\t本地解决方案目录:\t\t{0}", this.mSlnPath));
            Output.Hint(string.Format("\tMSBuild配置文件:\t\t{0}", this.mBuildXmlFile));
            Output.Hint(string.Format("\t是否压缩发布目标:\t\t{0}", this.mIsZipPublishTarget));
            Output.Hint(string.Format("\t发布目标位置目录:\t\t{0}", this.mPublishTargetDirectory));
            Output.Hint(string.Format("\t是否上传文件:\t\t\t{0}", this.mIsUploadFile));
            Output.Hint(string.Format("\t服务器IP:\t\t\t{0}", this.mServerIP));
            Output.Hint(string.Format("\t服务器端口号:\t\t\t{0}", this.mServerPort));
            Output.Hint(string.Format("\t服务器文件名称前缀:\t\t{0}", this.mServerZipFileName));
            Output.Hint(string.Format("\t服务器站点目录:\t\t{0}", this.mServerSitePath));
            Output.Hint(string.Format("\t服务器不删除的目录:\t\t{0}", this.mServerNoDelete));
            //Output.Hint(string.Format("\t要压缩的文件内容:\t\t{0}", this.GetZipFileContent()));

            Output.Hint(string.Format("\tMSBuild文件路径:\t\t{0}", this.mMSBuildFile));
            Output.Hint(string.Format("\t7Z文件路径:\t\t\t{0}", this.mFile7Z));
        }
    }
}
