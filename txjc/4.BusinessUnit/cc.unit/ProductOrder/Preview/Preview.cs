using cc.common;
using cc.dal;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.common.Utility;

namespace cc.unit.ProductOrder.Preview
{
    public class Preview
    {
        /// <summary>
        /// 预览信息
        /// </summary>
        public List<PreviewInfo> mProInfo = new List<PreviewInfo>();

        /// <summary>
        /// 预览商品信息字符串
        /// </summary>
        string mProInfoStr;

        /// <summary>
        /// 订单来源
        /// </summary>
        OrderSource mOrderSource;

        /// <summary>
        /// 批处理号
        /// </summary>
        string mBatchId;

        /// <summary>
        /// 买家
        /// </summary>
        cc.common.UserInfo mLoginUser;

        /// <summary>
        /// 要购买的所有商品信息list
        /// </summary>
        public List<VProductInfo> mProductInfoList = new List<VProductInfo>();

        /// <summary>
        /// 购买商品的公司信息
        /// </summary>
        public List<VCompanyInfo> mSupplierInfoList = new List<VCompanyInfo>();

        /// <summary>
        /// 买家的收货地址
        /// </summary>
        List<AddressInfo> mBuyerAddressList;

        public Preview(cc.common.UserInfo iLoginUser, string iProInfoStr, OrderSource iOrderSource, string iBatchId)
        {
            this.mLoginUser = iLoginUser;
            this.mProInfoStr = iProInfoStr;
            this.mOrderSource = iOrderSource;
            this.mBatchId = iBatchId;
        }

        public JObject GetPreviewInfo(out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            JObject result = new JObject();

            if (this.SerializeProInfo(out iErrorMsg) == false)
            {
                return result;
            }

            if (this.mProInfo.Count == 0)
            {
                iErrorMsg = "读取预览商品信息出错！";
                return result;
            }

            #region 获取商品信息
            if (this.GetProInfoByDB(out iErrorMsg) == false)
            {
                return result;
            }
            #endregion

            #region 获取卖家信息
            if (this.GetSupplierInfoByDB(out iErrorMsg) == false)
            {
                return result;
            }
            #endregion

            #region 验证库存是否充足
            foreach (var item in this.mProInfo)
            {
                if (item.ProCount > item.ProInfo.Amount)
                {
                    iErrorMsg = string.Format("商品【{0}】库存不足，无法下单！", item.ProInfo.Name);
                    return result;
                }
            }
            #endregion

            JArray supplierJson = new JArray();

            #region 开始返回数据
            foreach (VCompanyInfo supplier in this.mSupplierInfoList)
            {
                #region 按卖家分组获取返回数据信息

                JArray items = new JArray();
                var proInfoList = (from t in this.mProInfo
                                   where t.ProInfo.UserSN == supplier.UserSN
                                   select t).ToList();

                foreach (var pro in proInfoList)
                {
                    items.Add(this.GetProductJson(pro));
                }

                #region 店铺信息
                decimal supplierTransFee = (from t in this.mProInfo
                                            where t.ProInfo.UserSN == supplier.UserSN
                                            select t.TransFee).Sum();

                decimal supplierSubTotal = (from t in this.mProInfo
                                            where t.ProInfo.UserSN == supplier.UserSN
                                            select t.SubTotal).Sum();

                decimal supplierTotal = supplierTransFee + supplierSubTotal;

                int sumProCount = (from t in this.mProInfo
                                   where t.ProInfo.UserSN == supplier.UserSN
                                   select t.ProCount).Sum();

                #endregion

                supplierJson.Add(new JObject()
                {
                    {"SupplierName",supplier.CompanyName},
                    {"UserSN_S",supplier.UserSN},
                    {"Total",supplierTotal},
                    {"SumProCount",sumProCount},
                    {"Items",items},

                });
                #endregion
            }
            #endregion

            #region 获取买家地址
            if (this.GetBuyerAddressByDB(out iErrorMsg) == false)
            {
                return result;
            }
            #endregion

            result.SetProperty("Supplier", supplierJson);
            result.SetProperty("BuyerAddress", this.GetBuyerAddressJson());

            return result;
        }

        #region 组合返回json数据内容

