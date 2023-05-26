using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels.NonDB;

[NotMapped]
[MessagePackObject]
public class COMMAND_TYPES
{
    [Key(0)]
    public int CT_ROWID { get; set; }

    [Key(1)]
    public string CT_DESC { get; set; }
}