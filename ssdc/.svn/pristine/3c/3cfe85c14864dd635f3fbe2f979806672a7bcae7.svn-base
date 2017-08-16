using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SSDC
{
    public partial class AnalysisDataHint : Form
    {
        public AnalysisDataHint()
        {
            InitializeComponent();

        }

        private void AnalysisDataHint_Load(object sender, EventArgs e)
        {

            //System.Threading.Thread.Sleep(2000);
            //this.Close();
            this.timer1.Enabled = true;
            this.timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SSDC.Common.DirectDbHelperMysql.ExecutProcedure("Analysis");
            this.timer2.Enabled = true;
            this.timer2.Start();

            this.timer1.Enabled = false;
            MessageBox.Show("数据分析完成，请点击分析结果，以查看数据");
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        }
    }
}
