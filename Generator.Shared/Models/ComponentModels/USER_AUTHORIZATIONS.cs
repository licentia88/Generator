using System.ComponentModel.DataAnnotations.Schema;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
public class USER_AUTHORIZATIONS
{
    [Key(0)]
    [Annotation.Key, Shema.DatabaseGenerated(Shema.DatabaseGeneratedOption.Identity)]
    public int UA_ROWID { get; set; }

    [Key(1)]
    public int UA_USER_REFNO { get; set; }

    [Key(2)]
    public int? UA_AUTH_CODE { get; set; }

    [Key(3)]
    [ForeignKey(nameof(UA_AUTH_CODE))]
    public AUTH_BASE AUTH_BASE { get; set; }
}









