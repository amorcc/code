using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.utility;

namespace cc.unit.Alipay
{
    public class AlipayMng
    {
        /// <summary>
        /// 微信公众号支付
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <param name="iOpenId"></param>
        /// <param name="iOrderCode"></param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public JObject Alipay(common.UserInfo iLoginUser, string iRN, string iOrderCodes, int iPayType, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            cc.log.Log.Debug(this.GetType(), iLoginUser, string.Format("rn={0},opeind={1},ordercodes={2},paytype={3}", iRN, "", iOrderCodes, iPayType));

            #region 获取订单信息
            cc.unit.PayMng.PayMng pay = new unit.PayMng.PayMng();
            cc.unit.PayMng.PayBeforeOrderInfo orderInfo = pay.GetOrderInfoByPayBefore(iLoginUser, iRN, iOrderCodes, iPayType, out iErrorMsg);

            if (!string.IsNullOrEmpty(iErrorMsg))
            {
                return null;
            }
            #endregion

            ////////////////////////////////////////////////////////////////////////////////////////////////

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", AlipayConfig.partner);
            sParaTemp.Add("seller_id", AlipayConfig.seller_id);
            sParaTemp.Add("_input_charset", AlipayConfig.input_charset.ToLower());
            sParaTemp.Add("service", AlipayConfig.service);
            sParaTemp.Add("payment_type", AlipayConfig.payment_type);
            sParaTemp.Add("notify_url", AlipayConfig.notify_url);
            sParaTemp.Add("return_url", AlipayConfig.return_url);
            sParaTemp.Add("out_trade_no", orderInfo.RN);
            sParaTemp.Add("subject", orderInfo.OrderCodes);
            sParaTemp.Add("total_fee", orderInfo.Total.ToString("0.00"));
            sParaTemp.Add("show_url", "show_url");
            //sParaTemp.Add("app_pay","Y");//启用此参数可唤起钱包APP支付。
            sParaTemp.Add("body", orderInfo.OrderCodes);
            //其他业务参数根据在线开发文档，添加参数.文档地址:https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.2Z6TSk&treeId=60&articleId=103693&docType=1
            //如sParaTemp.Add("参数名","参数值");

            //待请求参数数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = BuildRequestPara(sParaTemp);

            JObject result = new JObject();

            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                result.SetProperty(temp.Key, temp.Value);
            }

            return result;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <returns>要请求的参数数组</returns>
        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = "";

            //过滤签名参数数组
            sPara = Core.FilterPara(sParaTemp);

            //获得签名结果
            mysign = BuildRequestMysign(sPara);

            //签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", mysign);
            sPara.Add("sign_type", AlipayConfig.sign_type.Trim().ToUpper());

            return sPara;
        }

        /// <summary>
        /// 生成请求时的签名
        /// </summary>
        /// <param name="sPara">请求给支付宝的参数数组</param>
        /// <returns>签名结果</returns>
        private static string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            string prestr = Core.CreateLinkString(sPara);

            //把最终的字符串签名，获得签名结果
            string mysign = "";
            switch (AlipayConfig.sign_type)
            {
                case "MD5":
                    mysign = AlipayMD5.Sign(prestr, AlipayConfig.key, AlipayConfig.input_charset);
                    break;
                default:
                    mysign = "";
                    break;
            }

            return mysign;
        }
    }
}
