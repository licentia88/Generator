using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ProtoBuf;

namespace Generator.Shared.Models;

[ProtoContract]
[ProtoInclude(500,typeof(GRIDS_D))]
[Table(nameof(GRIDS_M))]
public class GRIDS_M: COMPONENT
{
    public GRIDS_M()
    {
        COMP_TYPE = nameof(GRIDS_M);
    }
 
    [ProtoMember(1)]
    [ForeignKey(nameof(Models.GRIDS_D.GD_M_REFNO))]
    public ICollection<GRIDS_D> GRIDS_D { get; set; } = new HashSet<GRIDS_D>();

    [ProtoMember(2)]
    [ForeignKey(nameof(Models.HEADER_BUTTON.HB_GRID_REFNO))]
    public ICollection<HEADER_BUTTON> HEADER_BUTTON { get; set; } = new HashSet<HEADER_BUTTON>();

    [ProtoMember(3)]
    [ForeignKey(nameof(Models.FOOTER_BUTTON.FB_GRID_REFNO))]
    public ICollection<FOOTER_BUTTON> FOOTER_BUTTON { get; set; } = new HashSet<FOOTER_BUTTON>();
}
