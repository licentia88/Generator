using System.ComponentModel.DataAnnotations;
using ProtoBuf;

namespace Generator.Examples.Shared.Models;

[ProtoContract]
public class STRING_TABLE
{
    [Key]
    [ProtoMember(1)]
    public string CT_ROWID { get; set; }

    [ProtoMember(2)]
    public string CT_DESC { get; set; }
}

