using cc.iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.services
{
    public class AddressMng : IAddressMng
    {
        common.Utility.ActionResult<int> IAddressMng.Insert(common.UserInfo iLoginUser, int iProvince, int iCity, int iZone, string iReceiver, string iPhone, string iAddressDetail)
        {
            cc.unit.Misc.AddressMng addressBU = new unit.Misc.AddressMng();
            bool result = addressBU.Insert(iLoginUser, iProvince, iCity, iZone, iReceiver, iPhone, iAddressDetail);

            if (result == true)
            {
                return cc.common.Utility.MyResponse.ToYou<int>(1);
            }
            else
            {
                return cc.common.Utility.MyResponse.ShowError<int>("添加收货地址错误");
            }
        }
    }
}
