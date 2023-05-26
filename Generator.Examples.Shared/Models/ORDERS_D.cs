 
 using System.ComponentModel.DataAnnotations;
 using System.ComponentModel.DataAnnotations.Schema;
 using Microsoft.EntityFrameworkCore;

namespace Generator.Examples.Shared.Models;

[MessagePack.MessagePackObject()]
public class ORDERS_D
{
    [MessagePack.Key(0)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OD_ROWID { get; set; }

    [MessagePack.Key(1)]
    public int OD_M_REFNO { get; set; }

    [MessagePack.Key(2)]
    public string OD_NAME { get; set; }

    [MessagePack.Key(3)]
    public int OD_QUANTITY { get; set; }

    [MessagePack.Key(4)]
    [Precision(19,2)]
    public decimal OD_PRICE { get; set; }
}
