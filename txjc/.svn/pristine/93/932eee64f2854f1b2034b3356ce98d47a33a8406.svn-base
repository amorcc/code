using cc.common.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.iservices
{
    public interface IPayMng
    {
        ActionResult<JArray> GetSupplierPayType(cc.common.UserInfo iLoginUser);

        ActionResult<int> SetSupplierPayType(common.UserInfo iLoginUser, int iPayTypeId, int iIsOpen);
    }
}
