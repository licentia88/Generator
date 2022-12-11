using System.Dynamic;
using Generator.Shared.Extensions;
using Mapster;
using ProtoBuf;

namespace Generator.Shared.Models;

[ProtoContract]
public class GenObject : DynamicObject
{
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

 

    [ProtoMember(1)]
    private byte[] Bytes { get; set; }

    [ProtoMember(2)]
    public bool IsList { get; }


    [ProtoIgnore] private IDictionary<string, object> Dictionary { get; set; } = new ExpandoObject();

    

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        if (Dictionary.ContainsKey(binder.Name))
        {
            result = Dictionary[binder.Name];
            return true;
        }
        else
        {
            result = "Invalid Property!";
            return false;
        }
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        Dictionary[binder.Name] = value;
        return true;
    }

    public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
    {
        dynamic method = Dictionary[binder.Name];
        result = method(args[0].ToString(), args[1].ToString());
        return true;
    }

    public void Add(string name, int value)
    {
        Dictionary.Add(name, value);
        Bytes = Dictionary.Serialize();
    }


    //public IEnumerable<object> DynamicData
    //{
    //    get
    //    {
    //        if (IsList)
    //        {
    //            var deserializedList = Bytes.Deserialize<List<IDictionary<string, object>>>();

    //            foreach (var data in deserializedList)
    //            {
    //                yield return data.Adapt<ExpandoObject>();
    //            }
    //        }
    //        else
    //        {
    //            Dictionary = Bytes.Deserialize<IDictionary<string, object>>();
    //            yield return Dictionary.Adapt<ExpandoObject>();
    //        }


    //    }
    //}

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
            Dictionary = Bytes.Deserialize<IDictionary<string, object>>();
            yield return Dictionary.Adapt<T>();
        }
    }

    public IEnumerable<object> DynamicData()
    {
        return DynamicData<ExpandoObject>();
    }
}


