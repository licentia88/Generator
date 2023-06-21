 
 using System.ComponentModel.DataAnnotations;
 using System.ComponentModel.DataAnnotations.Schema;
using MemoryPack;
using Microsoft.EntityFrameworkCore;

namespace Generator.Examples.Shared.Models;

[MemoryPackable()]
public partial class ORDERS_D
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OD_ROWID { get; set; }

    public int OD_M_REFNO { get; set; }

    public string OD_NAME { get; set; }

    public int OD_QUANTITY { get; set; }

    [Precision(19,2)]
    public decimal OD_PRICE { get; set; }
}
