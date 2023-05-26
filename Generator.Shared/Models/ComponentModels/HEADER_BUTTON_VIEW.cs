using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
// ReSharper disable once InconsistentNaming
public class HEADER_BUTTON_VIEW : VIEW_BASE_M
{
    public HEADER_BUTTON_VIEW()
    {
        VBM_TYPE = nameof(HEADER_BUTTON_VIEW);
    }
}