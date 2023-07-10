using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[MemoryPackable]
[Table(nameof(HEADER_BUTTON_VIEW))]
// ReSharper disable once InconsistentNaming
public partial class HEADER_BUTTON_VIEW : VIEW_BASE
{
    public HEADER_BUTTON_VIEW()
    {
        VB_TYPE = nameof(HEADER_BUTTON_VIEW);
    }
}