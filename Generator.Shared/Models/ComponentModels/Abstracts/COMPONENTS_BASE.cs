using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Formats.Asn1;
using MemoryPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Generator.Shared.Models.ComponentModels.Abstracts;


[MemoryPackable]
[MemoryPackUnion(0, typeof(GRID_BASE))]
[MemoryPackUnion(1, typeof(GRID_M))]
[MemoryPackUnion(2, typeof(GRID_D))]
[Index(nameof(CB_IDENTIFIER),IsUnique =true)]
public abstract partial class COMPONENTS_BASE//:Annotation.IValidatableObject
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CB_ROWID { get; set; }

    public string CB_TYPE { get; set; }

    [Required(ErrorMessage = "*")]
    public string CB_TITLE { get; set; }

    public string CB_IDENTIFIER { get; set; }

    [Required(ErrorMessage = "*")]
    public string CB_DATABASE { get; set; }

    [Required(ErrorMessage = "*")]
    public string CB_QUERY_OR_METHOD { get; set; }

    [Required(ErrorMessage = "*")]
    public int CB_COMMAND_TYPE { get; set; }

    [ForeignKey(nameof(ComponentModels.PERMISSIONS.PER_COMPONENT_REFNO))]
    public ICollection<PERMISSIONS> PERMISSIONS { get; set; } = new HashSet<PERMISSIONS>();

    
    //public IEnumerable<Annotation.ValidationResult> Validate(Annotation.ValidationContext validationContext)
    //{
    //    var validationResultList = new List<Annotation.ValidationResult>();

    //    //if (string.IsNullOrEmpty(CB_IDENTIFIER))
    //    //    validationResultList.Add(new System.ComponentModel.DataAnnotations.ValidationResult("*"));

    //    return validationResultList;
    //}
}

 