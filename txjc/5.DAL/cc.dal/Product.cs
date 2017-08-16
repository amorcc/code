using cc.basedal;
using cc.model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.common.Utility;

namespace cc.dal
{
    public class Product : BaseDAL<VProductInfo>
    {
        public Product()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "v_Product";
            this._sortField = "DateAdded ";
            this._isDesc = true;
        }

        protected override VProductInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new VProductInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.Amount = Convert.ToInt32(dr["Amount"] == DBNull.Value ? 0 : dr["Amount"]);
            obj.BillNeeded = Convert.ToInt32(dr["BillNeeded"] == DBNull.Value ? 0 : dr["BillNeeded"]);
            obj.Desc = Convert.ToString(dr["Desc"] == DBNull.Value ? "" : dr["Desc"]);
            obj.Image = Convert.ToString(dr["Image"] == DBNull.Value ? "" : dr["Image"]);
            obj.Images = Convert.ToString(dr["Images"] == DBNull.Value ? "" : dr["Images"]);
            obj.Name = Convert.ToString(dr["Name"] == DBNull.Value ? "" : dr["Name"]);
            obj.Price = Convert.ToDecimal(dr["Price"] == DBNull.Value ? 0 : dr["Price"]);
            obj.SaledRecent30Days = Convert.ToInt32(dr["SaledRecent30Days"] == DBNull.Value ? 0 : dr["SaledRecent30Days"]);
            obj.Status = Convert.ToInt32(dr["Status"] == DBNull.Value ? 0 : dr["Status"]);
            obj.UserSN = Convert.ToString(dr["UserSN"] == DBNull.Value ? "" : dr["UserSN"]);
            obj.Supplier = Convert.ToString(dr["Supplier"] == DBNull.Value ? "" : dr["Supplier"]);
            obj.Weight = Convert.ToInt32(dr["Weight"] == DBNull.Value ? 1 : dr["Weight"]);

