using System.Collections.Generic;

namespace OpenSrs
{
    public static class DicExtensions
    {
        public static string GetValueOrDefault(this IDictionary<string, string> dic, string key)
        {
            dic.TryGetValue(key, out string value);

            return value;
        }
    }
}