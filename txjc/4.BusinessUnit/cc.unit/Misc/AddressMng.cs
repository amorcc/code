using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.Misc
{
    public class AddressMng
    {
        public bool Insert(cc.common.UserInfo iLoginUser, int iProvince, int iCity, int iZone, string iReceiver, string iPhone, string iAddressDetail)
        {
            cc.dal.Address addressDal = new dal.Address();
            return addressDal.Insert(iLoginUser, iProvince, iCity, iZone, iReceiver, iPhone, iAddressDetail);
        }
    }
}
