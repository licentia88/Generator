using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Generator.Examples.Shared.Models;

[MessagePack.MessagePackObject()]
public class USER
{
    [MessagePack.Key(0)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int U_ROWID { get; set; }

    [Required(ErrorMessage = "Zorunlu alan")]
    [DisplayName("User")]
    [MessagePack.Key(1)]
    public string U_NAME { get; set; }

    [Required(ErrorMessage = "Zorunlu alan")]
    [DisplayName("Last Name")]
    [MessagePack.Key(2)]
    public string U_LASTNAME { get; set; }

    [MessagePack.Key(3)]
    public int U_AGE { get; set; }

    [MessagePack.Key(4)]
    public DateTime U_REGISTER_DATE { get; set; }

    [MessagePack.Key(5)]
    public bool U_IS_MARRIED { get; set; }

    [MessagePack.Key(6)]
    [ForeignKey(nameof(Models.ORDERS_M.OM_USER_REFNO))]
    public ICollection<ORDERS_M> ORDERS_M { get; set; } = new HashSet<ORDERS_M>();
}
