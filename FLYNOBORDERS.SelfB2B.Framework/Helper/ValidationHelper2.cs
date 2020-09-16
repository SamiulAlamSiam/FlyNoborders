using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLYNOBORDERS.SelfB2B.Framework.Helper
{
    public static class ValidationHelper2
    {
        public static bool IsValidString(string value)
        {
            if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
                return false;
            return true;
        }

        public static bool IsValidInt(string value)
        {
            try
            {
                int i = Int32.Parse(value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsValidFloat(string value)
        {
            try
            {
                float i = float.Parse(value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
