using cc.basemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.model
{
    /// <summary>
    /// 卖家支付方式
    /// </summary>
    [Serializable]
    public class VSupplierPayTypeInfo : BaseModel
    {
        public int PayTypeId { get; set; }
        public decimal SupplierRate { get; set; }
        public string UserSN { get; set; }
        public string PayTypeDesc { get; set; }
        public int ClientType { get; set; }
    }

    /// <summary>
    /// 用于寻找共同的支付方式
    /// </summary>
    public class PayTypeInfo
    {
        public int PayTypeId;
        public string PayTypeDesc;

        public PayTypeInfo(int iPayTypeId, string iPayTypeDesc)
        {
            this.PayTypeDesc = iPayTypeDesc;
            this.PayTypeId = iPayTypeId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is PayTypeInfo)
            {
                PayTypeInfo pti = obj as PayTypeInfo;

                if (pti.PayTypeId == this.PayTypeId)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
