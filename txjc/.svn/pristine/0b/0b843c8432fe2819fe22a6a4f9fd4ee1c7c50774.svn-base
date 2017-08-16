using cc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.Retailer
{
    public class RetailerMng
    {
        public List<VMySupplierInfo> GetMySupplier(common.UserInfo iLoginUser, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages)
        {
            cc.dal.MySupplier mySupplierDal = new dal.MySupplier();
            return mySupplierDal.GetMySupplier(iLoginUser, iPageIndex, iPageSize, out iTotalRows, out iTotalPages);
        }

        public VCompanyInfo SearchSupplier(common.UserInfo iLoginUser, string iUserSN)
        {
            cc.dal.Company companyDal = new dal.Company();
            return companyDal.GetCompanyInfo(iUserSN);
        }

        public bool AddSupplier(common.UserInfo iLoginUser, string iUserSN_S, string iUserSN_R, out string iErrorMsg)
        {
            cc.dal.MySupplier mySupplierDal = new dal.MySupplier();
            return mySupplierDal.AddSupplier(iLoginUser, iUserSN_S, iUserSN_R, out iErrorMsg);
        }

        public bool RemoveSupplier(common.UserInfo iLoginUser, int iUserCanBuyId, out string iErrorMsg)
        {
            cc.dal.MySupplier mySupplierDal = new dal.MySupplier();
            return mySupplierDal.RemoveSupplier(iLoginUser, iUserCanBuyId, out iErrorMsg);
        }
    }
}
