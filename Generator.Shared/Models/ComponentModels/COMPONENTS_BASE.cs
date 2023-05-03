using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;


[ProtoContract]
[ProtoInclude(100,nameof(PAGES_M))]
//[ProtoInclude(200, nameof(GRIDS_D))]
[Table(nameof(COMPONENTS_BASE))]
public class COMPONENTS_BASE
{
    public COMPONENTS_BASE()
    {

    }

    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CB_ROWID { get; set; }

    [ProtoMember(2)]
    public string CB_TYPE { get; set; }

    [ProtoMember(3)]
    public string CB_TITLE { get; set; }

    [ProtoMember(4)]
    public string CB_IDENTIFIER { get; set; }

    [ProtoMember(5)]
    public string CB_DATABASE { get; set; }

    [ProtoMember(6)]
    public string CB_QUERY_METHOD { get; set; }

    [ProtoMember(7)]
    public int CB_COMMAND_TYPE { get; set; }

    [ProtoMember(8)]
    [ForeignKey(nameof(ComponentModels.PERMISSIONS.PER_COMPONENT_REFNO))]
    public ICollection<PERMISSIONS> PERMISSIONS { get; set; } = new HashSet<PERMISSIONS>();
}
