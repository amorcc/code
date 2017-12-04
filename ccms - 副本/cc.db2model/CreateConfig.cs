using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cc.db2model
{
    /// <summary>
    /// 生成时的配置信息
    /// </summary>
    public class CreateConfig
    {
        /// <summary>
        /// Model保存地址
        /// </summary>
        public string ModelFileSavePath;
        /// <summary>
        /// Dal保存地址
        /// </summary>
        public string DalFileSavePath;

        /// <summary>
        /// Model命名空间
        /// </summary>
        public string ModelNamespace;
        /// <summary>
        /// Dal命名空间
        /// </summary>
        public string DalNamespace;
        /// <summary>
        /// Model基类
        /// </summary>
        public string ModelBaseClassName;
        /// <summary>
        /// Dal基类
        /// </summary>
        public string DalBaseClassName;
        /// <summary>
        /// Model文件Using
        /// </summary>
        public string ModelUsing;
        /// <summary>
        /// Dal文件Using
        /// </summary>
        public string DalUsing;

        /// <summary>
        /// 表名过滤
        /// </summary>
        public string TableNameFilter;

        public CreateConfig()
        {
            this.ModelNamespace = cc.utility.Common.App("modelNamespace");
            this.DalNamespace = cc.utility.Common.App("dalNamespace");
            this.ModelBaseClassName = cc.utility.Common.App("modelBaseClassName");
            this.DalBaseClassName = cc.utility.Common.App("dalBaseClassName");
            this.ModelUsing = cc.utility.Common.App("modelUsing");
            this.DalUsing = cc.utility.Common.App("dalUsing");
            this.ModelFileSavePath = cc.utility.Common.App("modelFileSavePath");
            this.DalFileSavePath = cc.utility.Common.App("dalFileSavePath");
            this.TableNameFilter = cc.utility.Common.App("tableNameFilter");

            if (ModelFileSavePath.IndexOf(":") < 0)
            {
                ModelFileSavePath = Application.StartupPath + "\\" + ModelFileSavePath;
            }

            if (DalFileSavePath.IndexOf(":") < 0)
            {
                DalFileSavePath = Application.StartupPath + "\\" + DalFileSavePath;
            }
        }
    }
}
