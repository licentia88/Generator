using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
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



[ProtoContract]
public class Person
{
    [Key]
    public int TEST { get; set; }
}
