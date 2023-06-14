using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[MemoryPackable]
public partial class USERS
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int U_ROWID { get; set; }

    public string U_USERNAME { get; set; }

    [ForeignKey(nameof(ComponentModels.USER_AUTHORIZATIONS.UA_USER_REFNO))]
    public ICollection<USER_AUTHORIZATIONS> USER_AUTHORIZATIONS { get; set; } = new HashSet<USER_AUTHORIZATIONS>();
}









