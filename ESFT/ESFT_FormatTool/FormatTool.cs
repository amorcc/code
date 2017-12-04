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

namespace ESFT_FormatTool
{
    public partial class FormatTool : Form
    {
        public FormatTool()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            if (openDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = openDlg.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            if (folderDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox2.Text = folderDlg.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != string.Empty
                && File.Exists(this.textBox1.Text))
            {
                FileType fileType = FileType.Other;
                string iExtension = Path.GetExtension(this.textBox1.Text);

                if (iExtension.ToUpper() == ".JPG"
                               || iExtension.ToUpper() == ".PNG"
                               || iExtension.ToUpper() == ".JPEG"
                               || iExtension.ToUpper() == ".BMP")
                {
                    fileType = FileType.Image;
                }
                else if (iExtension.ToUpper() == ".MP4"
                        || iExtension.ToUpper() == ".AVI"
                        || iExtension.ToUpper() == ".FLV"
                        || iExtension.ToUpper() == ".WMV"
                        || iExtension.ToUpper() == ".3GP"
                        || iExtension.ToUpper() == ".RMVB"
                        || iExtension.ToUpper() == ".MOV"
                        )
                {
                    fileType = FileType.Video;
                }
                else
                {
                    MessageBox.Show("不支持您选择的文件格式转换，只支持 JPG PNG JPEG BMP MP4 AVI FLV WMV 3GP RMVB MOV");
                    return;
                }

                Format.FormatFile(fileType, this.textBox1.Text, this.textBox2.Text, System.IO.Path.GetFileName(this.textBox1.Text));
            }
            else
            {
                MessageBox.Show("文件不存在");
                return;
            }

            MessageBox.Show("转换完成");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            if (openDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox3.Text = openDlg.FileName;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string md5 = ESFT.Common.FileHash.GetMD5HashFromFile(this.textBox3.Text);

            this.textBox4.Text += md5 + " ,  " + this.textBox3.Text + "\r\n";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.textBox7.Text += System.Guid.NewGuid().ToString() + "\r\n";
        }
    }
}
