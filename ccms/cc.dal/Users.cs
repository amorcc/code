using cc.basedal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.dal
{
    public class Users : BaseDAL<model.UsersInfo>
    {
        public Users()
            : base()
        {
            this._sortField = "DateAdded Desc";
        }
    }
}
