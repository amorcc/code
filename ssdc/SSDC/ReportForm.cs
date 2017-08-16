using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSDC
{
    public partial class ReportForm : BaseForm
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        public override void InitForm()
        {
            base.InitForm();
            this.button2_Click(null, null);
        }

        public int mCurrentReport = 1;

        #region 总体情况
        private void button2_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "总体情况重复数据分析结果";
            this.mCurrentReport = 1;
            this.ReadData(1, this.pageControl1.mPageSize);
        }
        #endregion

        #region 居民+职工+新农合
        private void button3_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "居民+职工+新农合重复数据分析结果";
            this.mCurrentReport = 2;
            this.ReadData(1, this.pageControl1.mPageSize);
        }
        #endregion

        #region 居民+职工
        private void button4_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "居民+职工重复数据分析结果";
            this.mCurrentReport = 3;
            this.ReadData(1, this.pageControl1.mPageSize);
        }
        #endregion

        #region 居民+新农合
        private void button5_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "居民+新农合重复数据分析结果";
            this.mCurrentReport = 4;
            this.ReadData(1, this.pageControl1.mPageSize);
        }
        #endregion

        #region 职工+新农合
        private void button6_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "职工+新农合重复数据分析结果";

            this.mCurrentReport = 5;
            this.ReadData(1, this.pageControl1.mPageSize);
        }
        #endregion

        #region 居民
        private void button7_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "居民重复数据分析结果";

            this.mCurrentReport = 6;
            this.ReadData(1, this.pageControl1.mPageSize);
        }
        #endregion

        #region 职工
        private void button8_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "职工重复数据分析结果";

            this.mCurrentReport = 7;
            this.ReadData(1, this.pageControl1.mPageSize);
        }
        #endregion

        #region 新农合
        private void button9_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "新农合重复数据分析结果";

            this.mCurrentReport = 8;
            this.ReadData(1, this.pageControl1.mPageSize);
        }
        #endregion

        #region 身份证非法

        private void button10_Click(object sender, EventArgs e)
        {
            this.lbTitle.Text = "身份证非法的人员数据信息";
            this.mCurrentReport = 10;
            this.ReadData(1, this.pageControl1.mPageSize);
            this.ccButton5.Visible = true;
        }
        #endregion

        DataTable ReadData(int iPageIndex, int iPageSize)
        {
            this.ccButton5.Visible = false;
            int pageRows = 0;
            DataTable dt = null;
            switch (this.mCurrentReport)
            {
                case 1:
                    //总体
                    dt = SSDC.DAL.ContrastResult.ResultAll(iPageIndex, iPageSize, out pageRows);
                    break;
                case 2:
                    //123
                    dt = SSDC.DAL.ContrastResult.Result123(iPageIndex, iPageSize, out pageRows);
                    break;
                case 3:
                    //12
                    dt = SSDC.DAL.ContrastResult.Result12(iPageIndex, iPageSize, out pageRows);
                    break;
                case 4:
                    //13
                    dt = SSDC.DAL.ContrastResult.Result13(iPageIndex, iPageSize, out pageRows);
                    break;
                case 5:
                    //23
                    dt = SSDC.DAL.ContrastResult.Result23(iPageIndex, iPageSize, out pageRows);
                    break;
                case 6:
                    //1
                    dt = SSDC.DAL.ContrastResult.Result1(iPageIndex, iPageSize, out pageRows);
                    break;
                case 7:
                    //1
                    dt = SSDC.DAL.ContrastResult.Result2(iPageIndex, iPageSize, out pageRows);
                    break;
                case 8:
                    //1
                    dt = SSDC.DAL.ContrastResult.Result3(iPageIndex, iPageSize, out pageRows);
                    break;
                case 9:
                    //1
                    dt = SSDC.DAL.ContrastResult.Result1(iPageIndex, iPageSize, out pageRows);
                    break;
                case 10:
                    dt = SSDC.DAL.ContrastResult.ResultIdNumber(iPageIndex, iPageSize, out pageRows);
                    break;
                default:
                    break;
            }
            this.dataGridView1.DataSource = dt;
            this.pageControl1.SetValue(iPageIndex, pageRows);

            return dt;
        }

        private void pageControl1_PageChange(object sender, EventArgs e)
        {
            //MessageBox.Show(this.pageControl1.mPageIndex.ToString());
            this.ReadData(this.pageControl1.mPageIndex, this.pageControl1.mPageSize);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要分析身份证信息吗？该操作可能需要耗费较长时间.", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                AnalysisDataHint hint = new AnalysisDataHint();
                hint.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pHintExport.Visible = true;
            DataTable dt = this.ReadData(1, 65535);

            if (dt != null && dt.Columns.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = this.dataGridView1.Columns[i].HeaderText;
                }
            }

            //SSDC.Common.ReadExcel.ExportExcel(dt);
            doExport(dt);

            this.pHintExport.Visible = false;
        }

        public void doExport(DataTable dt)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Export Excel File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName == "")
            {
                return;
            }

            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            string str = "";

            try
            {
                //写标题
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dt.Columns[i].ToString().Trim();
                }
                sw.WriteLine(str);

                //写内容
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }

                        tempStr += "" + dt.Rows[j][k].ToString();
                    }
                    sw.WriteLine(tempStr);
                }

                sw.Close();
                myStream.Close();
            }

            catch (Exception ex)
            {
                sw.Close();
                myStream.Close();
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }

        private void ccButton1_Click(object sender, EventArgs e)
        {

        }

        private void ccButton1_BtnClick(object sender, EventArgs e)
        {

            this.pHintExport.Visible = true;
            this.pHintExport.Refresh();
            DataTable dt = this.ReadData(1, 65535);

            if (dt != null && dt.Columns.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = this.dataGridView1.Columns[i].HeaderText;
                }
            }

            //SSDC.Common.ReadExcel.ExportExcel(dt);
            doExport(dt);

            this.pHintExport.Visible = false;
        }

        private void ccButton1_BtnClick()
        {

        }

        private void ccButton1_Load(object sender, EventArgs e)
        {

        }

        private void ccButton3_Load(object sender, EventArgs e)
        {

        }
    }
}
