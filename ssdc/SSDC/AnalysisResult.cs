using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SSDC
{
    public partial class AnalysisResult : Form
    {
        public AnalysisResult()
        {
            InitializeComponent();
        }

        private void AnalysisResult_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = SSDC.DAL.AnalysisResult.GetRepeatInfo();
            //this.dataGridView1.DataBindings();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || !(sender is DataGridView))
            {
                return;
            }

            if (e.Value.ToString() == "是")
            {
                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Red;
            }

            DataGridView view = (DataGridView)sender;
            object originalValue = e.Value;

            if (view.Columns[e.ColumnIndex].DataPropertyName == "sex")
                e.Value = ((int)originalValue == 1) ? "男" : "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)this.dataGridView1.DataSource;

            if (dt != null && dt.Columns.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = this.dataGridView1.Columns[i].HeaderText;
                }
            }

            SSDC.Common.ReadExcel.ExportExcel(dt);
        }


    }
}
