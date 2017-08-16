using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SSDC
{
    public partial class DataSourceMng : BaseForm
    {
        public DataSourceMng()
        {
            InitializeComponent();
        }

        public override void InitForm()
        {
            SSDC.DAL.datainfo datainfoDal = new DAL.datainfo();
            DataTable dataInfo = datainfoDal.GetDataInfo().Tables[0];

            this.comboBox1.DataSource = dataInfo;
            this.comboBox1.DisplayMember = "DataName";
            this.comboBox1.ValueMember = "ID";

            this.BindData(1, 1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectValue = this.comboBox1.SelectedValue.ToString();
            int tableNum = 1;

            int.TryParse(selectValue, out tableNum);

            this.BindData(tableNum, 1);
        }

        public void BindData(int iTableNum, int iPageIndex)
        {
            int pageRows = 0;
            this.dataGridView1.DataSource = SSDC.DAL.ContrastResult.GetTableX(iTableNum, iPageIndex, 15, out pageRows);

            this.pageControl1.SetValue(this.pageControl1.mPageIndex, pageRows);
        }

        private void pageControl1_PageChange(object sender, EventArgs e)
        {
            string selectValue = this.comboBox1.SelectedValue.ToString();
            int tableNum = 1;

            if (int.TryParse(selectValue, out tableNum))
            {
                this.BindData(tableNum, this.pageControl1.mPageIndex);
            }
        }
    }
}
