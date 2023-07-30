using Generator.Shared.Enums;
using MemoryPack;

namespace Generator.Shared.Models.ServiceModels;

//[MemoryPackable]
//public partial struct PipeData
//{
   
//    public PipeData(string host, Operation? operation)
//    {
//        Host = host;
//        Operation = operation;
//    }
 
//    public static PipeData New(string host, Operation? operation)
//    {
//        return new PipeData(host, operation);
//    }

//    public string Host { get; set; }

//    public Operation? Operation { get; set; }
//}

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
