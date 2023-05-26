using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Examples.Shared.Models;

[Serializable]
[MessagePack.MessagePackObject()]
public class TEST_TABLE
{
	[MessagePack.Key(0)]
	[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int TT_ROWID { get; set; }

	[MessagePack.Key(1)]
	public string TT_DESC { get; set; }

	[MessagePack.Key(2)]
	public DateTime TT_DATE { get; set; }

	[MessagePack.Key(3)]
    public DateTime? TT_NULLABLE_DATE{ get; set; }

    [MessagePack.Key(4)]
	public bool TT_BOOLEAN { get; set; }

	[MessagePack.Key(5)]
	public string TT_DEFAULT_VALUE_STRING { get; set; } = "TESTME";

	[MessagePack.Key(6)]
	public string TT_STRING_TABLE_CODE { get; set; }

}

 

