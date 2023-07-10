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
[MemoryPackUnion(2, typeof(GRID_VIEW))]
[Index(nameof(VB_GRID_REFNO), nameof(VB_TYPE),nameof(VBM_TITLE), IsUnique =true)]
public abstract partial class VIEW_BASE
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VB_ROWID { get; set; }

    [Required]
    public string VB_TYPE { get; set; }

    public int VB_GRID_REFNO { get; set; }

    public string VBM_TITLE { get; set; }

    [ForeignKey(nameof(ComponentModels.GRID_FIELDS.GF_COMPONENT_REFNO))]
    public ICollection<GRID_FIELDS> GRID_FIELDS { get; set; } = new HashSet<GRID_FIELDS>();
}
