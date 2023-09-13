using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;

namespace Generator.Examples.Shared.Models;

[MemoryPackable]
public partial class GENDER
{
    [Key]
    public string GEN_CODE { get; set; }

    public string GEN_DESC { get; set; }
}

[MemoryPackable()]
public partial class ORDERS_M
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OM_ROWID { get; set; }

    public int OM_USER_REFNO { get; set; }

    [Required]
    public string OM_DESCRIPTION { get; set; }

    public string OM_MORE_FIELD_ONE { get; set; }

    public string OM_MORE_FIELD_TWO { get; set; }

    public string OM_MORE_FIELD_THREE { get; set; }

    public string OM_MORE_FIELD_FOUR { get; set; }

    public string OM_MORE_FIELD_FIVE{ get; set; }

    public string OM_MORE_FIELD_SIX { get; set; }

    public string OM_MORE_FIELD_SEVEN { get; set; }

    public string OM_MORE_FIELD_EIGHT { get; set; }

    [ForeignKey(nameof(Models.ORDERS_D.OD_M_REFNO))]
    public ICollection<ORDERS_D> ORDERS_D { get; set; } = new HashSet<ORDERS_D>();
}
