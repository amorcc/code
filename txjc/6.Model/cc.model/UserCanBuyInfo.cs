using cc.basemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.model
{
    [Serializable]
    public class UserCanBuyInfo : BaseModel
    {
        public string UserSN_R { get; set; }
        public string UserSN_S { get; set; }
    }
}
