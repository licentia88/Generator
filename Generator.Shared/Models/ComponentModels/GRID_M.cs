using System.Formats.Asn1;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePack.Union(0, typeof(GRID_M))]
[MessagePack.Union(1, typeof(GRID_D))]
public interface IGridRef
{
    public ICollection<GRID_D> GRID_D { get; set; }
}

[MessagePackObject]
[Shema.Table(nameof(GRID_M))]
public class GRID_M : GRID_BASE, IGridRef
{
    public GRID_M()
    {
        CB_TYPE = nameof(GRID_M);
    }

    [Key(16)]
    [Shema.ForeignKey(nameof(ComponentModels.GRID_D.GD_M_REFNO))]
    public ICollection<GRID_D> GRID_D { get; set; }
}
