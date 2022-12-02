using ProtoBuf;

namespace Generator.Shared.Models;


[ProtoContract]
public class RESPONSE_REQUEST
{
    [ProtoMember(1)]
    public byte[] RR_DATA { get; set; }

    [ProtoMember(2)]
    public string TableName { get; set; }

    public RESPONSE_REQUEST()
    {

    }

    public RESPONSE_REQUEST(byte[] model)
    {
        RR_DATA = model;
    }
}



