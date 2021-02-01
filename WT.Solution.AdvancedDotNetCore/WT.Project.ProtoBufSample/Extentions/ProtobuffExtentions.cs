using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WT.Project.ProtoBufSample.Extentions
{
    public static class ProtobuffExtentions
    {
        public static T DeserializeFromStringToProtoBuff<T>(this string txt)
        {
            byte[] arr = Convert.FromBase64String(txt);
            using (MemoryStream ms = new MemoryStream(arr))
                return ProtoBuf.Serializer.Deserialize<T>(ms);
        }

        public static string SerializeToString_PB<T>(this T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, obj);
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
        }
    }
}
