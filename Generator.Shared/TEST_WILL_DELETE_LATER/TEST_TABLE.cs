using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Shared.TEST_WILL_DELETE_LATER;

public class TEST_TABLE
{
	[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int TT_ROWID { get; set; }

	public string TT_DESC { get; set; }
}

public class COMPUTED_TABLE
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CT_ROWID { get; set; }

    public string CT_DESC { get; set; }
}

public class STRING_TABLE
{
    [Key]
    public string CT_ROWID { get; set; }

    public string CT_DESC { get; set; }
}

