using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.utility
{
    public class MyException : ApplicationException
    {
        //public MyException(){}
        public MyException(string message) : base(message) { }
        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
    }
}
