using Generator.Equals;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[MemoryPackable]
// ReSharper disable once InconsistentNaming
public partial class SIDE_BUTTON_VIEW : VIEW_BASE_M
{
    public SIDE_BUTTON_VIEW()
    {
        VBM_TYPE = nameof(SIDE_BUTTON_VIEW);
    }
}

