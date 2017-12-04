using cc.utility.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.db2model
{
    public class FileWrite
    {
        string mFileFullName;
        StreamWriter mStreamWriter;

        public FileWrite(string iFileFullName)
        {
            this.mFileFullName = iFileFullName;
            if (File.Exists(iFileFullName))
            {
                File.Delete(iFileFullName);
            }

            FileStream fs = File.Create(iFileFullName);

            fs.Close();
            fs.Dispose();

            this.mStreamWriter = new StreamWriter(iFileFullName, true, Encoding.UTF8);

        }

        public void WriteColumn(DBTableColumn iColInfo)
        {
            if (iColInfo.ColumnName == "Id" || iColInfo.ColumnName == "DateAdded")
            {
                return;
            }

            if (!string.IsNullOrEmpty(iColInfo.ColumnDesc))
            {
                this.mStreamWriter.WriteLine(string.Format("        /// <summary>"));
                this.mStreamWriter.WriteLine(string.Format("        /// {0}", iColInfo.ColumnDesc));
                this.mStreamWriter.WriteLine(string.Format("        /// </summary>"));
            }

            this.mStreamWriter.WriteLine(string.Format("        public {0} {1} {{ get; set; }}", DBTableColumn.GetCSharpTypeStr(iColInfo.ColumsType), iColInfo.ColumnName));

            this.mStreamWriter.WriteLine(string.Format(""));
        }

        public void WriteUsing(string iUsing)
        {
            if (!string.IsNullOrEmpty(iUsing))
            {
                List<string> usingList = iUsing.Split(';').ToList();

                if (usingList != null)
                {
                    foreach (var line in usingList)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            this.mStreamWriter.WriteLine(line + ";");
                        }
                    }
                }
            }
        }

        public void WriteLine(string iLine)
        {
            this.mStreamWriter.WriteLine(iLine);
        }


        public void WriteClose()
        {
            this.mStreamWriter.Close();
        }
    }
}
