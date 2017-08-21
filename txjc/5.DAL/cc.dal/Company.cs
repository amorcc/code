using cc.basedal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.model;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using cc.common.Utility;
using cc.common.DataConvert;
using System.Data;

namespace cc.dal
{
    public class Company : BaseDAL<VCompanyInfo>
    {
        public Company()
            : base()
        {
            this._sqlCon = cc.common.Sys.SystemConnections.B2bConn;
            this._tableName = "v_Company";
            this._sortField = "Id";
            this._primaryKey = "Id";
            this._isDesc = false;
        }

        protected override VCompanyInfo DataReaderToEntity(SqlDataReader dr)
        {
            var obj = new VCompanyInfo();
            obj.Id = Convert.ToInt32(dr["Id"] == DBNull.Value ? 0 : dr["Id"]);
            obj.DateAdded = Convert.ToDateTime(dr["DateAdded"] == DBNull.Value ? new DateTime(1900, 1, 1) : dr["DateAdded"]);

            obj.UserSN = Convert.ToString(dr["UserSN"] == DBNull.Value ? "" : dr["UserSN"]);
            obj.UserId = Convert.ToInt32(dr["UserId"] == DBNull.Value ? 0 : dr["UserId"]);
            obj.CompanyName = Convert.ToString(dr["CompanyName"] == DBNull.Value ? "" : dr["CompanyName"]);
            obj.AreaCode = Convert.ToInt32(dr["AreaCode"] == DBNull.Value ? 0 : dr["AreaCode"]);
            obj.CompanyPhone = Convert.ToString(dr["CompanyPhone"] == DBNull.Value ? "" : dr["CompanyPhone"]);
            obj.CompanyAddress = Convert.ToString(dr["CompanyAddress"] == DBNull.Value ? "" : dr["CompanyAddress"]);
            obj.BusinessScope = Convert.ToString(dr["BusinessScope"] == DBNull.Value ? "" : dr["BusinessScope"]);
            obj.QRCode = cc.utility.Common.App("ApiSiteUrl") + Convert.ToString(dr["QRCode"] == DBNull.Value ? "" : dr["QRCode"]);
            obj.IsOpenSupplier = DataConvert.ToInt32(dr["IsOpenSupplier"]);
            obj.Openid = DataConvert.ToString(dr["Openid"]);
            obj.ShopName = DataConvert.ToString(dr["ShopName"]);
            obj.LogoImgUrl = DataConvert.ToString(dr["LogoImgUrl"]);
            obj.WechatNumber = DataConvert.ToString(dr["WechatNumber"]);

            return obj;
        }

        public VCompanyInfo GetCompanyInfo(string iUserSN)
        {
            if (string.IsNullOrEmpty(iUserSN))
            {
                iUserSN = System.Guid.NewGuid().ToString();
            }

            SearchCondition sc = new SearchCondition();
            sc.AddCondition("UserSN", iUserSN, OperateType.Equal);
            return base.FindByCondition(sc.ConditionStr);
        }

        public override List<VCompanyInfo> GetModelList(string jsonPara)
        {
            List<VCompanyInfo> result = new List<VCompanyInfo>();
            JObject para = JObject.Parse(jsonPara);

            int queryType = para.GetValueExt<int>("QueryType");
            JObject queryPara = para.GetValueExt<JObject>("QueryPara");

            if (queryType == 1)
            {
                #region 根据proId查询，多个用逗号隔开
                string proIds = queryPara.GetValueExt<string>("ManyUserSN_S", "");

                if (!string.IsNullOrEmpty(proIds))
                {
                    List<string> manyUserSNList = new List<string>();
                    List<string> userSNNoFindList = new List<string>();
                    #region 格式化proId到List中
                    string[] userSNArray = proIds.Split(',');

                    foreach (var userSN in userSNArray)
                    {
                        manyUserSNList.Add(userSN);
                    }
                    #endregion

                    List<VCompanyInfo> cache = cc.common.Cache.CacheMng<VCompanyInfo>.GetCache(this._tableName);

                    #region 将没有查询到的ProId加入到proIdNoFindList,查询到的直接加入result中
                    if (cache != null && cache.Count > 0)
                    {
                        foreach (var item in manyUserSNList)
                        {

                            var findCacheResult = (from t in cache
                                                   where t.UserSN == item
                                                   select t).ToList();

                            if (findCacheResult != null && findCacheResult.Count > 0)
                            {
                                result.AddRange(findCacheResult);
                            }
                            else
                            {
                                userSNNoFindList.Add(item);
                            }
                        }
                    }
                    else
                    {
                        userSNNoFindList.AddRange(manyUserSNList);
                    }

                    #endregion

                    #region 查询剩余的没有缓存的数据信息
                    string noFindProIdStr = string.Join(",", userSNNoFindList);

                    var noCacheResult = this.GetCompanyListByManyUserSN(noFindProIdStr);

                    result.AddRange(noCacheResult);
                    #endregion

                    cc.common.Cache.CacheMng<VCompanyInfo>.AddCache(this._tableName, noCacheResult);
                }
                #endregion
            }

            return result;
        }

