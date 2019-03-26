using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.BusinessObjects;

namespace TMS.BusinessLayer.Common
{
    public static class EnumExtensions
    {

        public static List<EnumValue> GetEnumValues<T>()
        {
        List<EnumValue> vals = new List<EnumValue>();
        foreach(var item in Enum.GetValues(typeof(T)))
            {
                vals.Add(new EnumValue()
                {
                    Name = Enum.GetName(typeof(T), item),
                    Value = (int)item
                });
            }
            return vals;
                
          }
    }
}
