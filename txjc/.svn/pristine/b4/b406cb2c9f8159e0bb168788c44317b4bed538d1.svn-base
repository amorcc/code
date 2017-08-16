using cc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.Supplier
{
    public class SupplierMng
    {
        public List<VMyRetailerInfo> GetMyRetailer(common.UserInfo iLoginUser, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages)
        {
            cc.dal.MyRetailer myRetailer = new dal.MyRetailer();
            return myRetailer.GetMyRetailer(iLoginUser, iPageIndex, iPageSize, out iTotalRows, out iTotalPages);
        }

       
    }
}
