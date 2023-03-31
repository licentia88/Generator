using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generator.Shared.Models.ComponentModels;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract]
//[ProtoInclude(100, typeof(GRIDS_M))]
//[ProtoInclude(200, typeof(GRIDS_D))]
//[ProtoInclude(600, typeof(HEADER_BUTTON))]
//[ProtoInclude(700, typeof(FOOTER_BUTTON))]
[Table(nameof(COMPONENT))]
public  class COMPONENT
{
    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int COMP_ROWID { get; set; }

    [ProtoMember(2)]
    public string COMP_TYPE { get; set; }

    [ProtoMember(3)]
    public string COMP_TITLE { get; set; }

    [ProtoMember(4)]
    public string COMP_DATABASE { get; set; }

    [ProtoIgnore]
    [ForeignKey(nameof(COMP_DATABASE))]
    public DATABASES DATABASES { get; set; }
}



public class PAGE_BUTTONS
{
    public int PB_ROWID { get; set; }

    [ForeignKey(nameof(ComponentModels.BUTTONS.B_PAGE_BUTTON_REFNO))]
    public ICollection<BUTTONS> BUTTONS { get; set; }
}
public class BUTTONS
{
    public int B_ROWID { get; set; }

    public int B_PAGE_BUTTON_REFNO { get; set; }

    public string B_TITLE { get; set; }

    public string B_ICON { get; set; }

    public string B_TOOLTIP_TEXT { get; set; }

    public string B_STORED_PROCEDURE { get; set; }

    public object MyProperty { get; set; }
}
public class HEADER_BUTTONS: BUTTONS
{

}

public class GRID_BUTTONS: BUTTONS
{

}

[ProtoContract]
public class PAGES_M
{
    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PM_ROWID { get; set; }

    [ForeignKey(nameof(ComponentModels.PAGES_D.PD_M_REFNO))]
    public ICollection<PAGES_D> PAGES_D { get; set; }
}

[ProtoContract]
[Table(name:nameof(PAGES_D))]
public class PAGES_D : PAGES_M
{
    public int PD_M_REFNO { get; set; }
}