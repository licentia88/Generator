using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract]
public class DISPLAY_FIELDS
{
    [ProtoMember(1)]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DF_ROWID { get; set; }

    [ProtoMember(2)]
    public int DF_COMPONENT_REFNO { get; set; }

    [ProtoMember(3)]
    public string DF_FIELD_NAME { get; set; }

    [ProtoMember(4)]
    public string DF_ALIAS_FIELD { get; set; }

    [ProtoMember(5)]
    public string DF_DATABASE { get; set; }

    [ProtoMember(6)]
    public string DF_TABLE_NAME { get; set; }
}
