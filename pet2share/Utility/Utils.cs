using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet2share.Utility
{
    public class Utils
    {
        public static int CIntDef(object Expression, int DefaultValue = 0)
        {
            try
            {
                return System.Convert.ToInt32(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static string CStrGuid(object Expression, string DefaultValue = "")
        {
            try
            {
                Guid g;
                g = new Guid(Expression.ToString());
                return Expression.ToString();
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static Guid CGuidDef(object Expression, string DefaultValue = "00000000-0000-0000-0000-000000000000")
        {
            try
            {
                Guid g;
                g = new Guid(Expression.ToString());
                return g;
            }
            catch (Exception)
            {
                Guid g;
                g = new Guid(DefaultValue);
                return g;
            }
        }

        public static long CLngDef(object Expression, int DefaultValue = 0)
        {
            try
            {
                return System.Convert.ToInt32(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static bool CBoolDef(object Experssion, bool DefaultValue = false)
        {
            try
            {
                return System.Convert.ToBoolean(Experssion);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static decimal CDecDef(object Expression, decimal DefaultValue = 0)
        {
            try
            {
                return System.Convert.ToDecimal(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static double CDblDef(object Expression, double DefaultValue = 0)
        {
            try
            {
                return System.Convert.ToDouble(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static DateTime CDateDef(object Expression, DateTime DefaultValue)
        {
            try
            {
                return System.Convert.ToDateTime(Expression);
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }

        public static string CStrDef(object Expression, string DefaultValue = "")
        {
            try
            {
                return Expression.ToString().Trim();
            }
            catch (Exception)
            {
                return DefaultValue;
            }
        }
    }
}