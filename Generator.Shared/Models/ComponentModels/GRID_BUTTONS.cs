using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels
{
    [ProtoContract]
    [Table(nameof(GRID_BUTTONS))]
    public class GRID_BUTTONS : BUTTONS_BASE
    {
        public GRID_BUTTONS()
        {
            CB_COMPONENT_TYPE = nameof(GRID_BUTTONS);
        }
 
    }
}

