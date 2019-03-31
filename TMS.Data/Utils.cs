using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Data
{
    public static class Utils
    {
        public const string appModel = "app_model";
        public const string appSecurities = "app_securities";
        public static T CustomParse<T>(object input)
        {
            var val = default(T);
           
            if (input != null && typeof(DBNull) != input.GetType())
            {
                val = (T)Convert.ChangeType(input, typeof(T));
            }
             return val;
        }
    }
}
