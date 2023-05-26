using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Generator.Examples.Shared.Models;


[MessagePackObject()]
public class CHILD_CLASS
{
    [Key(0)]
    [System.ComponentModel.DataAnnotations.Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CC_ROWID { get; set; }

    [Key(1)]
    public string CC_DESC { get; set; }

    [Key(2)]
    public int CC_PARENT_REFNO { get; set; }
}

