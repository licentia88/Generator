using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf.Grpc;

namespace Generator.Shared.TEST_WILL_DELETE_LATER;

[Serializable]
public class TEST_TABLE
{
	[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int TT_ROWID { get; set; }

	public string TT_DESC { get; set; }

	public DateTime TT_DATE { get; set; }

    public DateTime? TT_NULLABLE_DATE{ get; set; }

	public bool TT_BOOLEAN { get; set; }

	public string TT_DEFAULT_VALUE_STRING { get; set; } = "TESTME";

	public string TT_STRING_TABLE_CODE { get; set; }

}

 

