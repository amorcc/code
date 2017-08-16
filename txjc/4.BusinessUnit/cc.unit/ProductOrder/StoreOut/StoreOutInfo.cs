using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.ProductOrder.StoreOut
{
    public class StoreOutInfo
    {
        public int ProId;
        public int ProCount;

        public StoreOutInfo(int iProId, int iProCount)
        {
            this.ProCount = iProCount;
            this.ProId = iProId;
        }
    }
}
