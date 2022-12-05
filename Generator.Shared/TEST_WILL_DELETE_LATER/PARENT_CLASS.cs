using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Shared.TEST_WILL_DELETE_LATER;

public class PARENT_CLASS
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PC_ROWID { get; set; }

    public string PC_DESC { get; set; }

    [ForeignKey(nameof(Shared.TEST_WILL_DELETE_LATER.CHILD_CLASS.CC_PARENT_REFNO))]
    public ICollection<CHILD_CLASS> CHILD_CLASS { get; set; }
}

