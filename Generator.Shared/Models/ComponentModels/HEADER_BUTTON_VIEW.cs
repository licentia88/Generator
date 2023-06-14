using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MemoryPackable]
// ReSharper disable once InconsistentNaming
public partial class HEADER_BUTTON_VIEW : VIEW_BASE_M
{
    public HEADER_BUTTON_VIEW()
    {
        VBM_TYPE = nameof(HEADER_BUTTON_VIEW);
    }
}