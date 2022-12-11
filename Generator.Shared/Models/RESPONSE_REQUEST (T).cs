using ProtoBuf;

namespace Generator.Shared.Models;

[ProtoContract]
public class RESPONSE_REQUEST<TModel>
{
    [ProtoMember(1)]
    public TModel RR_DATA { get; set; }

    public RESPONSE_REQUEST()
    {

    }

    public RESPONSE_REQUEST(TModel model)
    {
        RR_DATA = model;
    }
}
