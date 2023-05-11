using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Examples.Shared.Models;

public class COMPUTED_TABLE
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CT_ROWID { get; set; }

    public string CT_DESC { get; set; }
}

