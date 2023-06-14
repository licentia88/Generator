using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels.Abstracts;

[MemoryPackable]
[Table(nameof(AUTH_BASE))]
[MemoryPackUnion(3, typeof(ROLES))]
[MemoryPackUnion(4, typeof(PERMISSIONS))]
public abstract partial class AUTH_BASE
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AUTH_ROWID { get; set; }

    public string AUTH_TYPE { get; set; }

    public string AUTH_NAME { get; set; }

    public abstract bool IsAuthorized(int roleId);
    
}