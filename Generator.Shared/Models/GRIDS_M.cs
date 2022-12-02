using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models;

public class IDENTIFIERS
{
    [Key]
    [ProtoMember(1)]
    public string DatabaseIdentifier { get; set; }

    [ProtoMember(2)]
    [MinLength(30)]
    [MaxLength(40)]
    public string ConnectionString { get; set; }
}

[ProtoContract]
public class GRIDS_M
{
    [ProtoMember(1)]
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UI_ROWID { get; set; }

    /// <summary>
    /// Database Connections
    /// </summary>
    [ProtoMember(1)]
    public string UI_IDENTIFIER { get; set; }


    [ForeignKey(nameof(Models.GRIDS_D.GD_M_REFNO))]
    public ICollection<GRIDS_D> GRIDS_D { get; set; } = new HashSet<GRIDS_D>();

    public ICollection<HEADER_BUTTONS> HEADER_BUTTONS { get; set; } = new HashSet<HEADER_BUTTONS>();

}

[ProtoContract]
public class HEADER_BUTTONS
{
    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HB_ROWID { get; set; }

    [ProtoMember(2)]
    public int HB_GRID_REFNI { get; set; }
}


[ProtoContract]
public class FOOTER_BUTTONS
{
    [ProtoMember(1)]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HB_ROWID { get; set; }

    [ProtoMember(2)]
    public int HB_GRID_REFNI { get; set; }
}
