using cc.basemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.model
{
    [Serializable]
    public class UserInfo : BaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName;
        /// <summary>
        /// 用户联系电话
        /// </summary>
        public string Phone;
        /// <summary>
        /// Email
        /// </summary>
        public string Email;

        public string UserSN;
        public string RealName;
        public int RegisterSource;
        public int IsAdmin;
        public string Tier;
        public string Openid;
    }
}
