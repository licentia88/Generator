using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels.NonDB;

[NotMapped]
[MessagePackObject]
public class STORED_PROCEDURES
{
    [Key(0)]
    public string SP_NAME { get; set; }
}