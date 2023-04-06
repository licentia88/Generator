using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels
{
    [ProtoContract]
    [Table(nameof(HEADER_BUTTONS))]
    public class HEADER_BUTTONS : BUTTONS_BASE
    {
        public HEADER_BUTTONS()
        {
            CB_COMPONENT_TYPE = nameof(HEADER_BUTTONS);
        }


    }
}

