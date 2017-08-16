using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SSDC
{
    public partial class SettingForm : BaseForm
    {
        public SettingForm()
        {
            InitializeComponent();

            this.lbVersion.Text = typeof(SSDC.MainForm).Assembly.GetName().Version.ToString();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string errorMsg = string.Empty;
            bool result = SSDC.Common.Register.RegisterSoft(this.txtRegCode.Text.Trim(), out errorMsg);

            if (result)
            {
                MessageBox.Show("注册成功！");
            }
            else
            {
                MessageBox.Show("注册失败，请联系管理员！");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SSDC.Common.Register.CheckRegister();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {

        }

        public override void InitForm()
        {
            if (SSDC.Common.Register.CheckRegister() == true)
            {
                this.lbRegStatus.Text = "已经注册";
                this.lbRegDate.Text = SSDC.Common.Register.GetRegDate().ToString("yyyy年MM月dd日 HH:mm:ss");
                this.textBox1.Text = SSDC.Common.Register.GetMachineInfo();
            }
            else
            {
                this.lbRegStatus.Text = "尚未注册，请立即注册！";
                this.lbRegDate.Text = "无";
                this.textBox1.Text = SSDC.Common.Register.GetMachineInfo();
            }
        }
    }
}
