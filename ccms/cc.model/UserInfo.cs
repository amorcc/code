using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.utility.Data;
using cc.basemodel;
using System.ComponentModel;

namespace cc.model
{

    [Table(TableName = "Users")]
    public class UsersInfo : BaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        [Column(VarcharLength = 50)]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        [Column(VarcharLength = 50)]
        public string Password { get; set; }

        [Description("登录次数")]
        public int LoginCount { get; set; }

        [Description("是否是经销商")]
        [Column(IsTableColumn = false, IsSerialize = false)]
        public bool IsRetailer { get; set; }
    }
}