            return obj;
        }

        public List<VProductInfo> GetProductListByManyUserSN(string iManyUserSN_S)
        {
            if (string.IsNullOrEmpty(iManyUserSN_S))
            {
                return null;
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN", iManyUserSN_S, OperateType.In);
            sc.AddCondition("Status", "1", OperateType.Equal);
            //sc.AddCondition("Name", "钉王", OperateType.Like);

            return base.List(sc.ConditionStr);
        }

        public List<VProductInfo> GetProductList(string iUserSN_R, string iKey, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            iTotalPages = 1;
            iTotalRows = 0;
            List<VProductInfo> result = new List<VProductInfo>();
            var paras = new[]
                    {
                        new SqlParameter("@key", iKey),
                        new SqlParameter("@userSN_R", iUserSN_R),
                        new SqlParameter("@PageIndex", iPageIndex),
                        new SqlParameter("@PageSize", iPageSize),
                        new SqlParameter(){ParameterName = "@totalPages",SqlDbType = SqlDbType.Int,Size = 4,Direction = ParameterDirection.Output},
                        new SqlParameter(){ParameterName = "@totalRows",SqlDbType = SqlDbType.Int,Size = 4,Direction = ParameterDirection.Output},
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            using (DataSet ds = SqlHelper.ExecuteDataset(this._sqlCon, CommandType.StoredProcedure, "proc_Product_Search", paras))
            {
                var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return result;
                }

                iTotalRows = Convert.ToInt32(paras[paras.Length - 2].Value);
                iTotalPages = Convert.ToInt32(paras[paras.Length - 3].Value);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        var obj = new VProductInfo();
                        obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
                        obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

                        obj.Amount = Convert.ToInt32(dr["Amount"] == DBNull.Value ? 0 : dr["Amount"]);
                        obj.BillNeeded = Convert.ToInt32(dr["BillNeeded"] == DBNull.Value ? 0 : dr["BillNeeded"]);
                        obj.Desc = Convert.ToString(dr["Desc"] == DBNull.Value ? "" : dr["Desc"]);
                        obj.Image = cc.utility.Common.App("ApiSiteUrl") + Convert.ToString(dr["Image"] == DBNull.Value ? "" : dr["Image"]);
                        obj.Images = Convert.ToString(dr["Images"] == DBNull.Value ? "" : dr["Images"]);
                        obj.Name = Convert.ToString(dr["Name"] == DBNull.Value ? "" : dr["Name"]);
                        obj.Price = Convert.ToDecimal(dr["Price"] == DBNull.Value ? 0 : dr["Price"]);
                        obj.SaledRecent30Days = Convert.ToInt32(dr["SaledRecent30Days"] == DBNull.Value ? 0 : dr["SaledRecent30Days"]);
                        obj.Status = Convert.ToInt32(dr["Status"] == DBNull.Value ? 0 : dr["Status"]);
                        obj.UserSN = Convert.ToString(dr["UserSN"] == DBNull.Value ? "" : dr["UserSN"]);
                        obj.Supplier = Convert.ToString(dr["Supplier"] == DBNull.Value ? "" : dr["Supplier"]);

                        result.Add(obj);
                    }
                }
            }

            return result;
        }

        public VProductInfo GetProductInfo(int iProId)
        {
            List<VProductInfo> cache = cc.common.Cache.CacheMng<VProductInfo>.GetCache(this._tableName);

            if (cache != null && cache.Count > 0)
            {
                VProductInfo pro = (from t in cache
                                    where t.Id == iProId
                                    select t).FirstOrDefault();

                if (pro != null)
                {
                    return pro;
                }
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("Id", iProId.ToString(), OperateType.Equal);

            return base.FindByCondition(sc.ConditionStr);
        }

        public List<VProductInfo> GetProductList(string iUserSN_S)
        {
            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN", iUserSN_S, OperateType.Equal);
            sc.AddCondition("Status", "1", OperateType.Equal);

            return base.List(sc.ConditionStr);
        }

        public override List<VProductInfo> GetModelList(string jsonPara)
        {
            List<VProductInfo> result = new List<VProductInfo>();
            JObject para = JObject.Parse(jsonPara);

            int queryType = para.GetValueExt<int>("QueryType");
            JObject queryPara = para.GetValueExt<JObject>("QueryPara");

            if (queryType == 1)
            {
                #region 根据proId查询，多个用逗号隔开
                string proIds = queryPara.GetValueExt<string>("ProIds", "");

                if (!string.IsNullOrEmpty(proIds))
                {
                    List<int> proIdList = new List<int>();
                    List<int> proIdNoFindList = new List<int>();
                    #region 格式化proId到List中
                    string[] proIdsArray = proIds.Split(',');

                    foreach (var proIdStr in proIdsArray)
                    {
                        int proId = 0;
                        if (int.TryParse(proIdStr, out proId) == true && proId > 0)
                        {
                            proIdList.Add(proId);
                        }
                    }
                    #endregion

                    List<VProductInfo> cache = cc.common.Cache.CacheMng<VProductInfo>.GetCache(this._tableName);

                    #region 将没有查询到的ProId加入到proIdNoFindList,查询到的直接加入result中
                    if (cache != null && cache.Count > 0)
                    {
                        foreach (var item in proIdList)
                        {

                            var findCacheResult = (from t in cache
                                                   where t.Id == item
                                                   select t).ToList();

                            if (findCacheResult != null && findCacheResult.Count > 0)
                            {
                                result.AddRange(findCacheResult);
                            }
                            else
                            {
                                proIdNoFindList.Add(item);
                            }
                        }
                    }
                    else
                    {
                        proIdNoFindList.AddRange(proIdList);
                    }

                    #endregion

                    #region 查询剩余的没有缓存的数据信息
                    string noFindProIdStr = string.Join(",", proIdNoFindList);

                    var noCacheResult = this.GetProductListByProIds(noFindProIdStr);

                    result.AddRange(noCacheResult);
                    #endregion

                    cc.common.Cache.CacheMng<VProductInfo>.AddCache(this._tableName, noCacheResult);
                }
                #endregion
            }

            return result;
        }

        public List<VProductInfo> GetProductListByProIds(string iProIds)
        {
            if (!string.IsNullOrEmpty(iProIds))
            {
                SearchCondition sc = new SearchCondition();
                sc.AddCondition("Id", iProIds, OperateType.In);

                return base.List(sc.ConditionStr);
            }
            else
            {
                return new List<VProductInfo>();
            }
        }

        public List<VProductInfo> SellerProductMng(common.UserInfo iLoginUser, int iStatus, string iProName, int iPageIndex, int iPageSize, out int iTotalRows, out int iTotalPages, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            if (iLoginUser == null || string.IsNullOrEmpty(iLoginUser.UserSN))
            {
                iTotalRows = 0;
                iTotalPages = 1;
                iErrorMsg = "无法获取卖家身份信息";
                return null;
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN", iLoginUser.UserSN, OperateType.Equal);
            sc.AddCondition("Status", iStatus.ToString(), OperateType.Equal);

            if (!string.IsNullOrEmpty(iProName))
            {
                sc.AddCondition("Name", iProName, OperateType.Like);
            }

            PageInfo pageinfo = new PageInfo();
            pageinfo.PageIndex = iPageIndex <= 0 ? 1 : iPageIndex;
            pageinfo.PageSize = iPageSize <= 0 ? 15 : iPageSize;

            var result = base.List(sc.ConditionStr, pageinfo);
            iTotalPages = pageinfo.TotalPages;
            iTotalRows = pageinfo.TotalRows;

            return result;
        }

        public bool UpdateStatus(common.UserInfo iLoginUser, int iProId, int iStatus, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            if (iLoginUser == null || string.IsNullOrEmpty(iLoginUser.UserSN))
            {
                iErrorMsg = "读取卖家信息失败";
                return false;
            }

            VProductInfo proinfo = this.GetProductInfo(iProId);

            if (proinfo == null)
            {
                iErrorMsg = "读取商品信息失败";
                return false;
            }

            if (proinfo.UserSN != iLoginUser.UserSN)
            {
                iErrorMsg = "您没有修改该商品信息的权限";
                return false;
            }

            if (iStatus != 0 && iStatus != 1)
            {
                iErrorMsg = "状态修改值不正确";
                return false;
            }

            JObject joPara = new JObject()
            {
                {"Status",iStatus},
            };

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("Id", iProId.ToString(), OperateType.Equal);

            if (base.UpdateByCondition(joPara.ToString(), sc.ConditionStr) > 0)
            {
                //写商品修改日志流
                cc.dal.ProductStateFlow psfDal = new ProductStateFlow();
                psfDal.Insert(iLoginUser.UserId, iStatus, string.Format("修改商品ID：{0}的状态为{1}", iProId, iStatus), string.Format("修改商品ID：{0}的状态为{1}", iProId, iStatus));

                return true;
            }
            else
            {
                return false;
            }

        }

        #region 发布和修改商品
        public bool ModifyProduct(common.UserInfo iLoginUser, int iProId, string iProName, int iBillNeeded, int iStatus, int iAmount, decimal iPrice, List<string> iImages, string iDesc, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;

            string image = string.Empty;
            string images = string.Empty;
            if (iImages == null || iImages.Count < 1)
            {
                iErrorMsg = "请上传商品图片，最低一个";
                return false;
            }
            else
            {
                image = iImages[0];
                iImages.RemoveAt(0);

                images = string.Join(",", iImages);
            }

            var paras = new[]
                    {
                        new SqlParameter("@proId", iProId),
                        new SqlParameter("@proName", iProName),
                        new SqlParameter("@billNeeded", iBillNeeded),
                        new SqlParameter("@status", iStatus),
                        new SqlParameter("@amount", iAmount),
                        new SqlParameter("@price", iPrice),
                        new SqlParameter("@image", image),
                        new SqlParameter("@images", images),
                        new SqlParameter("@desc", iDesc),
                        new SqlParameter("@userSN_S", iLoginUser.UserSN),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "[proc_Product_Add_Or_Modify]", paras);

            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }
            return true;
        }
        #endregion

        #region 删除商品
        public bool DeleteProduct(common.UserInfo iLoginUser, int iProId, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            VProductInfo proinfo = this.GetProductInfo(iProId);

            if (proinfo == null)
            {
                iErrorMsg = "您要删除的商品信息不存在";
                return false;
            }

            if (proinfo.UserSN != iLoginUser.UserSN)
            {
                iErrorMsg = "您没有权限删除该商品";
                return false;
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("Id", iProId.ToString(), OperateType.Equal);

            var sql = string.Format("DELETE FROM dbo.{0} {1}", "Product", sc.ConditionStr);
            return SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.Text, sql, null) > 0;


        }
        #endregion

        #region 修改库存
        public bool UpdateAmount(common.UserInfo iLoginUser, int iProId, int iAmount, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var paras = new[]
                    {
                        new SqlParameter("@proId", iProId),
                        new SqlParameter("@amount", iAmount),
                        new SqlParameter("@userSN", iLoginUser.UserSN),
                        new SqlParameter("@sysUserId", iLoginUser.UserId),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "[proc_Product_UpdateAmount]", paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }

        #endregion

        #region 修改价格
        public bool UpdatePrice(common.UserInfo iLoginUser, int iProId, decimal iPrice, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            var paras = new[]
                    {
                        new SqlParameter("@proId", iProId),
                        new SqlParameter("@price", iPrice),
                        new SqlParameter("@userSN", iLoginUser.UserSN),
                        new SqlParameter("@sysUserId", iLoginUser.UserId),
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}

                    };

            SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, "[proc_Product_UpdatePrice]", paras);
            var msg = paras[paras.Length - 1].Value;//返回非空,执行数据查询异常
            if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
            {
                iErrorMsg = msg.ToString();
                return false;
            }

            return true;
        }

        #endregion
    }
}
