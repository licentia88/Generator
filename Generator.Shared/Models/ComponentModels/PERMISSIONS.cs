using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

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

    public COMPONENTS_BASE COMPONENTS_BASE { get; set; }

    [MemoryPackIgnore]
    public string Description => $"{COMPONENTS_BASE?.CB_TITLE} - {PER_DESCRIPTION}";

    public override bool IsAuthorized(int roleId)
    {
        return AUTH_ROWID == roleId;
    }
}









