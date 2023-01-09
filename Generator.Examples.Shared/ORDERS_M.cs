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

    [ProtoMember(3)]
    public string OM_DESCRIPTION { get; set; }

    [ForeignKey(nameof(Shared.ORDERS_D.OD_M_REFNO))]
    public ICollection<ORDERS_D> ORDERS_D { get; set; } = new HashSet<ORDERS_D>();
}
