using MemoryPack;
using MessagePack;

namespace Generator.Shared.Models.ServiceModels;

[MemoryPackable]
public partial class RESPONSE_REQUEST<TModel>
{
    //[Key(0)]
    public TModel Data { get; set; }

    public RESPONSE_REQUEST()
    {

    }

    [MemoryPackConstructor]
    public RESPONSE_REQUEST(TModel data)
    {
        Data = data;
    }
}
