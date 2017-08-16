using cc.common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.unit.CartMng
{
    public class CartMng
    {
        public int GetCartCount(string iUserSN_R)
        {
            cc.dal.Cart cartDal = new dal.Cart();
            return cartDal.GetCartCount(iUserSN_R);
        }

        /// <summary>
        /// 获取购物车信息
        /// </summary>
        /// <param name="iUserSN_R"></param>
        /// <returns></returns>
        public JArray GetCartInfo(string iUserSN_R)
        {
            cc.dal.Cart cartDal = new dal.Cart();
            return cartDal.GetCartInfo(iUserSN_R);
        }

          /// <summary>
        /// 修改购物车商品数量
        /// </summary>
        /// <param name="iLoginUser"></param>
        /// <param name="iProId"></param>
        /// <param name="iModifyCount">新的修改后的数量</param>
        /// <param name="iErrorMsg"></param>
        /// <returns></returns>
        public bool CartModifyCount(UserInfo iLoginUser, int iProId, int iModifyCount, out string iErrorMsg)
        {
            cc.dal.Cart cartDal = new dal.Cart();
            return cartDal.CartModifyCount(iLoginUser, iProId, iModifyCount, out iErrorMsg);
        } 
    }
}
