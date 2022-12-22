using Mapster;
using MBrace.FsPickler;

namespace Generator.Shared.Extensions
{
    public static class FsPicklerExtensions
    {
        private static BinarySerializer serializer = FsPickler.CreateBinarySerializer();

        public static TModel ToModel<TModel>(this IDictionary<string,object> obj)
        {
            return obj.Adapt<TModel>();
        }

        public static List<TModel> ToModel<TModel>(this List<IDictionary<string, object>> obj)
        {
            return obj.Adapt<List<TModel>>();
        }

        public static TModel ToModel<TModel>(this List<object> obj)
        {
            return obj.Adapt<TModel>();
        }

        public static TModel ToModel<TModel>(this object obj)
        {
            return obj.Adapt<TModel>();
        }

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
            //var serializer = MessagePackSerializer.Get<IDictionary<string, object>>();
            return serializer.UnPickle<IDictionary<string, object>>(bytes);
        }

        public static List<IDictionary<string, object>> Deserialize(this byte[] bytes)
        {
            return serializer.UnPickle<List<IDictionary<string, object>>>(bytes);
        }

        public static object DeserializeByType<T>(this T  type, byte[] bytes)
        {
            return serializer.UnPickle<T>(bytes);
        }

        public static byte[] Serialize<T>(this T thisObj)
        {
            return serializer.Pickle(thisObj);
        }

      
        public static T Deserialize<T>(this byte[] bytes)
        {
            return serializer.UnPickle<T>(bytes);
        }
  
    }
}

