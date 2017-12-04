using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace cc.model2db
{
    public class AppendRichTextBox
    {
        public static void Append(System.Windows.Forms.RichTextBox iRichTextBox, string iOutputStr)
        {
            iRichTextBox.Invoke(new System.Action(delegate()
            {
                int start = iRichTextBox.Text.Length;
                iRichTextBox.Text += iOutputStr + "\n";

                iRichTextBox.Select(start, iOutputStr.Length);
                iRichTextBox.SelectionColor = Color.Black;
                iRichTextBox.SelectionStart = iRichTextBox.Text.Length;
            }));
        }

        public static void Append(System.Windows.Forms.RichTextBox iRichTextBox, string iOutputStr, Color iFontColor)
        {
            iRichTextBox.Invoke(new System.Action(delegate()
            {
                int start = iRichTextBox.Text.Length;
                iRichTextBox.Text += iOutputStr + "\n";

                iRichTextBox.Select(start, iOutputStr.Length);
                iRichTextBox.SelectionColor = iFontColor;
                iRichTextBox.SelectionStart = iRichTextBox.Text.Length;
            }));
        }

        public static void AppendSQL(System.Windows.Forms.RichTextBox iRichTextBox, string iOutputStr)
        {
            iRichTextBox.Invoke(new System.Action(delegate()
            {
                int start = iRichTextBox.Text.Length;

                if (!string.IsNullOrEmpty(iOutputStr))
                {
                    iOutputStr = iOutputStr.Replace("               ", "\n\t\t");
                    iOutputStr = iOutputStr.Replace("      ", "\n\t");
                    iOutputStr = iOutputStr.Replace(");", "\n);");
                    iOutputStr = iOutputStr.Replace(";", ";\n");
                }

                iRichTextBox.Text += iOutputStr + "\n";

                iRichTextBox.Select(start, iOutputStr.Length);
                iRichTextBox.SelectionColor = Color.Blue;
                iRichTextBox.SelectionStart = iRichTextBox.Text.Length;
            }));
        }
    }
}
