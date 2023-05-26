using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Examples.Shared.Models;

[MessagePack.MessagePackObject()]
public class ORDERS_M
{
    [MessagePack.Key(0)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OM_ROWID { get; set; }

    [MessagePack.Key(1)]
    public int OM_USER_REFNO { get; set; }

    [Required]
    [MessagePack.Key(2)]
    public string OM_DESCRIPTION { get; set; }

    [MessagePack.Key(3)]
    public string OM_MORE_FIELD_ONE { get; set; }

    [MessagePack.Key(4)]
    public string OM_MORE_FIELD_TWO { get; set; }

    [MessagePack.Key(5)]
    public string OM_MORE_FIELD_THREE { get; set; }


    [MessagePack.Key(6)]
    public string OM_MORE_FIELD_FOUR { get; set; }

    [MessagePack.Key(7)]
    public string OM_MORE_FIELD_FIVE{ get; set; }

    [MessagePack.Key(8)]
    public string OM_MORE_FIELD_SIX { get; set; }

    [MessagePack.Key(9)]
    public string OM_MORE_FIELD_SEVEN { get; set; }


    [MessagePack.Key(10)]
    public string OM_MORE_FIELD_EIGHT { get; set; }

    [MessagePack.Key(11)]
    [ForeignKey(nameof(Models.ORDERS_D.OD_M_REFNO))]
    public ICollection<ORDERS_D> ORDERS_D { get; set; } = new HashSet<ORDERS_D>();
}
