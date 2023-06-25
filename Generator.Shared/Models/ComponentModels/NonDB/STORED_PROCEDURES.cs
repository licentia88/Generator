using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels.NonDB;

[NotMapped]
[MemoryPackable]
public partial class STORED_PROCEDURES
{
    public string SP_NAME { get; set; }
}