using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
// ReSharper disable once InconsistentNaming
public class SIDE_BUTTON_VIEW : VIEW_BASE_M
{
    public SIDE_BUTTON_VIEW()
    {
        VBM_TYPE = nameof(SIDE_BUTTON_VIEW);
    }
}

