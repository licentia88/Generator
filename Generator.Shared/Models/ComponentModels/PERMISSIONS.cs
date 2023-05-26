using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
public class PERMISSIONS
{
    [Key(0)]
    [Annotation.Key,Shema.DatabaseGenerated(Shema.DatabaseGeneratedOption.Identity)]
    public int PER_ROWID { get; set; }

    [Key(1)]
    public int PER_COMPONENT_REFNO { get; set; }
}