using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SSDC
{
    public partial class ImportProgress3 : Form
    {
        public ImportProgress3()
        {
            InitializeComponent();
        }

        private void ImportProgress_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 城镇居民医保datatable
        /// </summary>
        public DataTable mTable = null;


        public string mColName = string.Empty;

        public string mColId = string.Empty;

        public int mDatainfoType = 0;

        /// <summary>
        /// 数据总条数
        /// </summary>
        int rowCount = 0;

        /// <summary>
        /// 已完成条数
        /// </summary>
        int completedCount = 0;

        /// <summary>
        /// 是否导入中
        /// </summary>
        bool importing = false;

        /// <summary>
        /// 开始导入时间
        /// </summary>
        DateTime beginImportTime = DateTime.Now;

        /// <summary>
        /// 本次导入批次唯一标识
        /// </summary>
        string thisImportBatch = System.Guid.NewGuid().ToString();

        /// <summary>
        /// 1-城镇居民医保,2-城镇职工医保,3-新农合医保
        /// </summary>
        public int mImportType = 0;

        /// <summary>
        /// 导入多少条数据开始更新界面
        /// </summary>
        int importRowCountUpdateUI = 1;

        public void InitCount()
        {
            #region 获取数据
            rowCount += this.mTable.Rows.Count;
            #endregion

            this.label1.Text = "0/" + rowCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.button1.Text == "导入完成")
            {
                this.Close();
            }

            if (importing == false)
            {
                importing = true;
                this.button1.Text = "停止导入";
                Thread t = new Thread(StartImportToDB);
                t.Start();
            }
            else
            {
                this.importing = false;
                this.button1.Text = "开始导入";
            }

        }

        int count = 0;

        public void StartImportToDB()
        {
            DateTime dtStart = DateTime.Now;
            if (this.completedCount == 0)
            {
                //刚开始导入
                this.beginImportTime = DateTime.Now;

                //新增导入记录
                SSDC.DAL.ImportRecord.Insert(this.rowCount, this.beginImportTime, this.thisImportBatch, this.mImportType);

                //更新UI
                this.UpdateUI();
            }

            //导入城镇居民医保
            this.Import(this.mDatainfoType, this.mTable, this.mColName, this.mColId);

            SSDC.DAL.datainfo.SetDataInfoColumnsNum(this.mImportType);

            this.CompletedImport();

            DateTime dtEnd = DateTime.Now;

            TimeSpan ts = dtEnd - dtStart;

            this.label3.Invoke(new System.Action(delegate()
            {
                this.label3.Text = ts.TotalSeconds.ToString();
            }));
        }

        /// <summary>
        /// 导入表数据
        /// </summary>
        /// <param name="iType">1-城镇居民医保,2-城镇职工医保,3-新农合医保</param>
        /// <param name="iDT"></param>
        /// <param name="iNameCol"></param>
        /// <param name="iIdNumberCol"></param>
        public void Import(int iType, DataTable iDT, string iNameCol, string iIdNumberCol)
        {
            this.mImportType = iType;
            string importTitle = string.Empty;

            if (iDT != null && iDT.Rows.Count > 0)
            {
                for (int i = 0; i < iDT.Rows.Count; i++)
                {
                    if (this.importing == true)
                    {

                        SSDC.DAL.czjmyb.InsertByDataRow(this.mDatainfoType, iDT.Rows[i], iNameCol, iIdNumberCol);


                        this.completedCount++;

                        if (this.completedCount % importRowCountUpdateUI == 0)
                        {
                            this.UpdateUI();
                        }
                    }
                    else
                    {
                        this.CancelImport();
                    }
                }
            }
        }

        /// <summary>
        /// 取消导入
        /// </summary>
        public void CancelImport()
        {

        }

        /// <summary>
        /// 导入完成
        /// </summary>
        public void CompletedImport()
        {
            this.button1.Invoke(new System.Action(delegate()
            {
                this.button1.Text = "导入完成";
            }));
            MessageBox.Show("导入完成");
        }

        public void UpdateUI()
        {
            //Thread.Sleep(100);

            int progress = Convert.ToInt32(Convert.ToDouble(this.completedCount) / Convert.ToDouble(this.rowCount) * 100);
            this.progressBar1.Invoke(new System.Action(delegate()
            {
                this.progressBar1.Value = progress;
            }));

            string importTitle = string.Empty;

            importTitle = "正在导入数据……";


            this.label2.Invoke(new System.Action(delegate()
            {
                this.label2.Text = importTitle;
            }));

            this.label1.Invoke(new System.Action(delegate()
            {
                this.label1.Text = this.completedCount.ToString() + "/" + this.rowCount.ToString();
            }));
        }

        private void ImportProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.importing = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
