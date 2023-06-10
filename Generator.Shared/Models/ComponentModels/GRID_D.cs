//using System.ComponentModel.DataAnnotations;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
[Shema.Table(nameof(GRID_D))]
public class GRID_D: GRID_M, IGridRef
{
    public GRID_D()
    {
        CB_TYPE = nameof(GRID_D);
    }

    [Key(17)]
    public int GD_M_REFNO { get; set; }
}
