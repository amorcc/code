using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SSDC
{
    public partial class DataQuery : BaseForm
    {
        public DataQuery()
        {
            InitializeComponent();
        }

        DataTable mDataInfo = null;

        public override void InitForm()
        {
            SSDC.DAL.datainfo datainfoDal = new DAL.datainfo();
            this.mDataInfo = datainfoDal.GetDataInfo().Tables[0];
            if (this.mDataInfo != null && this.mDataInfo.Rows.Count > 0)
            {
                foreach (DataRow row in this.mDataInfo.Rows)
                {
                    string id = row["Id"].ToString();
                    string dataName = row["DataName"].ToString();

                    this.checkedListBox1.Items.Add(dataName, true);
                }

            }
        }

        public void ClearData()
        {
            this.textBox1.Text = "";
        }

        public void QueryIdNumber(string iIdNumber, bool iShowReturn, string iName, int iPageIndex)
        {
            this.pHintExport.Visible = true;
            this.pChildMain.Refresh();
            this.textBox1.Text = iIdNumber;
            //this.textBox2.Text = iName;

            string idNumber = this.textBox1.Text;
            this.textBox2.Text = iName;

            this.btnReturn.Visible = iShowReturn;

            //DataTable dt = SSDC.DAL.ContrastResult.GetIdNumberInfo(idNumber, iName);

            int totalRows = 0;
            DataTable dt = this.QueryDataByDB(iName, idNumber, iPageIndex, this.pageControl1.mPageSize, out totalRows);
            this.dataGridView1.DataSource = dt;

            this.pageControl1.SetValue(iPageIndex, totalRows);

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("没查询到数据！");
            }
            this.pHintExport.Visible = false;
        }

        public DataTable QueryDataByDB(string iName, string iIdNumber, int iPageIndex, int iPageSize, out int oPageRows)
        {
            oPageRows = 0;
            string sqlStr = string.Format("CALL QueryData('{2}','{3}',{0},{1})", iPageIndex, iPageSize, iName, iIdNumber);

            DataSet ds = SSDC.Common.DirectDbHelperMysql.Query(sqlStr);

            DataTable data = ds.Tables[0];
            DataTable dt2 = ds.Tables[1];

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                var rowCount = dt2.Rows[0][0];
                oPageRows = Convert.ToInt32(rowCount != DBNull.Value && rowCount != null ? rowCount : 0);
            }

            return data;

            //return ModifyColumnName2(data);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.QueryIdNumber(this.textBox1.Text, false, this.textBox2.Text, 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.pChildMain.Visible = false;
        }

        private void ccButton1_Load(object sender, EventArgs e)
        {

        }

        private void pageControl1_PageChange(object sender, EventArgs e)
        {
            this.QueryIdNumber(this.textBox1.Text, false, this.textBox2.Text, this.pageControl1.mPageIndex);
        }
    }
}
