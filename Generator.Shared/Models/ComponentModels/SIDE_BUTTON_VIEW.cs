using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[MemoryPackable]
[Table(nameof(SIDE_BUTTON_VIEW))]
// ReSharper disable once InconsistentNaming
public partial class SIDE_BUTTON_VIEW : VIEW_BASE
{
    public SIDE_BUTTON_VIEW()
    {
        VB_TYPE = nameof(SIDE_BUTTON_VIEW);
    }
}

