using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[MemoryPackable]
public partial class ROLES_DETAILS
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RD_ROWID { get; set; }

    public int RD_M_REFNO { get; set; }

    public int RD_PERMISSION_REFNO { get; set; }

    [ForeignKey(nameof(RD_PERMISSION_REFNO))]
    public PERMISSIONS PERMISSIONS { get; set; }

}









