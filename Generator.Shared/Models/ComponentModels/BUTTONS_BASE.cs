using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels
{
    [ProtoContract]
    [ProtoInclude(100, nameof(HEADER_BUTTONS))]
    [ProtoInclude(200, nameof(GRID_BUTTONS))]
    //[ProtoInclude(300, nameof(CRUD_BUTTONS))]
    [Table(nameof(BUTTONS_BASE))]
    public class BUTTONS_BASE : COMPONENT_BASE
    {

        [ProtoMember(1)]
        public int BB_PAGE_REFNO { get; set; }
        //[ProtoMember(1)]
        //public string BB_TOOLTIP { get; set; }

        //[ProtoMember(2)]
        //public string BB_ICON { get; set; }

        //[ProtoMember(3)]
        //public string BB_SIZE { get; set; }

        //[ProtoMember(4)]
        //public string BB_COLOR { get; set; }


    }
}

