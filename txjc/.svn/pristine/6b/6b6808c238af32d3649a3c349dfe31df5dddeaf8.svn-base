using cc.model;
using cc.common.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.ProductOrder.Pay
{
    public class PayPreview
    {
        public JObject GetPayPreviewInfo(cc.common.UserInfo iLoginUser, string iBatchId, string iOrderCodes, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            JObject result = new JObject();

            cc.dal.ProductOrder poDal = new dal.ProductOrder();
            List<VProductOrderInfo> poList = poDal.GetProductOrderList(iBatchId, iOrderCodes);

            List<string> userSNList = (from t in poList
                                       select t.UserSN_S).ToList();

            cc.dal.VSupplierPayType vspDal = new dal.VSupplierPayType();
            List<PayTypeInfo> commonPayType = vspDal.GetCommonPayType(userSNList);

            decimal subTotal = (from t in poList
                                select t.FinalPrice).Sum();

            decimal transFeeTotal = (from t in poList
                                     select t.TransFee).Sum();

            decimal total = subTotal + transFeeTotal;

            JArray payTypaJA = new JArray();

            foreach (var item in commonPayType)
            {
                payTypaJA.Add(new JObject()
                {
                    {"PayType",item.PayTypeId},
                    {"PayTypeName",item.PayTypeDesc},
                });
            }

            result.SetProperty("SubTotal", subTotal.ToString("0.00"));
            result.SetProperty("TransFeeTotal", transFeeTotal.ToString("0.00"));
            result.SetProperty("Total", total.ToString("0.00"));
            result.SetProperty("CommonPayType", payTypaJA);

            return result;
        }
    }
}
