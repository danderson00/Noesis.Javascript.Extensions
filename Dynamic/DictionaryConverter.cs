using System.Collections.Generic;
using System.Dynamic;

namespace Noesis.Javascript.Dynamic
{
    public static class DictionaryConverter
    {
        public static dynamic AsDynamic(this IDictionary<string, object> source)
        {
            var result = new ExpandoObject();

            foreach (var item in source)
                if (item.Value is Dictionary<string, object>)
                    Dictionary(result).Add(item.Key, Dictionary(item.Value).AsDynamic());
                else
                    Dictionary(result).Add(item);

            return result;
        }

        private static IDictionary<string, object> Dictionary(object source)
        {
            return source as IDictionary<string, object>;
        }
    }
}
