using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSDC
{
    public partial class CCButton : UserControl
    {
        public CCButton()
        {
            InitializeComponent();
        }

        Color _TextColor = Color.White;

        [Browsable(true)]
        [Description("按下背景"), Category("按下背景"), DefaultValue("")]
        public Color TextColor
        {
            set
            {
                this._TextColor = value;
            }
            get
            {
                return this._TextColor;
            }
        }

        int _TextTop = 0;

        [Browsable(true)]
        [Description("按下背景"), Category("按下背景"), DefaultValue("")]
        public int TextTop
        {
            set
            {
                this._TextTop = value;
            }
            get
            {
                return this._TextTop;
            }
        }


        public string _BtnText = "";

        [Browsable(true)]
        [Description("按下背景"), Category("按下背景"), DefaultValue("")]
        public string BtnText
        {
            set
            {
                this._BtnText = value;
            }
            get
            {
                return this._BtnText;
            }
        }

        public int _GroupNum;

        [Browsable(true)]
        [Description("按下背景"), Category("按下背景"), DefaultValue("")]
        public int GroupNum
        {
            set
            {
                this._GroupNum = value;
            }
            get
            {
                return this._GroupNum;
            }
        }

        public bool _Down;

        [Browsable(true)]
        [Description("按下背景"), Category("按下背景"), DefaultValue("")]
        public bool Down
        {
            set
            {
                this._Down = value;

                if (this._Down == true)
                {
                    this.BackgroundImage = this.DownBgColor;
                }
                else
                {
                    this.BackgroundImage = this.NormalBgColor;
                }
            }
            get
            {
                return this._Down;
            }
        }

        public Image _DownBgColor;

        [Browsable(true)]
        [Description("按下背景"), Category("按下背景"), DefaultValue("")]
        public Image DownBgColor
        {
            set
            {
                this._DownBgColor = value;
            }
            get
            {
                return this._DownBgColor;
            }
        }

        public Image _NormalBgColor;

        [Browsable(true)]
        [Description("正常背景"), Category("正常背景"), DefaultValue("")]
        public Image NormalBgColor
        {
            set
            {
                this._NormalBgColor = value;
            }
            get
            {
                return this._NormalBgColor;
            }
        }

        public Image _MouseMoveBgColor;

        [Browsable(true)]
        [Description("鼠标移上背景"), Category("鼠标移上背景"), DefaultValue("")]
        public Image MouseMoveBgColor
        {
            set
            {
                this._MouseMoveBgColor = value;
            }
            get
            {
                return this._MouseMoveBgColor;
            }
        }

        private void CCButton_Load(object sender, EventArgs e)
        {

        }

        private void CCButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (this._Down == false)
            {
                this.BackgroundImage = this.MouseMoveBgColor;
            }
        }

        private void CCButton_MouseLeave(object sender, EventArgs e)
        {
            if (this._Down == false)
            {
                this.BackgroundImage = this.NormalBgColor;
            }
        }

        private void CCButton_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.Parent.Controls)
            {
                if (item is CCButton)
                {
                    CCButton btn = (CCButton)item;

                    if (btn.Down == true && btn.GroupNum == this.GroupNum)
                    {
                        btn.Down = false;
                    }
                }
            }

            this.Down = true;

            OnBtnClick(sender, e);
        }

        //委托
        public delegate void BtnClickEventHandler(object sender, EventArgs e);
        //事件
        public event BtnClickEventHandler BtnClick;
        //方法
        public void OnBtnClick(object sender, EventArgs e)
        {
            if (BtnClick != null)
                BtnClick(sender, e);
        }

        private void CCButton_Load_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BtnText.Trim()))
            {
                this.label1.Visible = false;
            }
            else
            {
                this.label1.Text = BtnText;
                this.label1.ForeColor = this.TextColor;
                this.label1.Top = this.TextTop;
            }


            if (this.NormalBgColor != null)
            {
                if (this._Down == true)
                {
                    this.BackgroundImage = this.DownBgColor;
                }
                else
                {
                    this.BackgroundImage = this.NormalBgColor;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.Parent.Controls)
            {
                if (item is CCButton)
                {
                    CCButton btn = (CCButton)item;

                    if (btn.Down == true && btn.GroupNum == this.GroupNum)
                    {
                        btn.Down = false;
                    }
                }
            }

            this.Down = true;

            OnBtnClick(sender, e);
        }
    }
}
