using ProtoBuf;

namespace Generator.Examples.Shared;

[ProtoContract]
public class RESPONSE_REQUEST<T>
{
    [ProtoMember(1)]
    public T Data { get; set; }

    public RESPONSE_REQUEST()
    {

    }

    public RESPONSE_REQUEST(T data)
    {
        Data = data;
    }
}
