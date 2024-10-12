using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VCardOnAbp.Commons;

public static class CommonFunction
{
    public static Dictionary<string, string> ToDict(this object classInput)
    {
        Dictionary<string, string?> dict = classInput.GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(x => x.GetValue(classInput, null) != null)
            .ToDictionary(prop => prop.Name, prop => (string)prop.GetValue(classInput, null));
        return dict ?? new Dictionary<string, string>();
    }
}
