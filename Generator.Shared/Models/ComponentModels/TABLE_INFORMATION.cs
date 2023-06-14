using System.ComponentModel.DataAnnotations.Schema;
using Generator.Shared.Models.ComponentModels.NonDB;
using MemoryPack;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[NotMapped]
[MemoryPackable]
// ReSharper disable once InconsistentNaming
public partial class TABLE_INFORMATION 
{
    public string TI_TABLE_NAME { get; set; }
}