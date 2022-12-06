using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace Generator.Shared.Models;

[ProtoContract]

public class RESPONSE_RESULT 
{
    [ProtoMember(1)]
    public byte[] Data { get; set; }

     

    public RESPONSE_RESULT()
    {
    }

    public RESPONSE_RESULT(byte[] bytes)
    {
        Data = bytes; 
    }

    

}


 