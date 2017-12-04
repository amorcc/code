using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.dal;
using cc.model;
using cc.basedal;
using cc.basebll;

namespace cc.bll
{
    public class Users : BaseBLL<dal.Users, model.UsersInfo>
    {
        public Users()
            : base()
        {

        }

    }
}
