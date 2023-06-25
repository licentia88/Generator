using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using MemoryPack;
using Microsoft.EntityFrameworkCore;

namespace Generator.Shared.Models.ComponentModels.Abstracts;

[Equatable]
[MemoryPackable]
[MemoryPackUnion(0, typeof(HEADER_BUTTON_VIEW))]
[MemoryPackUnion(1, typeof(SIDE_BUTTON_VIEW))]
[Index(nameof(VBM_PAGE_REFNO), nameof(VBM_TYPE),nameof(VBM_TITLE), IsUnique =true)]
public abstract partial class VIEW_BASE_M
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VBM_ROWID { get; set; }

    [Required]
    public string VBM_TYPE { get; set; }

    public int VBM_PAGE_REFNO { get; set; }

    public string VBM_TITLE { get; set; }

    public string VBM_SOURCE { get; set; }

    [ForeignKey(nameof(ComponentModels.GRID_FIELDS.GF_VIEW_REFNO))]
    public ICollection<GRID_FIELDS> GRID_FIELDS { get; set; } = new HashSet<GRID_FIELDS>();
}
