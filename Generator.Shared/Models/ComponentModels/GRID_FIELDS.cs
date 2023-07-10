
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using MemoryPack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[MemoryPackable]
public partial class GRID_FIELDS
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GF_ROWID { get; set; }

    public int GF_COMPONENT_REFNO { get; set; }

    public string GF_CONTROL_TYPE { get; set; }

    public string GF_BINDINGFIELD { get; set; }

    public string GF_LABEL { get; set; }

    [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_XS { get; set; }

    [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_SM { get; set; }

    [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_MD { get; set; }

    [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_LG { get; set; }

    [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_XLG { get; set; }

    [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_XXLG { get; set; }

    public bool GF_EDITOR_VISIBLE { get; set; }

    public bool GF_EDITOR_ENABLED { get; set; }

    public bool GF_GRID_VISIBLE { get; set; }

    public bool GF_USE_AS_SEARCHFIELD { get; set; }

    public bool GF_REQUIRED { get; set; }

    public string GF_DATABASE { get; set; }

    public string GF_SOURCE { get; set; }

    public string GF_VALUEFIELD { get; set; }

    public string GF_DISPLAYFIELD { get; set; }

    public string GF_DATASOURCE { get; set; }

    public string GF_DATASOURCE_QUERY { get; set; }

    public string GF_TRUE_TEXT { get; set; }

    public string GF_FALSE_TEXT { get; set; }

    public string GF_FORMAT { get; set; }

    public int GF_INPUT_TYPE { get; set; }
}