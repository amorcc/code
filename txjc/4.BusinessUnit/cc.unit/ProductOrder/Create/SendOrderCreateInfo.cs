using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.ProductOrder.Create
{
    public class SendOrderCreateInfo
    {
        public string UserSN;
        public decimal Total;
        public string Openid;
        public string OrderCode;
        public string RetailerName;

        public SendOrderCreateInfo(string iUserSN, string iOpenid, string iOrderCode, string iRetailerName, decimal iTotal)
        {
            this.UserSN = iUserSN;
            this.Total = iTotal;
            this.Openid = iOpenid;
            this.OrderCode = iOrderCode;
            this.RetailerName = iRetailerName;
        }
    }
}
