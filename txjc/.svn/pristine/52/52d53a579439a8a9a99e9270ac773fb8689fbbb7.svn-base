using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.system.utility
{
    /// <summary>
    /// 单号生成
    /// cc  2016-08-23
    /// </summary>
    public class SerialNumber
    {
        /// <summary>
        /// 缓存单号，保证短时间不重复
        /// </summary>
        static List<string> mList = new List<string>();

        /// <summary>
        /// 最多缓存的单号数量
        /// </summary>
        static int mSaveCodeCount = 100;

        /// <summary>
        /// 生成订单号:15位
        /// cc  2016-08-23
        /// 生成规则：
        ///     1、第一位：年-2015
        ///     2、月日时分秒： MMddhhmmss
        ///     3、4位随机数：  使用guid做种子生成随机数
        /// </summary>
        /// <returns></returns>
        static string CreateCode()
        {
            DateTime dt = DateTime.Now;
            int year = dt.Year - 2015;
            Random ran = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            int r = ran.Next(0, 9999);
            string code = string.Format("{0}{1}{2}", year, dt.ToString("MMddHHmmss"), r.ToString().PadLeft(4, '0'));
            return code;
        }

        /// <summary>
        /// 生成订单号:15位
        /// cc  2016-08-23
        /// 生成规则：
        ///     1、第一位：年-2015
        ///     2、月日时分秒： MMddhhmmss
        ///     3、4位随机数：  使用guid做种子生成随机数
        /// </summary>
        /// <returns></returns>
        public static string CreateOrderCode()
        {
            string orderCode = null;
            bool exist = false;
            do
            {
                orderCode = CreateCode();

                exist = mList.Where(t => t == orderCode).ToList().Count > 0 ? true : false;
            }
            while (exist == true);

            if (mList.Count > mSaveCodeCount)
            {
                mList.Clear();
            }
            else
            {
                mList.Add(orderCode);
            }

            if (!string.IsNullOrWhiteSpace(orderCode) && orderCode.Length == 15)
            {
                return orderCode;
            }
            else
            {
                throw new Exception("生成单号失败，请稍后重试！");
            }
        }

        ///// <summary>
        ///// 子订单号
        ///// </summary>
        ///// <returns></returns>
        //public static string CreateSubOrderCode()
        //{
        //    string refundCode = CreateOrderCode();
        //    return "S" + refundCode;
        //}

        /// <summary>
        /// 生成预售单号
        /// </summary>
        /// <returns></returns>
        public static string CreatePreSellCode()
        {
            string refundCode = CreateOrderCode();
            return "0" + refundCode;
        }

        ///// <summary>
        ///// 生成预售子订单号
        ///// </summary>
        ///// <returns></returns>
        //public static string CreateSubPreSellCode()
        //{
        //    string refundCode = CreateOrderCode();
        //    return "S0" + refundCode;
        //}

        /// <summary>
        /// 生成退款编号
        /// </summary>
        /// <returns></returns>
        public static string CreateRefundCode()
        {
            string refundCode = CreateOrderCode();
            return "R" + refundCode;
        }

        /// <summary>
        /// 生成投诉编号
        /// </summary>
        /// <returns></returns>
        public static string CreateComplaintCode()
        {
            string refundCode = CreateOrderCode();
            return "C" + refundCode;
        }
        /// <summary>
        /// 生成分货编号 2016-10-10 xd
        /// </summary>
        /// <returns></returns>
        public static string CreateGoodsDistributionCode()
        {
            string refundCode = CreateOrderCode();
            return "F" + refundCode;
        }

        /// <summary>
        /// 返回6位随机码
        /// </summary>
        /// <returns></returns>
        public static string CreateSixRandom()
        {
            Random ran = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            return ran.Next(0, 999999).ToString();
        }
    }
}
