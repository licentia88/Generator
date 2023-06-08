 using System.ComponentModel.DataAnnotations.Schema;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;


[MessagePackObject]
[Table(nameof(PERMISSIONS))]
public class PERMISSIONS: AUTH_BASE
{
    public PERMISSIONS()
    {
        AUTH_TYPE = nameof(PERMISSIONS);
    }

    [Key(3)]
    public string PER_DESCRIPTION { get; set; }

    [Key(4)]
    [Annotation.Required(ErrorMessage = "*")]
    public int PER_COMPONENT_REFNO { get; set; }
}









