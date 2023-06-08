﻿using System.ComponentModel.DataAnnotations.Schema;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
[Table(nameof(ROLES))]
public class ROLES : AUTH_BASE
{
    public ROLES()
    {
        AUTH_TYPE = nameof(ROLES);
    }

    [Key(3)]
    [ForeignKey(nameof(ComponentModels.ROLES_DETAILS.RD_M_REFNO))]
    public ICollection<ROLES_DETAILS> ROLES_D { get; set; } = new HashSet<ROLES_DETAILS>();
}









