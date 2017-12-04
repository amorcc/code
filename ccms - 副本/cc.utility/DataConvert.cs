using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.utility
{
    public class DataConvert
    {
        public static string ToString(object iRowValue, string iDefaultValue = "")
        {
            try
            {
                return Convert.ToString(iRowValue == DBNull.Value ? iDefaultValue : iRowValue);
            }
            catch
            {
                return iDefaultValue;
            }
        }

        public static decimal ToDecimal(object iRowValue, decimal iDefaultValue = 0)
        {
            try
            {
                return Convert.ToDecimal(iRowValue == DBNull.Value ? iDefaultValue : iRowValue);
            }
            catch
            {
                return iDefaultValue;
            }
        }

        public static int ToInt32(object iRowValue, int iDefaultValue = 0)
        {
            try
            {
                return Convert.ToInt32(iRowValue == DBNull.Value ? iDefaultValue : iRowValue);
            }
            catch
            {
                return iDefaultValue;
            }
        }

        public static Int64 ToInt64(object iRowValue, int iDefaultValue = 0)
        {
            try
            {
                return Convert.ToInt64(iRowValue == DBNull.Value ? iDefaultValue : iRowValue);
            }
            catch
            {
                return iDefaultValue;
            }
        }

        public static double ToDouble(object iRowValue, double iDefaultValue = 0)
        {
            try
            {
                return Convert.ToDouble(iRowValue == DBNull.Value ? iDefaultValue : iRowValue);
            }
            catch
            {
                return iDefaultValue;
            }
        }

        public static DateTime ToDateTime(object iRowValue)
        {
            try
            {
                return Convert.ToDateTime(iRowValue == DBNull.Value ? new DateTime(1900, 1, 1) : iRowValue);
            }
            catch
            {
                return new DateTime(1900, 1, 1);
            }
        }
    }
}
