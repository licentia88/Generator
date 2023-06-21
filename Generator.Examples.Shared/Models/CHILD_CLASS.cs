using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;
using MessagePack;

namespace Generator.Examples.Shared.Models;


[MemoryPackable()]
public partial class CHILD_CLASS
{
    [System.ComponentModel.DataAnnotations.Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CC_ROWID { get; set; }

    public string CC_DESC { get; set; }

    public int CC_PARENT_REFNO { get; set; }
}

