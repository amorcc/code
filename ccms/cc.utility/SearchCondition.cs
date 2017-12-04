using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.utility
{
    public class SearchCondition
    {
        private List<string> _conditionList = new List<string>();
        /// <summary>
        /// 根据条件构造SQL查询条件
        /// </summary>
        /// <param name="field">条件字段</param>
        /// <param name="fValue">值</param>
        /// <param name="oType">比较类型</param>
        public void AddCondition(string field, string fValue, OperateType oType)
        {
            if (string.IsNullOrEmpty(fValue))
            {
                fValue = "";
            }

            switch (oType)
            {
                case OperateType.Like:
                    _conditionList.Add("and " + field + " like'%" + fValue.Replace("%", "[%]") + "%' ");
                    break;
                case OperateType.RightLike:
                    _conditionList.Add("and " + field + " like'" + fValue + "%' ");
                    break;
                case OperateType.Equal:
                    _conditionList.Add("and " + field + "='" + fValue + "' ");
                    break;
                case OperateType.NotEqual:
                    _conditionList.Add("and " + field + "<>'" + fValue + "' ");
                    break;
                case OperateType.MoreThan:
                    _conditionList.Add("and " + field + ">'" + fValue + "' ");
                    break;
                case OperateType.LessThen:
                    _conditionList.Add("and " + field + "<'" + fValue + "' ");
                    break;
                case OperateType.In:
                    _conditionList.Add("and " + field + " in('" + fValue.Replace(",", "','") + "') ");
                    break;
                case OperateType.NotIn:
                    _conditionList.Add("and " + field + " not in('" + fValue.Replace(",", "','") + "') ");
                    break;
                case OperateType.InSql:
                    _conditionList.Add("and " + field + " in(" + fValue + ") ");
                    break;
                case OperateType.Or:
                    var fies = field.Split(new char[] { '|' });
                    var fval = fValue.Split(new char[] { '|' });
                    var lstOr = new List<string>();
                    for (int i = 0; i < fies.Length; i++)
                    {
                        lstOr.Add(" " + fies[i] + "='" + fval[i] + "' ");
                    }
                    _conditionList.Add("and (" + string.Join(" Or ", lstOr) + ") ");
                    break;
                case OperateType.Between:
                    _conditionList.Add("and " + field + " between'" + fValue.Split(new char[] { '|' })[0] + "' and '" + fValue.Split(new char[] { '|' })[1] + "'");
                    break;
                case OperateType.DateBetween:
                    if (fValue.Split(new char[] { '|' })[0] != "")
                        _conditionList.Add("and " + field + ">'" + fValue.Split(new char[] { '|' })[0] + "' ");
                    if (fValue.Split(new char[] { '|' })[1] != "")
                        _conditionList.Add("and " + field + "<'" + fValue.Split(new char[] { '|' })[1] + " 23:59:59:59' ");
                    break;
            }
        }
        /// <summary>
        /// 返回构造的条件语句
        /// </summary>
        public string ConditionStr
        {
            get
            {
                if (_conditionList.Count == 0)
                    return "";
                else
                {
                    string con = string.Join("", _conditionList.ToArray());
                    return " Where " + con.Substring(3, con.Length - 3);
                }
            }
        }

    }
    public enum OperateType
    {
        Like, Equal, MoreThan, LessThen, Between, In, NotIn, NotEqual, InSql, DateBetween, Or, RightLike

    }
}
