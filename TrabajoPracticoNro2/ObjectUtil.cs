using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPracticoNro2
{
    public static class ObjectUtil
    {
        public static object GetDefaulValue(object c)
        {
            var defaultPropertyAttribute = c.GetType().GetCustomAttributes(true).OfType<DefaultPropertyAttribute>().FirstOrDefault();
            var defaultProperty = defaultPropertyAttribute.Name;

            return c.GetType().GetProperty(defaultProperty).GetValue(c);
        }

        public static void SetDefaulValue(object c, object value)
        {
            var defaultPropertyAttribute = c.GetType().GetCustomAttributes(true).OfType<DefaultPropertyAttribute>().FirstOrDefault();
            var defaultProperty = defaultPropertyAttribute.Name;

            c.GetType().GetProperty(defaultProperty).SetValue(c, value);
        }
    }
}
