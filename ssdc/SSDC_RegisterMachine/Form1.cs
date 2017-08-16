using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SSDC_RegisterMachine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = RegisterMachine.GetRegisterCode(this.textBox1.Text, this.dateTimePicker1.Value);


            string code = SSDC.Common.DES.Decrypt(this.textBox2.Text);
        }
    }
}
