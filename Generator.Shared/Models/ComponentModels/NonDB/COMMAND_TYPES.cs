using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels.NonDB;

[NotMapped]
[MemoryPackable]
public partial class COMMAND_TYPES
{
    public int CT_ROWID { get; set; }

    public string CT_DESC { get; set; }
}