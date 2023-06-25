using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[MemoryPackable()]
[Table(nameof(GRID_D))]
public partial class GRID_D: GRID_M, IGridRef
{
    public GRID_D()
    {
        CB_TYPE = nameof(GRID_D);
    }

    public int GD_M_REFNO { get; set; }
}
