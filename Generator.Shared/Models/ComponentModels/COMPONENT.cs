using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models;

[ProtoContract]
//[ProtoInclude(100, typeof(GRIDS_M))]
//[ProtoInclude(200, typeof(GRIDS_D))]
//[ProtoInclude(600, typeof(HEADER_BUTTON))]
//[ProtoInclude(700, typeof(FOOTER_BUTTON))]
[Table(nameof(COMPONENT))]
public  class COMPONENT
{
    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int COMP_ROWID { get; set; }

    [ProtoMember(2)]
    public string COMP_TYPE { get; set; }

    [ProtoMember(3)]
    public string COMP_TITLE { get; set; }

    [ProtoMember(4)]
    public string COMP_DATABASE { get; set; }

    [ProtoIgnore]
    [ForeignKey(nameof(COMP_DATABASE))]
    public DATABASE DATABASES { get; set; }
}
