using cc.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.ProductOrder.Preview
{
    public enum OrderSource
    {
        BuyNow = 1,
        Cart = 2,
    }

    /// <summary>
    /// 订单预览商品数据结构
    /// </summary>
    public class PreviewInfo
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public int ProId;
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ProCount;

        /// <summary>
        /// 产品信息
        /// </summary>
        public VProductInfo ProInfo;

        /// <summary>
        /// 运费
        /// </summary>
        public decimal TransFee
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// 商品小计:不算运费,
        /// </summary>
        public decimal SubTotal
        {
            get
            {
                return ProInfo.Price * ProCount;
            }
        }

        /// <summary>
        /// 商品合计
        /// </summary>
        public decimal Total
        {
            get
            {
                return SubTotal + TransFee;
            }
        }

        public PreviewInfo(int iProId, int iProCount)
        {
            this.ProId = iProId;
            this.ProCount = iProCount;
        }

    }
}
