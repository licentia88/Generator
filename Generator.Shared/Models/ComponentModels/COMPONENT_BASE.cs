using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract]
[ProtoInclude(100, nameof(PAGES_M))]
[ProtoInclude(200, nameof(BUTTONS_BASE))]
[Table(nameof(COMPONENT_BASE))]
	public class COMPONENT_BASE
	{
    /// <summary>
    /// Primary Key
    /// </summary>
		[ProtoMember(1)]
		[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CB_ROWID { get; set; }

    /// <summary>
    /// Methodlar icin kod
    /// </summary>
    [Required]
    [ProtoMember(2)]
    public string CB_CODE { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    [Required]
    [ProtoMember(3)]
    public string CB_TITLE { get; set; }

    /// <summary>
    /// Control un turu
    /// </summary>
    [ProtoMember(3)]
    public string CB_COMPONENT_TYPE { get; set; }

    /// <summary>
    /// Aciklama alani
    /// </summary>
    [ProtoMember(4)]
    public string CB_DESCRIPTION { get; set; }


    /// <summary>
    ///  Command Turu Text or StoredProcedure
    /// </summary>
    [ProtoMember(5)]
    public int CB_COMMAND_TYPE { get; set; }

    /// <summary>
    /// DatabaseName
    /// </summary>
    [Required]
    [ProtoMember(6)]
    public string CB_DATABASE { get; set; }


    /// <summary>
    /// Query or StoredProcedure, dolu olan hangisiysa onu alacak
    /// </summary>
    [Required]
    [ProtoMember(7)]
    public string CB_SQL_COMMAND { get; set; }

    /// <summary>
    /// TableName
    /// </summary>
    [ProtoMember(8)]
    public string CB_TABLE { get; set; }


    /// <summary>
    /// Yaratilan Yetkiler
    /// </summary>
    [ProtoMember(9)]
    [ForeignKey(nameof(Models.ComponentModels.PERMISSIONS.PER_COMPONENT_REFNO))]
    public ICollection<PERMISSIONS> PERMISSIONS { get; set; } = new HashSet<PERMISSIONS>();

    [ProtoMember(10)]
    [ForeignKey(nameof(Models.ComponentModels.DISPLAY_FIELDS.DF_COMPONENT_REFNO))]
    public ICollection<DISPLAY_FIELDS> DISPLAY_FIELDS { get; set; } = new HashSet<DISPLAY_FIELDS>();
}
