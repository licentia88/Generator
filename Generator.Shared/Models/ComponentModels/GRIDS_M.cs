using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract]
[ProtoInclude(200, nameof(PAGES_D))]
[Table(nameof(PAGES_M))]
public class PAGES_M:COMPONENTS_BASE
{
    public PAGES_M()
    {
        CB_TYPE = nameof(PAGES_M);
    }

    [ProtoMember(1)]
    public int PM_EDIT_MODE { get; set; }

    [ProtoMember(2)]
    public int PM_EDIT_TRIGGER { get; set; }

    [ProtoMember(3)]
    public int PM_MAX_WIDTH { get; set; }

    [ProtoMember(4)]
    public bool PM_DENSE { get; set; }

    [ProtoMember(5)]
    [Required(ErrorMessage = "*")]
    public int? PM_ROWS_PER_PAGE { get; set; }

    [ProtoMember(6)]
    public bool PM_ENABLE_SORTING { get; set; }

    [ProtoMember(7)]
    public bool PM_ENABLE_FILTERING { get; set; }

    [ProtoMember(8)]
    public bool PM_STRIPED { get; set; }

    [ProtoMember(9)]
    [ForeignKey(nameof(ComponentModels.VIEW_BASE.VB_PAGE_REFNO))]
    public ICollection<VIEW_BASE> VIEW_BASE { get; set; }

    [ProtoMember(10)]
    [ForeignKey(nameof(ComponentModels.PAGES_D.PD_M_REFNO))]
    public ICollection<PAGES_D> PAGES_D { get; set; }
}

[ProtoContract]
[Table(nameof(PAGES_D))]
public class PAGES_D: PAGES_M
{
    public PAGES_D()
    {
        CB_TYPE = nameof(PAGES_D);
    }

    [ProtoMember(1)]
    public int PD_M_REFNO { get; set; }
}



