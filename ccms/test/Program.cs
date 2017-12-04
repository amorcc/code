using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cc.utility;
using Newtonsoft.Json.Linq;
using System.Data;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            cc.model.UsersInfo model = new cc.model.UsersInfo();
            model.DateAdded = DateTime.Now;
            model.UserName = "a";
            model.Password = "111111";
            model.LoginCount = 1;
            model.IsRetailer = false;
            //cc.utility.Factory<cc.bll.Users>.Instance.Insert(model);

            //cc.model.UsersInfo model = Factory<cc.bll.Users>.Instance.FindByPrimaryKey("2");

            //model.UserName = "b";
            //model.Password = "222222";
            //model.LoginCount = 2;
            //model.IsRetailer = true;

            //cc.utility.Factory<cc.bll.Users>.Instance.Update(model);

            SearchCondition sc = new SearchCondition();

            sc.AddCondition("LoginCount", "1", OperateType.Equal);

            List<cc.model.UsersInfo> list = Factory<cc.bll.Users>.Instance.List(sc.ConditionStr);
            cc.model.UsersInfo u = Factory<cc.bll.Users>.Instance.FindByCondition(sc.ConditionStr);
            Console.ReadKey();
        }

        //public class BaseModel
        //{
        //    public int Id;

        //    public BaseModel() { }
        //}

        //public class BaseDAL<T> where T : BaseModel, new()
        //{
        //    public BaseDAL()
        //    {

        //    }
        //    public DataSet ExecuteDataset(string iSPName)
        //    {
        //        Console.WriteLine("BaseDAL<T>:" + iSPName);
        //        return null;
        //    }

        //    public T GetModel(T para)
        //    {
        //        Console.WriteLine("BaseDAL<T>:getmodel");
        //        return para;
        //    }
        //}

        //public class BaseBLL<T, V> where V : BaseModel, new()
        //{
        //    BaseDAL<V> baseDal;
        //    public BaseBLL()
        //    {
        //        baseDal = Reflect<BaseDAL<V>>.Create(typeof(T).Name, typeof(T).Namespace);
        //    }

        //    public V GetModel(V para)
        //    {
        //        return baseDal.GetModel(para);
        //    }
        //}

        //public class UserModel : BaseModel
        //{

        //}

        //public class UserDAL : BaseDAL<UserModel>
        //{
        //    public UserDAL()
        //        : base()
        //    {

        //    }
        //}

        //public class UserBLL : BaseBLL<UserDAL, UserModel>
        //{
        //    public UserBLL()
        //        : base()
        //    {

        //    }

        //    public UserModel Test(UserModel m)
        //    {
        //        return base.GetModel(m);
        //    }
        //}
    }
}
