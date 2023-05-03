using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract]
[ProtoInclude(100, nameof(GRID_CRUD_VIEW))]
[ProtoInclude(200, nameof(HEADER_BUTTON_VIEW))]
[ProtoInclude(300, nameof(SIDE_BUTTON_VIEW))]
[Index(nameof(VB_PAGE_REFNO), nameof(VB_TYPE),nameof(VB_TITLE), IsUnique =true)]
[Table(nameof(VIEW_BASE))]
public class VIEW_BASE
{
   
    [ProtoMember(1)]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VB_ROWID { get; set; }

    [Required]
    [ProtoMember(2)]
    public string VB_TYPE { get; set; }

    [ProtoMember(3)]
    public int VB_PAGE_REFNO { get; set; }

    [ProtoMember(4)]
    public string VB_TITLE { get; set; }

    [ProtoMember(5)]
    public string CB_SOURCE { get; set; }

    [ProtoMember(6)]
    [ForeignKey(nameof(ComponentModels.GRID_FIELDS.GF_VIEW_REFNO))]
    public ICollection<GRID_FIELDS> GRID_FIELDS { get; set; } = new HashSet<GRID_FIELDS>();
}

[ProtoContract]
[Table(nameof(GRID_CRUD_VIEW))]
public class GRID_CRUD_VIEW : VIEW_BASE
{
    public GRID_CRUD_VIEW()
    {
        VB_TYPE = nameof(GRID_CRUD_VIEW);
        VB_TITLE = "CRUD";
    }

    [ProtoMember(1)]
    public bool GCV_CREATE { get; set; }

    [ProtoMember(2)]
    public bool GCV_UPDATE { get; set; }

    [ProtoMember(3)]
    public bool GCV_DELETE { get; set; }

}

[ProtoContract]
[Table(nameof(HEADER_BUTTON_VIEW))]
public class HEADER_BUTTON_VIEW : VIEW_BASE
{
    public HEADER_BUTTON_VIEW()
    {
        VB_TYPE = nameof(HEADER_BUTTON_VIEW);
    }
}

[ProtoContract]
[Table(nameof(SIDE_BUTTON_VIEW))]
public class SIDE_BUTTON_VIEW : VIEW_BASE
{
    public SIDE_BUTTON_VIEW()
    {
        VB_TYPE = nameof(SIDE_BUTTON_VIEW);
    }
}

