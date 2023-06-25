using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using Generator.Shared.Enums;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable(IgnoreInheritedMembers =true)]
[MemoryPackable(GenerateType.CircularReference, SerializeLayout.Sequential)]
[Table(nameof(PERMISSIONS))]
public partial class PERMISSIONS: AUTH_BASE
{
    public PERMISSIONS()
    {
        AUTH_TYPE = nameof(PERMISSIONS);
    }

    public string PER_DESCRIPTION { get; set; }

    [Required(ErrorMessage = "*")]
    public int PER_COMPONENT_REFNO { get; set; }

    [IgnoreEquality]
    public COMPONENTS_BASE COMPONENTS_BASE { get; set; }

 
    public override bool IsAuthorized(int roleId)
    {
        return AUTH_ROWID == roleId;
    }

    public PERMISSIONS SetPermissionInfo(string Title, Operation operation)
    {
        AUTH_NAME = Title;
        PER_DESCRIPTION = $"{operation} Permissions";

        return this;
    }
    
}









