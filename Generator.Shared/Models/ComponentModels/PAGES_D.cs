using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels
{
    [ProtoContract]
    [Table(nameof(PAGES_D))]
    [Index(nameof(PD_M_REFNO), IsUnique =true)]
    public class PAGES_D: PAGES_M
    {
		[ProtoMember(1)]
		public int PD_M_REFNO { get; set; }
	}
}

