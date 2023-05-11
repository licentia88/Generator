using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Examples.Shared.Models;

public class PARENT_CLASS
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PC_ROWID { get; set; }

    public string PC_DESC { get; set; }

    public string PC_STRING_TABLE_CODE { get; set; }

    [ForeignKey(nameof(PC_STRING_TABLE_CODE))]
    public STRING_TABLE STRING_TABLE { get; set; }

    [ForeignKey(nameof(Models.CHILD_CLASS.CC_PARENT_REFNO))]
    public ICollection<CHILD_CLASS> CHILD_CLASS { get; set; }
}

