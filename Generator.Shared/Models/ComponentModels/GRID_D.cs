using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
public class GRID_D: GRID_BASE
{
    [Key(16)]
    public int GD_M_REFNO { get; set; }
}
