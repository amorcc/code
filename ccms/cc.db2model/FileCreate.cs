using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using cc.utility.Data;

namespace cc.db2model
{
    public class FileCreate
    {
        CreateConfig mConfig;
        List<string> mTableNames;
        RichTextBox mRichTextBox;
        string mDBConnName;

        public FileCreate(List<string> iTableNames, CreateConfig iConfig, RichTextBox iRichTextBox, string iDBConnName)
        {
            this.mDBConnName = iDBConnName;
            this.mConfig = iConfig;
            this.mTableNames = iTableNames;
            this.mRichTextBox = iRichTextBox;
        }
        public void Start()
        {
            this.InitFileSavePath();
            System.Threading.Thread thread = new System.Threading.Thread(Run);
            thread.Start();
        }

        void Run(object obj)
        {
            if (this.mTableNames != null)
            {
                this.UpdateUI(string.Format("共选择了{0}个表", this.mTableNames.Count), System.Drawing.Color.Black);
                for (int i = 0; i < this.mTableNames.Count; i++)
                {
                    string item = this.mTableNames[i];
                    List<DBTableColumn> cols = QueryDB.GetTableColumnInfo(this.mDBConnName, item);

                    if (cols != null && cols.Count > 0)
                    {
                        #region 写Model类文件
                        string modelFileName = this.GetFileName(item) + "Info";
                        FileWrite fw = new FileWrite(this.mConfig.ModelFileSavePath + "\\" + modelFileName + ".cs");
                        #region 写using
                        fw.WriteUsing(this.mConfig.ModelUsing);
                        #endregion
                        fw.WriteLine(string.Format(""));
                        fw.WriteLine(string.Format("namespace {0}", this.mConfig.ModelNamespace));
                        fw.WriteLine("{");
                        fw.WriteLine(string.Format("    public class {0} : {1}", modelFileName, this.mConfig.ModelBaseClassName));

                        fw.WriteLine("    {");

                        foreach (var col in cols)
                        {
                            fw.WriteColumn(col);
                        }

                        fw.WriteLine("    }");
                        fw.WriteLine("}");

                        fw.WriteClose();
                        #endregion

                        #region 写Dal类文件
                        string dalFileName = this.GetFileName(item);
                        FileWrite fw2 = new FileWrite(this.mConfig.DalFileSavePath + "\\" + dalFileName + ".cs");
                        #region 写using
                        fw2.WriteUsing(this.mConfig.DalUsing + "using " + this.mConfig.ModelNamespace + ";");
                        #endregion
                        fw2.WriteLine(string.Format(""));
                        fw2.WriteLine(string.Format("namespace {0}", this.mConfig.DalNamespace));
                        fw2.WriteLine("{");
                        fw2.WriteLine(string.Format("    public class {0} : {1}<{2}>", dalFileName, this.mConfig.DalBaseClassName, modelFileName));

                        fw2.WriteLine("    {");

                        fw2.WriteLine(string.Format("        public {0}()", dalFileName));
                        fw2.WriteLine(string.Format("            : base()"));
                        fw2.WriteLine(string.Format("        {{"));
                        fw2.WriteLine(string.Format("            this._tableName = \"{0}\";", item));
                        fw2.WriteLine(string.Format("        }}"));
                        fw2.WriteLine(string.Format(""));


                        fw2.WriteLine(string.Format("        protected override {0} DataReaderToEntity(SqlDataReader dr)", modelFileName));
                        fw2.WriteLine(string.Format("        {{"));
                        fw2.WriteLine(string.Format("            var obj = new {0}();", modelFileName));
                        foreach (var col in cols)
                        {
                            fw2.WriteLine(string.Format("            obj.{0} = DataConvert.{1}(dr[\"{0}\"]);", col.ColumnName, DBTableColumn.GetCSharpDataConvert(col.ColumsType)));

                        }
                        fw2.WriteLine(string.Format("            return obj;"));
                        fw2.WriteLine(string.Format("        }}"));


                        fw2.WriteLine("    }");
                        fw2.WriteLine("}");

                        fw2.WriteClose();
                        #endregion

                        this.UpdateUI(string.Format("{1}/{2}:表{0}正在生成", item, i + 1, this.mTableNames.Count), System.Drawing.Color.Black);
                    }
                    else
                    {
                        this.UpdateUI(string.Format("表{0}未获取到列信息", item), System.Drawing.Color.Red);
                    }

                }
                this.UpdateUI(string.Format("*******全部生成成功"), System.Drawing.Color.Blue);
            }
        }

        void InitFileSavePath()
        {
            if (Directory.Exists(this.mConfig.ModelFileSavePath))
            {
                Directory.Delete(this.mConfig.ModelFileSavePath, true);
            }

            if (Directory.Exists(this.mConfig.DalFileSavePath))
            {
                Directory.Delete(this.mConfig.DalFileSavePath, true);
            }

            Directory.CreateDirectory(this.mConfig.ModelFileSavePath);
            Directory.CreateDirectory(this.mConfig.DalFileSavePath);
        }

        string GetFileName(string iTablename)
        {
            if (!string.IsNullOrEmpty(iTablename))
            {
                if (!string.IsNullOrEmpty(this.mConfig.TableNameFilter))
                {
                    iTablename = iTablename.Replace(this.mConfig.TableNameFilter, "");
                }
                iTablename = iTablename.Replace("_", "");
            }

            return iTablename;
        }

        public void UpdateUI(string iOutputStr, System.Drawing.Color iFontColor)
        {

            this.mRichTextBox.Invoke(new System.Action(delegate()
            {
                int start = this.mRichTextBox.Text.Length;
                this.mRichTextBox.Text += iOutputStr + "\n";

                this.mRichTextBox.Select(start, iOutputStr.Length);
                this.mRichTextBox.SelectionColor = iFontColor;
                this.mRichTextBox.SelectionStart = this.mRichTextBox.Text.Length;
            }));
        }
    }
}
