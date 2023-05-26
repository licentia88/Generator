using MessagePack;
 
namespace Generator.Shared.Models.ServiceModels;

[MessagePackObject]
public class RESPONSE_RESULT<TModel>
{
    [Key(0)]
    public TModel Data { get; set; }

    public RESPONSE_RESULT()
    {

    }

    public RESPONSE_RESULT(TModel model)
    {
        Data = model;
    }
}

