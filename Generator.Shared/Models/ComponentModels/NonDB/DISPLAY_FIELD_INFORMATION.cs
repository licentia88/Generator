using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels.NonDB;

[NotMapped]
[MemoryPackable]
public partial class DISPLAY_FIELD_INFORMATION
{
    public string DFI_NAME { get; set; }

    public bool DFI_IS_SEARCH_FIELD { get; set; }

}