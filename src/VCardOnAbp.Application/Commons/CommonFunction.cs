using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;

namespace VCardOnAbp.Commons
{
    public static class CommonFunction
    {
        public static Dictionary<string, string> ToDict(this object classInput) {
            var dict = classInput.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(classInput, null));
            return dict ?? new Dictionary<string, string>();
        }
    }
}
