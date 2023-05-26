using System.ComponentModel.DataAnnotations;

namespace Generator.Examples.Shared.Models;

[MessagePack.MessagePackObject()]
public class STRING_TABLE
{
    [Key]
    [MessagePack.Key(0)]
    public string CT_ROWID { get; set; }

    [MessagePack.Key(1)]
    public string CT_DESC { get; set; }
}

