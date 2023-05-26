
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
public class GRID_FIELDS
{
    [Key(0)]
    [Annotation.Key, Shema.DatabaseGenerated(Shema.DatabaseGeneratedOption.Identity)]
    public int GF_ROWID { get; set; }

    [Key(1)]
    public int GF_VIEW_REFNO { get; set; }

    [Key(2)]
    public string GF_CONTROL_TYPE { get; set; }

    [Key(3)]
    public string GF_BINDINGFIELD { get; set; }

    [Key(4)]
    public string GF_LABEL { get; set; }

    [Key(5)]
    [Annotation.Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_XS { get; set; }

    [Key(6)]
    [Annotation.Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_SM { get; set; }

    [Key(7)]
    [Annotation.Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_MD { get; set; }

    [Key(8)]
    [Annotation.Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_LG { get; set; }

    [Key(9)]
    [Annotation.Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_XLG { get; set; }

    [Key(10)]
    [Annotation.Range(1, 12, ErrorMessage = "Value must be between 1-12")]
    public int? GF_XXLG { get; set; }

    [Key(11)]
    public bool GF_EDITOR_VISIBLE { get; set; }

    [Key(12)]
    public bool GF_EDITOR_ENABLED { get; set; }

    [Key(13)]
    public bool GF_GRID_VISIBLE { get; set; }

    [Key(14)]
    public bool GF_USE_AS_SEARCHFIELD { get; set; }

    [Key(15)]
    public bool GF_REQUIRED { get; set; }

    [Key(16)]
    public string GF_DATABASE { get; set; }

    [Key(17)]
    public string GF_SOURCE { get; set; }

    [Key(18)]
    public string GF_VALUEFIELD { get; set; }

    [Key(19)]
    public string GF_DISPLAYFIELD { get; set; }

    [Key(20)]
    public string GF_DATASOURCE { get; set; }

    [Key(21)]
    public string GF_DATASOURCE_QUERY { get; set; }

    [Key(22)]
    public string GF_TRUE_TEXT { get; set; }

    [Key(23)]
    public string GF_FALSE_TEXT { get; set; }

    [Key(24)]
    public string GF_FORMAT { get; set; }

    [Key(25)]
    public int GF_INPUT_TYPE { get; set; }
}