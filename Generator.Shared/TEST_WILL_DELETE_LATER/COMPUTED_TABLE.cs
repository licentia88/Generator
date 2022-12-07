using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Shared.TEST_WILL_DELETE_LATER;

public class COMPUTED_TABLE
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CT_ROWID { get; set; }

    public string CT_DESC { get; set; }
}

