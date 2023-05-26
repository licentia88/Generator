using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels.NonDB;

[NotMapped]
[MessagePackObject]
public class DATABASE_INFORMATION
{
    [Key(0)]
    public string DI_DATABASE_NAME { get; set; }
}