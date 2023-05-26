using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;
namespace Generator.Shared.Models.ComponentModels.NonDB;

[NotMapped]
[MessagePackObject]
public class DISPLAY_FIELD_INFORMATION
{
    [Key(0)]
    public string DFI_NAME { get; set; }

    [Key(1)]
    public bool DFI_IS_SEARCH_FIELD { get; set; }

}