using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels.Abstracts;

[MessagePackObject]
[Table(nameof(AUTH_BASE))]
[Union(1, typeof(ROLES))]
[Union(2, typeof(PERMISSIONS))]
public abstract class AUTH_BASE
{
    [Key(0)]
    [Annotation.Key,Shema.DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AUTH_ROWID { get; set; }

    [Key(1)]
    public string AUTH_TYPE { get; set; }

    [Key(2)]
    public string AUTH_NAME { get; set; }

    public abstract bool IsAuthorized(int roleId);
    
}