using System.ComponentModel.DataAnnotations;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels.Abstracts;

[MemoryPackable]
[MemoryPackUnion(3, typeof(GRID_M))]
[MemoryPackUnion(4, typeof(GRID_D))]
public abstract partial class GRID_BASE:COMPONENTS_BASE
{
    [Required(ErrorMessage ="*")]
    public int GB_EDIT_MODE { get; set; }

    public int GB_EDIT_TRIGGER { get; set; }

    public int GB_MAX_WIDTH { get; set; }
   
    public bool GB_DENSE { get; set; }

    [Required(ErrorMessage = "*")]
    public int? GB_ROWS_PER_PAGE { get; set; }

    public bool GB_ENABLE_SORTING { get; set; }

    public bool GB_ENABLE_FILTERING { get; set; }

    public bool GB_STRIPED { get; set; }
 
}
