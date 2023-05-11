﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Examples.Shared.Models;

[ProtoContract]
public class USER
{
    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int U_ROWID { get; set; }

    [Required(ErrorMessage = "Zorunlu alan")]
    [DisplayName("User")]
    [ProtoMember(2)]
    public string U_NAME { get; set; }

    [Required(ErrorMessage = "Zorunlu alan")]
    [DisplayName("Last Name")]
    [ProtoMember(3)]
    public string U_LASTNAME { get; set; }

    [ProtoMember(4)]
    public int U_AGE { get; set; }

    [ProtoMember(5)]
    public DateTime U_REGISTER_DATE { get; set; }

    [ProtoMember(6)]
    public bool U_IS_MARRIED { get; set; }

    [ProtoMember(7)]
    [ForeignKey(nameof(Models.ORDERS_M.OM_USER_REFNO))]
    public ICollection<ORDERS_M> ORDERS_M { get; set; } = new HashSet<ORDERS_M>();
}