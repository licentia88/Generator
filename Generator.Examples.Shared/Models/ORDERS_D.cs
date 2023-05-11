using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProtoBuf;

namespace Generator.Examples.Shared.Models;

[ProtoContract]
public class ORDERS_D
{
    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OD_ROWID { get; set; }

    [ProtoMember(2)]
    public int OD_M_REFNO { get; set; }

    [ProtoMember(3)]
    public string OD_NAME { get; set; }

    [ProtoMember(4)]
    public int OD_QUANTITY { get; set; }

    [ProtoMember(5)]
    [Precision(19,2)]
    public decimal OD_PRICE { get; set; }
}
