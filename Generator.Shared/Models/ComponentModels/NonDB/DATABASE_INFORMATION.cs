using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels.NonDB;

[NotMapped]
[MemoryPackable]
public  partial class DATABASE_INFORMATION
{
    public string DI_DATABASE_NAME { get; set; }
}