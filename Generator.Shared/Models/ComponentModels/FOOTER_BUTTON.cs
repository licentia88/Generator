using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract()]
[Table(nameof(FOOTER_BUTTON))]
public class FOOTER_BUTTON : COMPONENT
{
    public FOOTER_BUTTON()
    {
        //COMP_TYPE = nameof(FOOTER_BUTTON);
    }

    [ProtoMember(1)]
    public int FB_GRID_REFNO { get; set; }

 
}
