using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Examples.Shared.Models;

[MessagePack.MessagePackObject()]
public class PARENT_CLASS
{
    [MessagePack.Key(0)]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PC_ROWID { get; set; }

    [MessagePack.Key(1)]
    public string PC_DESC { get; set; }

    [MessagePack.Key(2)]
    public string PC_STRING_TABLE_CODE { get; set; }

    [MessagePack.Key(3)]
    [ForeignKey(nameof(PC_STRING_TABLE_CODE))]
    public STRING_TABLE STRING_TABLE { get; set; }

    [MessagePack.Key(4)]
    [ForeignKey(nameof(Models.CHILD_CLASS.CC_PARENT_REFNO))]
    public ICollection<CHILD_CLASS> CHILD_CLASS { get; set; }
}

