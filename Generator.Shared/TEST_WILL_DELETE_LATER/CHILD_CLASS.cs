using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Shared.TEST_WILL_DELETE_LATER;


public class CHILD_CLASS
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CC_ROWID { get; set; }

    public string CC_DESC { get; set; }

    public int CC_PARENT_REFNO { get; set; }
}

