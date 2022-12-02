using System;
using ProtoBuf;

namespace Generator.Shared.Models;

[ProtoContract]
public class RESPONSE_RESULT<TModel>
{
    [ProtoMember(1)]
    public TModel Data { get; set; }

    public RESPONSE_RESULT()
    {

    }

    public RESPONSE_RESULT(TModel model)
    {
        Data = model;
    }
}

