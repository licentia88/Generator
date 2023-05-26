using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Generator.Examples.Shared.Models;

[MessagePackObject()]
public class COMPUTED_TABLE
{
    [Key(0)]
    [System.ComponentModel.DataAnnotations.Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CT_ROWID { get; set; }

    [Key(1)]
    public string CT_DESC { get; set; }
}

