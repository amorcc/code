using SSDC.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SSDC
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Init();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sss = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            MessageBox.Show(sss);

            DirectDbHelperMysql.CanConnectionMysql();
        }

        public void OpenFormInMain(BaseForm iBaseForm)
        {
            this.MainArea.Controls.Add(iBaseForm.pChildMain);
            iBaseForm.InitForm();
            iBaseForm.pChildMain.BringToFront();

        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (SSDC.Common.Register.CheckRegister() == false)
            {
                MessageBox.Show("软件尚未注册，无法导入数据，请联系管理员!");
                return;
            }

            importDataFrom.InitForm();
            importDataFrom.pChildMain.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SSDC.Common.DirectDbHelperMysql.ExecutProcedure("Analysis");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            crForm.InitForm();
            crForm.pChildMain.BringToFront();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要重新分析数据吗？该操作可能需要耗费较长时间.", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                AnalysisDataHint hint = new AnalysisDataHint();
                hint.ShowDialog();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            button10_Click(null, null);
        }

        ContrastResult crForm;
        ReportForm reportForm;
        ImportData importDataFrom;

        ImportRecord importRecordFrom;
        DataQuery dataQueryFrom;

        DataSourceMng dataSourceMngFrom;
        SettingForm settingForm;

        public void Init()
        {
            importDataFrom = new ImportData();
            this.OpenFormInMain(importDataFrom);
            crForm = new ContrastResult();

            //crForm.dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            //this.OpenFormInMain(crForm);

            importRecordFrom = new ImportRecord();
            this.OpenFormInMain(importRecordFrom);

            dataQueryFrom = new DataQuery();
            dataQueryFrom.InitForm();
            dataQueryFrom.btnReturn.Click += button3_Click_1;
            this.OpenFormInMain(dataQueryFrom);

            dataSourceMngFrom = new DataSourceMng();
            this.OpenFormInMain(dataSourceMngFrom);

            settingForm = new SettingForm();
            this.OpenFormInMain(settingForm);

            reportForm = new ReportForm();
            reportForm.dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            this.OpenFormInMain(reportForm);
        }

        void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var cellValue = reportForm.dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            string idNumber = "";
            if (cellValue != null)
            {
                idNumber = cellValue.ToString();
            }

            this.ccButton1.Down = false;
            this.ccButton4.Down = true;
            dataQueryFrom.pChildMain.Visible = true;
            dataQueryFrom.pChildMain.BringToFront();
            dataQueryFrom.QueryIdNumber(idNumber, true, "", 1);
            //SSDC.DAL.ContrastResult.GetIdNumberInfo(idNumber);


        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            importRecordFrom.Init();

            importRecordFrom.pChildMain.BringToFront();
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            dataQueryFrom.ClearData();
            dataQueryFrom.pChildMain.Visible = true;
            dataQueryFrom.pChildMain.BringToFront();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataSourceMngFrom.pChildMain.BringToFront();
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            settingForm.InitForm();
            settingForm.pChildMain.BringToFront();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SSDC.Common.Register.WriteLastDate();
        }

        #region 移动窗体
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        #endregion

        private void panel18_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void panel18_DoubleClick(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            reportForm.InitForm();
            reportForm.pChildMain.BringToFront();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Computer computer = new Computer();
            string macId = computer.GetMacAddress();

            MessageBox.Show(macId);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ccButton1_Load(object sender, EventArgs e)
        {

        }

        private void ccButton4_Load(object sender, EventArgs e)
        {

        }

        private void ccButton3_Load(object sender, EventArgs e)
        {

        }

    }
}
