using System.ComponentModel.DataAnnotations;
using Generator.Shared.Extensions;
using MBrace.FsPickler;
using ProtoBuf;

namespace Generator.Shared.Models;

[ProtoContract]

public class RESPONSE_RESULT 
{
    //[ProtoMember(1)]
    //public byte[] Data { get; set; }

    [ProtoMember(1)]
    public GenObject GenObject { get; set; }


    public RESPONSE_RESULT()
    {
    }

    public RESPONSE_RESULT(GenObject data)
    {
        GenObject = data;
    }

    //public RESPONSE_RESULT(byte[] bytes)
    //{
    //    Data = bytes; 
    //}


    //public void DeserializeToObject()
    //{
    //    var serializer = FsPickler.CreateBinarySerializer();

    //    serializer.UnPickle<IDictionary<string,object>>
    //}

}


 