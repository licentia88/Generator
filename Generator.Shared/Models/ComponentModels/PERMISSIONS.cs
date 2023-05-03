using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract]
public class PERMISSIONS
{
    [ProtoMember(1)]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PER_ROWID { get; set; }

    [ProtoMember(2)]
    public int PER_COMPONENT_REFNO { get; set; }
}