        public List<VCompanyInfo> GetCompanyListByManyUserSN(string iManyUserSN)
        {
            if (!string.IsNullOrEmpty(iManyUserSN))
            {
                SearchCondition sc = new SearchCondition();
                sc.AddCondition("UserSN", iManyUserSN, OperateType.In);

                return base.List(sc.ConditionStr);
            }
            else
            {
                return new List<VCompanyInfo>();
            }
        }

        public bool CreateInviteQRCode(common.UserInfo iLoginUser, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            VCompanyInfo companyInfo = this.GetCompanyInfo(iLoginUser.UserSN);

            string wapSiteUrl = cc.utility.Common.App("WapSiteUrl");
            string inviteUrl = string.Format("{0}#/joinme/{1}", wapSiteUrl, iLoginUser.UserSN);
            string filePath = string.Format("QRCode/{0}.png", iLoginUser.UserSN);

            cc.common.QRCode.QRCode qrcode = new common.QRCode.QRCode();
            if (qrcode.CreateQCcode(inviteUrl, "/" + filePath))
            {
                var result = this.SetCompanyQRCode(iLoginUser, filePath, out iErrorMsg);

                if (result == true && string.IsNullOrEmpty(iErrorMsg))
                {
                    return true;
                }
            }

            return false;
        }

        bool SetCompanyQRCode(common.UserInfo iLoginUser, string iQRCodeFileName, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            try
            {
                var sp = "[proc_Company_AddQRCode]";
                var paras = new[]
                    {
                        new SqlParameter("@filePath", iQRCodeFileName),
                        new SqlParameter("@userSN", iLoginUser.UserSN),
                        new SqlParameter("@sysUserID", iLoginUser.UserId), //权限控制 及参数列表
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}
                    };
                SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, sp, paras);
                var msg = paras[paras.Length - 1].Value;
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                iErrorMsg = ex.ToString();
                return false;
            }
        }

        public bool JoinMe(common.UserInfo iLoginUser, string iUserSN_S, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            try
            {
                var sp = "[proc_Company_Join]";
                var paras = new[]
                    {
                        new SqlParameter("@userSN_S", iUserSN_S),
                        new SqlParameter("@userSN_R", iLoginUser.UserSN),
                        new SqlParameter("@sysUserID", iLoginUser.UserId), //权限控制 及参数列表
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}
                    };
                SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, sp, paras);
                var msg = paras[paras.Length - 1].Value;
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                iErrorMsg = ex.ToString();
                return false;
            }
        }

        public bool UpdateCompanyInfo(common.UserInfo iLoginUser, int iId, string iCompanyName, int iAreaCode, string iCompanyPhone, string iCompanyAddress, string iBusinessScope, string iShopName, string iWechatNumber, string iLogoImgUrl, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            try
            {
                var sp = "[proc_Company_Update]";
                var paras = new[]
                    {
                        new SqlParameter("@id", iId),
                        new SqlParameter("@companyName", iCompanyName),
                        new SqlParameter("@areaCode", iAreaCode),
                        new SqlParameter("@companyPhone", iCompanyPhone),
                        new SqlParameter("@companyAddress", iCompanyAddress),
                        new SqlParameter("@businessScope", iBusinessScope),
                        new SqlParameter("@shopName", iShopName),
                        new SqlParameter("@wechatNumber", iWechatNumber),
                        new SqlParameter("@logoImgUrl", iLogoImgUrl),
                        new SqlParameter("@sysUserID", iLoginUser.UserId), //权限控制 及参数列表
                        new SqlParameter(){ParameterName = "@msg",SqlDbType = SqlDbType.VarChar,Size = 200,Direction = ParameterDirection.Output}
                    };
                SqlHelper.ExecuteNonQuery(this._sqlCon, CommandType.StoredProcedure, sp, paras);
                var msg = paras[paras.Length - 1].Value;
                if (msg != null && !string.IsNullOrEmpty(msg.ToString()))
                {
                    iErrorMsg = msg.ToString();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                iErrorMsg = ex.ToString();
                return false;
            }
        }
    }
}
