using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[MemoryPackable]
[Table(nameof(ROLES))]
public partial class ROLES : AUTH_BASE
{
    public ROLES()
    {
        AUTH_TYPE = nameof(ROLES);
    }

    [ForeignKey(nameof(ComponentModels.ROLES_DETAILS.RD_M_REFNO))]
    public ICollection<ROLES_DETAILS> ROLES_DETAILS { get; set; } = new HashSet<ROLES_DETAILS>();

    public override bool IsAuthorized(int roleId)
    {
        var result = ROLES_DETAILS.FirstOrDefault(x => x.RD_PERMISSION_REFNO == roleId);

        return result is not null;
    }
}









