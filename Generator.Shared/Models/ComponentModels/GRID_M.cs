using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
public class GRID_M: GRID_BASE
{
    [Key(16)]
    [Shema.ForeignKey(nameof(ComponentModels.GRID_D.GD_M_REFNO))]
    public ICollection<GRID_D> GRID_D { get; set; }
}
