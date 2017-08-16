using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSDC
{
    public partial class ModifyTableName : Form
    {
        public ModifyTableName()
        {
            InitializeComponent();
        }

        public string mTableName;
        public int mID;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text.Trim()))
            {
                MessageBox.Show(string.Format("数据源名称不能为空!"));
            }

            if (this.mID <= 0 || this.mID > 8)
            {
                MessageBox.Show("修改出错，请稍后重试");
                this.Close();
            }

            string tableName = this.textBox1.Text;

            SSDC.DAL.datainfo.UpdateDataName(this.mID, tableName);

            this.Close();
        }

        private void ModifyTableName_Load(object sender, EventArgs e)
        {
            this.textBox1.Text = this.mTableName;
        }

        private void ccButton8_BtnClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ccButton1_Load(object sender, EventArgs e)
        {

        }


    }
}
