using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UC_WebAPI.LogMng
{
    public partial class log4net_init : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (cc.log.Log.IsLoadedLog4net() == false)
                {
                    this.Label3.Text = "log4net配置文件未加载！";
                }
                else
                {
                    this.Label3.Text = "log4net配置文件已加载！";
                }


            }
        }

        public void Button5_Click(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("\\log4net.config")));
            this.Label1.Text = "log4net初始化已完成！";
        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("\\log4net.config")));
            this.Label1.Text = "log4net初始化已完成！";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int aaa = 0;
                int bbb = 0;
                double ccc = aaa / bbb;
            }
            catch (Exception ex)
            {
                cc.log.Log.Error(typeof(log4net_init), ex);
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            cc.log.Log.Debug(typeof(log4net_init), null, "***********************************************");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                string n = this.TextBox1.Text.Trim();

                Assembly ass = Assembly.LoadFrom(Server.MapPath("\\") + "/BIN/" + n);

                this.Label2.Text = n + "当前版本号为：" + ass.GetName().Version.ToString();
            }
            catch
            {
                this.Label2.Text = "录入的dll文件名称错误，无法获取版本号！";
            }
        }



    }
}