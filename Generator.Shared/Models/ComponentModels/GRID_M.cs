using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using Generator.Shared.Helpers;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[MemoryPackable]
[MemoryPackUnion(5, typeof(GRID_M))]
[MemoryPackUnion(6, typeof(GRID_D))]
public partial interface IGridRef
{
    public ICollection<GRID_D> GRID_D { get; set; }
}

[Equatable]
[MemoryPackable]
[Table(nameof(GRID_M))]
public partial class GRID_M : GRID_BASE, IGridRef
{
    public GRID_M()
    {
        CB_TYPE = nameof(GRID_M);
        CB_IDENTIFIER = RandomStringGenerator.GenerateRandomString('G');
    }

    [ForeignKey(nameof(ComponentModels.GRID_D.GD_M_REFNO))]
    public ICollection<GRID_D> GRID_D { get; set; }
}
