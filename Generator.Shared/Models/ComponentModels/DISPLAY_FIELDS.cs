using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract]
public class PAGES_BASE
{
    [ProtoMember(1)]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PB_ROWID { get; set; }

    [ProtoMember(2)]
    [Required,MaxLength(30)]
    public string PB_TITLE { get; set; }

    [ProtoMember(3)]
    [Required, MaxLength(200)]
    public string PB_DESCRIPTION { get; set; }

    [ProtoMember(4)]
    [Required,MaxLength(30)]
    public string PB_DATABASE { get; set; }

    [ProtoMember(5)]
    public string PB_COMMAND_TYPE { get; set; }

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

    [ForeignKey(nameof(ComponentModels.PERMISSIONS.PER_COMPONENT_REFNO))]
    public List<PERMISSIONS> PERMISSIONS { get; set; }
}

[ProtoContract]
public class DISPLAY_FIELDS
{
    [ProtoMember(1)]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DF_ROWID { get; set; }

    [ProtoMember(2)]
    public int DF_COMPONENT_REFNO { get; set; }

    [ProtoMember(3)]
    public string DF_FIELD_NAME { get; set; }

    [ProtoMember(4)]
    public string DF_COMMAND_TYPE { get; set; }

    [Required]
    [ProtoMember(5)]
    public string DF_TITLE { get; set; }

    [ProtoMember(6)]
    public string DF_DATABASE { get; set; }

    [ProtoMember(7)]
    public string DF_STORED_PROCEDURE { get; set; }

    [ProtoMember(8)]
    public string DF_TABLE { get; set; }

}


