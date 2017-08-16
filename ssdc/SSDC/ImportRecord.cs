using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SSDC
{
    public partial class ImportRecord : BaseForm
    {
        public ImportRecord()
        {
            InitializeComponent();
        }

        private void ImportRecord_Load(object sender, EventArgs e)
        {

        }

        public void Init()
        {
            DataTable dt = SSDC.DAL.ImportRecord.GetAll();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][0] = (i + 1);
            }

            this.dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
    }
}
