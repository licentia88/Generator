using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels.NonDB;

[NotMapped]
[MemoryPackable]
public partial class STORED_PROCEDURES
{
    public string SP_NAME { get; set; }
}