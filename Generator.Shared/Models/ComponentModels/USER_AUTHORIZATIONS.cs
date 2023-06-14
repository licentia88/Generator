using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[MemoryPackable]
public partial class USER_AUTHORIZATIONS
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UA_ROWID { get; set; }

    public int UA_USER_REFNO { get; set; }

    public int? UA_AUTH_CODE { get; set; }

    [ForeignKey(nameof(UA_AUTH_CODE))]
    public AUTH_BASE AUTH_BASE { get; set; }
}