        JArray GetBuyerAddressJson()
        {
            JArray result = new JArray();
            if (this.mBuyerAddressList != null)
            {
                foreach (var item in this.mBuyerAddressList)
                {
                    result.Add(new JObject()
                {
                    {"Receiver",item.Receiver},
                    {"Phone",item.Phone},
                    {"Id",item.Id},
                    {"AddressTotal",item.AddressTotal},
                    {"IsDefault",item.IsDefault},
                });
                }
            }

            return result;
        }

        JObject GetProductJson(PreviewInfo iProInfo)
        {
            JObject result = new JObject()
            {
                {"ProId",iProInfo.ProInfo.Id},
                {"ProName",iProInfo.ProInfo.Name},
                {"ProImage",iProInfo.ProInfo.Image},
                {"ProPrice",iProInfo.ProInfo.Price.ToString("0.00")},
                {"ProCount",iProInfo.ProCount},
                {"TransFee",iProInfo.TransFee},
                {"SubTotal",iProInfo.SubTotal},
                {"Total",iProInfo.Total},
            };

            return result;
        }
        #endregion

        #region 从DB获取数据

        #region 获取购买商品的公司信息

        bool GetBuyerAddressByDB(out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            cc.dal.Address aDal = new Address();
            this.mBuyerAddressList = aDal.GetAddressByUserSN(this.mLoginUser.UserSN);

            //if (this.mBuyerAddressList == null || this.mBuyerAddressList.Count == 0)
            //{
            //    iErrorMsg = "您没有设置收货地址，无法下单!";
            //    return false;
            //}

            return true;
        }

        bool GetSupplierInfoByDB(out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            var listUserSN_S = (from t in this.mProductInfoList
                                select t.UserSN).Distinct().ToList();

            var strUserSN_S = string.Join(",", listUserSN_S);

            JObject para = new JObject()
            {
                {"QueryType",1},
                {"QueryPara",new JObject(){
                        {"ManyUserSN_S", strUserSN_S},
                    }
                },
            };

            cc.dal.Company cDal = new Company();
            this.mSupplierInfoList = cDal.GetModelList(para.ToString());

            if (mSupplierInfoList == null || this.mSupplierInfoList.Count == 0)
            {
                iErrorMsg = "查询卖家信息时出错";
                return false;
            }

            return true;
        }
        #endregion

        #region 获取购买的商品信息
        bool GetProInfoByDB(out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            string proIdsStr = string.Empty;
            foreach (var item in this.mProInfo)
            {
                proIdsStr += string.IsNullOrEmpty(proIdsStr) ? "" : ",";
                proIdsStr += item.ProId;
            }

            JObject para = new JObject()
            {
                {"QueryType",1},
                {"QueryPara",new JObject(){
                        {"ProIds",proIdsStr},
                    }
                },
            };

            cc.dal.Product pDal = new Product();
            this.mProductInfoList = pDal.GetModelList(para.ToString());

            if (mProductInfoList == null || this.mProductInfoList.Count == 0)
            {
                iErrorMsg = "查询购买商品信息时出错";
                return false;
            }

            foreach (var item in this.mProInfo)
            {
                var pro = (from t in this.mProductInfoList
                           where t.Id == item.ProId
                           select t).FirstOrDefault();

                item.ProInfo = pro;
            }

            return true;
        }
        #endregion
        #endregion

        #region 序列化产品信息字符串
        bool SerializeProInfo(out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            if (string.IsNullOrEmpty(this.mProInfoStr))
            {
                iErrorMsg = "读取预览商品信息出错！";
                return false;
            }
            string[] proInfoArray = this.mProInfoStr.Split(',');

            if (proInfoArray != null && proInfoArray.Length > 0)
            {
                foreach (var proStr in proInfoArray)
                {
                    string[] proArray = proStr.Split('|');

                    if (proArray != null && proArray.Length >= 2)
                    {
                        int proId = 0;
                        int proCount = 0;

                        if (int.TryParse(proArray[0], out proId) && int.TryParse(proArray[1], out proCount))
                        {
                            if (proId > 0 && proCount > 0)
                            {
                                PreviewInfo pi = new PreviewInfo(proId, proCount);
                                this.mProInfo.Add(pi);
                            }
                        }
                    }
                }
            }

            return true;
        }
        #endregion
    }
}
