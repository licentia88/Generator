using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
public class ROLES_DETAILS
{
    [Key(0)]
    [Annotation.Key, Shema.DatabaseGenerated(Shema.DatabaseGeneratedOption.Identity)]
    public int RD_ROWID { get; set; }

    [Key(1)]
    public int RD_M_REFNO { get; set; }

    [Key(2)]
    public int? RD_PERMISSION_REFNO { get; set; }

    [Key(3)]
    [ForeignKey(nameof(RD_PERMISSION_REFNO))]
    public PERMISSIONS PERMISSIONS { get; set; }

}









