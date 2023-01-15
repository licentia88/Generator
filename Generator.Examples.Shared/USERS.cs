using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Examples.Shared;

[ProtoContract]
public class USER
{
    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int U_ROWID { get; set; }

    [Required]
    [ProtoMember(2)]
    public string U_NAME { get; set; }

    [ProtoMember(3)]
    public int U_AGE { get; set; }

    [ProtoMember(4)]
    public DateTime U_REGISTER_DATE { get; set; }

    [ForeignKey(nameof(Shared.ORDERS_M.OM_USER_REFNO))]
    public ICollection<ORDERS_M> ORDERS_M { get; set; } = new HashSet<ORDERS_M>();
}
