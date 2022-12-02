using System.Dynamic;
using System.Linq;
using Generator.Shared.Models;
using Mapster;
using MsgPack;
using MsgPack.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Generator.Shared.Extensions
{
    public static class MsgPackExtensions
    {
        public static List<IDictionary<string,object>> AdaptToDictionary<T>(this List<T> obj)
        {
            return obj.Adapt<List<IDictionary<string, object>>>();
        }

        public static IDictionary<string, object> AdaptToDictionary<T>(this T obj) 
        {
            return obj.Adapt<IDictionary<string, object>>();
        }

        public static IDictionary<string,object> DeserializeSingle(this byte[] bytes)
        {
            var serializer = MessagePackSerializer.Get<IDictionary<string, object>>();
            using (var byteStream = new MemoryStream(bytes))
            {
                return serializer.Unpack(byteStream);
            }
        }

        public static List<IDictionary<string, object>> Deserialize(this byte[] bytes)
        {
            var serializer = MessagePackSerializer.Get<List<IDictionary<string, object>>>();
            using (var byteStream = new MemoryStream(bytes))
            {
                return serializer.Unpack(byteStream);
            }
        }
 
        public static byte[] Serialize<T>(this T thisObj)
        {
            var serializer = MessagePackSerializer.Get<T>();

            using (var byteStream = new MemoryStream())
            {
                serializer.Pack(byteStream, thisObj);
                return byteStream.ToArray();
            }
        }

      
        public static T Deserialize<T>(this byte[] bytes)
        {
            var serializer = MessagePackSerializer.Get<T>();
            using (var byteStream = new MemoryStream(bytes))
            {
                return serializer.Unpack(byteStream);
            }
        }

        

        public static object Deserialize(this byte[] bytes, Type destinatoonType)
        {
            var serializer = MessagePackSerializer.Get(destinatoonType);
            using (var byteStream = new MemoryStream(bytes))
            {
                return serializer.Unpack(byteStream);
            }
        }

        //public static IEnumerable<IDictionary<string, object>> DeserializeToExpandObject<T>(this byte[] bytes) where T: List<IDictionary<string, object>>
        //{
        //    var result = Deserialize<T>(bytes);

        //    return result.Select((Dictionary<string, object> arg) => arg.Aggregate(new ExpandoObject() as IDictionary<string, object>,
        //                    (a, p) => { a.Add(p); return a; }));
        //}

       
    }
}

