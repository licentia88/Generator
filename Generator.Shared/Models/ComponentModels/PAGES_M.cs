using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels
{
    [ProtoContract]
	[ProtoInclude(1100,nameof(PAGES_D))]
	[Table(nameof(PAGES_M))]
	public class PAGES_M: COMPONENT_BASE
    {
        public PAGES_M()
        {
            CB_COMPONENT_TYPE = nameof(PAGES_M);
        }
 
        [ProtoMember(1)]
        public bool PM_CREATE { get; set; }

        [ProtoMember(2)]
        public bool PM_READ { get; set; } 

        [ProtoMember(3)]
        public bool PM_UPDATE { get; set; }

        [ProtoMember(4)]
        public bool PM_DELETE { get; set; }

       
        /// <summary>
        /// Header ve ya Grid Button Tanimlari
        /// </summary>
        [ProtoMember(5)]
        [ForeignKey(nameof(ComponentModels.HEADER_BUTTONS.BB_PAGE_REFNO))]
        public ICollection<BUTTONS_BASE> BUTTONS_BASE { get; set; } = new HashSet<BUTTONS_BASE>();


        [ProtoMember(6)]
        [ForeignKey(nameof(ComponentModels.PAGES_D.PD_M_REFNO))]
        public ICollection<PAGES_D> PAGES_D { get; set; } = new HashSet<PAGES_D>();

    }
}

