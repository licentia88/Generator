using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Examples.Shared;

[ProtoContract]
public class ORDERS_M
{
    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OM_ROWID { get; set; }

    [ProtoMember(2)]
    public int OM_USER_REFNO { get; set; }

    [Required]
    [ProtoMember(3)]
    public string OM_DESCRIPTION { get; set; }

    [ProtoMember(4)]
    public string OM_MORE_FIELD_ONE { get; set; }

    [ProtoMember(5)]
    public string OM_MORE_FIELD_TWO { get; set; }

    [ProtoMember(6)]
    public string OM_MORE_FIELD_THREE { get; set; }


    [ProtoMember(7)]
    public string OM_MORE_FIELD_FOUR { get; set; }

    [ProtoMember(8)]
    public string OM_MORE_FIELD_FIVE{ get; set; }

    [ProtoMember(9)]
    public string OM_MORE_FIELD_SIX { get; set; }

    [ProtoMember(10)]
    public string OM_MORE_FIELD_SEVEN { get; set; }


    [ProtoMember(11)]
    public string OM_MORE_FIELD_EIGHT { get; set; }


    [ForeignKey(nameof(Shared.ORDERS_D.OD_M_REFNO))]
    public ICollection<ORDERS_D> ORDERS_D { get; set; } = new HashSet<ORDERS_D>();
}
