using MessagePack;

namespace Generator.Shared.Models.ComponentModels.Abstracts;


[MessagePackObject]
[Union(0, typeof(GRID_BASE))]
public abstract class COMPONENTS_BASE
{
    [Key(0)]
    [Annotation.Key, Shema.DatabaseGenerated(Shema.DatabaseGeneratedOption.Identity)]
    public int CB_ROWID { get; set; }

    [Key(1)]
    public string CB_TYPE { get; set; }

    [Key(2)]
    public string CB_TITLE { get; set; }

    [Key(3)]
    public string CB_IDENTIFIER { get; set; }

    [Key(4)]
    public string CB_DATABASE { get; set; }

    [Key(5)]
    public string CB_QUERY_OR_METHOD { get; set; }

    [Key(6)]
    public int CB_COMMAND_TYPE { get; set; }

    [Key(7)]
    [Shema.ForeignKey(nameof(ComponentModels.PERMISSIONS.PER_COMPONENT_REFNO))]
    public ICollection<PERMISSIONS> PERMISSIONS { get; set; } = new HashSet<PERMISSIONS>();
}
