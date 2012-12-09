using Newtonsoft.Json;
using System;

namespace Noesis.Javascript.Dynamic
{
    internal static class ObjectExtensions
    {
        public static string AsArgument(this object input)
        {
            if (input.IsNumeric())
                return input.ToString();
            return JsonConvert.SerializeObject(input);
        }


        public static bool IsNumeric(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

    }
}
