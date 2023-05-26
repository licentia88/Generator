using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[NotMapped]
[MessagePackObject]
// ReSharper disable once InconsistentNaming
public class TABLE_INFORMATION
{
    [Key(0)]
    public string TI_TABLE_NAME { get; set; }
}