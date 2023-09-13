using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;

namespace Generator.Examples.Shared.Models;

[MemoryPackable]
public partial class USER
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int U_ROWID { get; set; }

    //[Required(ErrorMessage = "Zorunlu alan")]
    [DisplayName("User")]
    public string U_NAME { get; set; }

    [Required(ErrorMessage = "Zorunlu alan")]
    [DisplayName("Last Name")]
    public string U_LASTNAME { get; set; }

    public int U_AGE { get; set; }

    public DateTime U_REGISTER_DATE { get; set; }

    public bool U_IS_MARRIED { get; set; }

    public string U_GEN_CODE { get; set; }

    [ForeignKey(nameof(U_GEN_CODE))]
    public GENDER GENDER { get; set; }

    [ForeignKey(nameof(Models.ORDERS_M.OM_USER_REFNO))]
    public ICollection<ORDERS_M> ORDERS_M { get; set; } = new HashSet<ORDERS_M>();
}
