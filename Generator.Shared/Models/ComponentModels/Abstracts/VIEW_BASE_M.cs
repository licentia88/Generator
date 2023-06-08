using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace Generator.Shared.Models.ComponentModels.Abstracts;

[MessagePackObject]
[Union(0, typeof(GRID_FIELDS))]
[Union(1, typeof(HEADER_BUTTON_VIEW))]
[Union(2, typeof(SIDE_BUTTON_VIEW))]
[Index(nameof(VBM_PAGE_REFNO), nameof(VBM_TYPE),nameof(VBM_TITLE), IsUnique =true)]
public abstract class VIEW_BASE_M
{

    [Key(0)]
    [Annotation.Key, Shema.DatabaseGenerated(Shema.DatabaseGeneratedOption.Identity)]
    public int VBM_ROWID { get; set; }

    [Annotation.Required]
    [Key(1)]
    public string VBM_TYPE { get; set; }

    [Key(2)]
    public int VBM_PAGE_REFNO { get; set; }

    [Key(3)]
    public string VBM_TITLE { get; set; }

    [Key(4)]
    public string VBM_SOURCE { get; set; }

    [Key(5)]
    [Shema.ForeignKey(nameof(ComponentModels.GRID_FIELDS.GF_VIEW_REFNO))]
    public ICollection<GRID_FIELDS> GRID_FIELDS { get; set; } = new HashSet<GRID_FIELDS>();
}
