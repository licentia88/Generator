using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
public class USERS
{
    [Key(0)]
    [Annotation.Key, Shema.DatabaseGenerated(Shema.DatabaseGeneratedOption.Identity)]
    public int U_ROWID { get; set; }

    [Key(1)]
    public string U_USERNAME { get; set; }

    [Key(2)]
    [ForeignKey(nameof(ComponentModels.USER_AUTHORIZATIONS.UA_USER_REFNO))]
    public ICollection<USER_AUTHORIZATIONS> USER_AUTHORIZATIONS { get; set; } = new HashSet<USER_AUTHORIZATIONS>();
}









