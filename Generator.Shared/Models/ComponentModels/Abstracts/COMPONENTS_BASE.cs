using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace Generator.Shared.Models.ComponentModels.Abstracts;


[MessagePackObject]
[Union(0, typeof(GRID_BASE))]
[Index(nameof(CB_IDENTIFIER),IsUnique =true)]
public abstract class COMPONENTS_BASE:Annotation.IValidatableObject
{
    [Key(0)]
    [Annotation.Key, Shema.DatabaseGenerated(Shema.DatabaseGeneratedOption.Identity)]
    public int CB_ROWID { get; set; }

    [Key(1)]
    public string CB_TYPE { get; set; }

    [Key(2)]
    [Annotation.Required(ErrorMessage = "*")]
    public string CB_TITLE { get; set; }

    [Key(3)]
    public string CB_IDENTIFIER { get; set; }

    [Key(4)]
    [Annotation.Required(ErrorMessage = "*")]
    public string CB_DATABASE { get; set; }

    [Key(5)]
    [Annotation.Required(ErrorMessage = "*")]
    public string CB_QUERY_OR_METHOD { get; set; }

    [Key(6)]
    [Annotation.Required(ErrorMessage = "*")]
    public int CB_COMMAND_TYPE { get; set; }

    [Key(7)]
    [Shema.ForeignKey(nameof(ComponentModels.PERMISSIONS.PER_COMPONENT_REFNO))]
    public ICollection<PERMISSIONS> PERMISSIONS { get; set; } = new HashSet<PERMISSIONS>();

    public IEnumerable<Annotation.ValidationResult> Validate(Annotation.ValidationContext validationContext)
    {
        var validationResultList = new List<Annotation.ValidationResult>();

        if (string.IsNullOrEmpty(CB_IDENTIFIER))
            validationResultList.Add(new System.ComponentModel.DataAnnotations.ValidationResult("*"));

        return validationResultList;
    }
}
