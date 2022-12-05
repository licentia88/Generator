using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models;

[ProtoContract]
[Table(nameof(HEADER_BUTTON))]
public class HEADER_BUTTON: COMPONENT
{
    public HEADER_BUTTON()
    {
        COMP_TYPE = nameof(HEADER_BUTTON);
    }
   

    [ProtoMember(1)]
    public int HB_GRID_REFNO { get; set; }
}
