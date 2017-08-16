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
    public partial class AnalysisIdNumberHint : Form
    {
        public AnalysisIdNumberHint()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SSDC.Common.DirectDbHelperMysql.ExecutProcedure("AnalysisIdNumber");

            this.timer1.Enabled = false;
            MessageBox.Show("身份证信息分析完成");
            this.Close();
        }
    }
}
