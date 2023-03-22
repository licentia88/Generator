using System.Dynamic;
using Generator.Shared.Extensions;
using Mapster;
using ProtoBuf;

namespace Generator.Shared.Models;

[ProtoContract]
public class GenObject
{
    [ProtoMember(1)]
    private byte[] Bytes { get; set; }

    [ProtoMember(2)]
    public bool IsList { get; }

    public GenObject()
    {
        IsList = false;
    }

    public GenObject(IDictionary<string, object> data)
    {
        Bytes = data.Serialize();
        IsList = false;
    }

    public GenObject(List<IDictionary<string, object>> data)
    {
        Bytes = data.Serialize();
        IsList = true;
    }

    public GenObject(object data)
    {
        Bytes = data.AdaptToDictionary().Serialize();
        IsList = false;
    }

    public GenObject(List<object> data)
    {
        Bytes = data.AdaptToDictionary().Serialize();
        IsList = true;
    }

 

    public IEnumerable<T> DynamicData<T>()
    {
         if (IsList)
         {
            var deserializedList = Bytes.Deserialize<List<IDictionary<string, object>>>();

            foreach (var data in deserializedList)
            {
                yield return data.Adapt<T>();
            }
        }
        else
        {
            yield return Bytes.Deserialize<IDictionary<string, object>>().Adapt<T>(); 
            //yield return Dictionary
        }
    }

    public IEnumerable<object> DynamicData()
    {
        return DynamicData<ExpandoObject>();
    }
}


