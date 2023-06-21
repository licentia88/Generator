using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;
using MessagePack;

namespace Generator.Examples.Shared.Models;

[MemoryPackable()]
public partial class COMPUTED_TABLE
{
    [System.ComponentModel.DataAnnotations.Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CT_ROWID { get; set; }

    public string CT_DESC { get; set; }
}

