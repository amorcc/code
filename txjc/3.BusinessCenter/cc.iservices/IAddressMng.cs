using cc.common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.iservices
{
    public interface IAddressMng
    {
        ActionResult<int> Insert(cc.common.UserInfo iLoginUser, int iProvince, int iCity, int iZone, string iReceiver, string iPhone, string iAddressDetail);
    }
}
