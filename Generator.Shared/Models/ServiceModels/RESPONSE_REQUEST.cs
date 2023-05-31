using MessagePack;

namespace Generator.Shared.Models.ServiceModels;

[MessagePackObject]
public class RESPONSE_REQUEST<TModel>
{
    [Key(0)]
    public TModel RR_DATA { get; set; }

    public RESPONSE_REQUEST()
    {

    }

    public RESPONSE_REQUEST(TModel model)
    {
        RR_DATA = model;
    }
}
