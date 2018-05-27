using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Football
{
    /// <summary>
    /// 
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType">Type</param>
        /// <param name="value">string</param>
        /// <returns>object</returns>
        public static object ConvertStringToEnum(Type enumType,string value)
        {
            return Enum.Parse(enumType,value);
        }
    }
}
