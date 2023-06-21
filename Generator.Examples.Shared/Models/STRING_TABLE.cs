using System.ComponentModel.DataAnnotations;
using MemoryPack;

namespace Generator.Examples.Shared.Models;

[MemoryPackable]
public partial class STRING_TABLE
{
    [Key]
    public string CT_ROWID { get; set; }

    public string CT_DESC { get; set; }
}

