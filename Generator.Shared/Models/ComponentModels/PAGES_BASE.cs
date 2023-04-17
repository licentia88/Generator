using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract]
[ProtoInclude(200, nameof(ComponentModels.PAGES_D))]
[Table(name: nameof(PAGES_M))]
public class PAGES_M: COMPONENTS_BASE
{
    [ProtoMember(5)]
    [MaxLength(10)]
    public string PB_EDIT_TRIGGER { get; set; }

    [ProtoMember(6)]
    [MaxLength(9)]
    public string EditMode { get; set; } = "Form";

    [ProtoMember(7)]
    [Required, MaxLength(15)]
    public string MaxWidth { get; set; }

    [ProtoMember(8)]
    [MaxLength(15)]
    public string SearchText { get; set; } = "Search";

    [ProtoMember(9)]
    [MaxLength(15)]
    public string CreateText { get; set; } = "Create";

    [ProtoMember(9)]
    [MaxLength(15)]
    public string UpdateText { get; set; } = "Update";

    [ProtoMember(9)]
    [MaxLength(15)]
    public string DeleteText { get; set; } = "Delete";

    [ProtoMember(9)]
    [MaxLength(15)]
    public string CancelText { get; set; } = "Cancel";

    [ProtoMember(11)]
    public bool EnableSorting { get; set; }

   

    [ForeignKey(nameof(ComponentModels.PAGES_D.PD_M_REFNO))]
    public ICollection<PAGES_D> PAGES_D { get; set; } = new HashSet<PAGES_D>();
}

[ProtoContract]
public class PAGE_FIELDS
{
    [ProtoMember(1)]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PF_ROWID { get; set; }

    [ProtoMember(2)]
    public string PF_BINDINGFIELD { get; set; }

    [ProtoMember(2)]
    public string PF_COMPONENT_TYPE { get; set; }

 

}
[ProtoContract]
[ProtoInclude(300, nameof(ComponentModels.HEADER_BUTTONS))]
[Table(name: nameof(PAGES_D))]
public class PAGES_D:PAGES_M
{
    public int PD_M_REFNO { get; set; }
}

[ProtoContract]
public class HEADER_BUTTONS: PAGES_M
{

}

[ProtoContract]
[ProtoInclude(100,nameof(ComponentModels.PAGES_M))]
[Table(name:nameof(COMPONENTS_BASE))]
public class COMPONENTS_BASE
{
    [ProtoMember(1)]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CB_ROWID { get; set; }

    [ProtoMember(2)]
    [Required,MaxLength(30)]
    public string CB_TITLE { get; set; }

    [ProtoMember(3)]
    [Required, MaxLength(200)]
    public string CB_DESCRIPTION { get; set; }

    [ProtoMember(4)]
    [Required,MaxLength(30)]
    public string CB_DATABASE { get; set; }

    [ProtoMember(5)]
    public string CB_COMMAND_TYPE { get; set; }

    [ForeignKey(nameof(ComponentModels.PERMISSIONS.PER_COMPONENT_REFNO))]
    public List<PERMISSIONS> PERMISSIONS { get; set; } 
}


