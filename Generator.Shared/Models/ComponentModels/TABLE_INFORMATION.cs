using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[NotMapped]
[MemoryPackable]
// ReSharper disable once InconsistentNaming
public partial class TABLE_INFORMATION 
{
    public string TI_TABLE_NAME { get; set; }
}