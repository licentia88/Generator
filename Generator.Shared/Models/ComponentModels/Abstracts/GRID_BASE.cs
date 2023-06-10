using MessagePack;

namespace Generator.Shared.Models.ComponentModels.Abstracts;

[MessagePackObject]
[Union(1, typeof(GRID_M))]
[Union(2, typeof(GRID_D))]
public abstract class GRID_BASE:COMPONENTS_BASE
{
    [Key(8)]
    [Annotation.Required(ErrorMessage ="*")]
    public int GB_EDIT_MODE { get; set; }

    [Key(9)]
    public int GB_EDIT_TRIGGER { get; set; }

    [Key(10)]
    public int GB_MAX_WIDTH { get; set; }

    [Key(11)]
    public bool GB_DENSE { get; set; }

    [Key(12)]
    [Annotation.Required(ErrorMessage = "*")]
    public int? GB_ROWS_PER_PAGE { get; set; }

    [Key(13)]
    public bool GB_ENABLE_SORTING { get; set; }

    [Key(14)]
    public bool GB_ENABLE_FILTERING { get; set; }

    [Key(15)]
    public bool GB_STRIPED { get; set; }

    //[Key(16)]
    //[Shema.ForeignKey(nameof(ComponentModels.VIEW_BASE.VB_PAGE_REFNO))]
    //public ICollection<VIEW_BASE> VIEW_BASE { get; set; }

    //[Key(17)]
    //[Shema.ForeignKey(nameof(ComponentModels.PAGES_D.PD_M_REFNO))]
    //public ICollection<PAGES_D> PAGES_D { get; set; }
}
