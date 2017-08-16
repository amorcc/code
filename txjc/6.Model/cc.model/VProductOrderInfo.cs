using cc.basemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.model
{
    public class VProductOrderInfo : BaseModel
    {
        public string UserSN_R { get; set; }
        public int UserID { get; set; }
        public string OrderCode { get; set; }
        public string UserSN_S { get; set; }
        public decimal TransFee { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public int OrderStatus { get; set; }
        public string StatusReason { get; set; }
        public string Message { get; set; }
        public int AreaCode { get; set; }
        public string Address { get; set; }
        public string Receiver { get; set; }
        public int ZipCode { get; set; }
        public string Phone { get; set; }
        public string Express { get; set; }
        public int SendNum { get; set; }
        public DateTime DateModifed { get; set; }
        public int PrimaryPayType { get; set; }
        public string Supplier { get; set; }
        public string Retailer { get; set; }
        public string BatchId { get; set; }
        public string PayTypeName { get; set; }

        /// <summary>
        /// 扩展字段，本订单的详情信息
        /// </summary>
        public List<VProductOrderDetailInfo> DetailList { get; set; }
    }

    /// <summary>
    /// 订单状态
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 已取消
        /// </summary>
        Canceled = -1,
        /// <summary>
        /// 待支付
        /// </summary>
        WaitPay = 1,
        /// <summary>
        /// 待核销
        /// </summary>
        WaitWriteOff = 11,
        /// <summary>
        /// 已支付
        /// </summary>
        Payment = 2,
        /// <summary>
        /// 已出库
        /// </summary>
        StoreOut = 3,
        /// <summary>
        /// 部分出库
        /// </summary>
        PartStoreOut = 31,
        /// <summary>
        /// 已发货
        /// </summary>
        Delivered = 4,
        /// <summary>
        /// 已完成
        /// </summary>
        Finished = 5,
    }
}
