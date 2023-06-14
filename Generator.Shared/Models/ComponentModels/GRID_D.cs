//using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[MemoryPackable]
[Table(nameof(GRID_D))]
public partial class GRID_D: GRID_M, IGridRef
{
    public GRID_D()
    {
        CB_TYPE = nameof(GRID_D);
    }

    public int GD_M_REFNO { get; set; }
}
