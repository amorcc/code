using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSDC.MyControl
{
    public partial class PageControl : UserControl
    {
        public PageControl()
        {
            InitializeComponent();
        }

        public int mTotalRows;
        public int mTotalPages;
        public int mPageSize = 50;
        public int mPageIndex = 1;

        public void SetValue(int iPageIndex, int iTotalRows)
        {
            this.mPageIndex = iPageIndex;
            this.mTotalRows = iTotalRows;

            this.mTotalPages = this.mTotalRows / this.mPageSize + (this.mTotalRows % this.mPageSize == 0 ? 0 : 1);
            this.label1.Text = string.Format("共{0}页，{1}条数据", this.mTotalPages, this.mTotalRows);

            this.cBoxPageNum.Items.Clear();

            for (int i = 0; i < this.mTotalPages; i++)
            {
                this.cBoxPageNum.Items.Add(i + 1);
            }

            this.cBoxPageNum.SelectedIndexChanged -= cBoxPageNum_SelectedIndexChanged;
            this.cBoxPageNum.Text = iPageIndex.ToString();

            this.cBoxPageNum.SelectedIndexChanged += cBoxPageNum_SelectedIndexChanged;
        }

        [Browsable(true), Description("选择按钮单击事件。"), Category("操作")]
        public event EventHandler PageChange;
        protected virtual void OnPageChange(object sender, EventArgs e)
        {
            if (PageChange != null)
            {
                PageChange(this, e);
            }
        }

        private void PageControl_Load(object sender, EventArgs e)
        {
            this.PageChange += PageControl_PageChange;
        }

        void PageControl_PageChange(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.mPageIndex > 1)
            {
                this.mPageIndex--;
                this.cBoxPageNum.Text = this.mPageIndex.ToString();
                //this.OnPageChange(sender, e);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.mPageIndex < this.mTotalRows)
            {
                this.mPageIndex++;
                this.cBoxPageNum.Text = this.mPageIndex.ToString();
                //this.OnPageChange(sender, e);
            }
        }

        private void cBoxPageNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mPageIndex = Convert.ToInt32(this.cBoxPageNum.Text);
            this.OnPageChange(sender, e);
        }
    }
}
