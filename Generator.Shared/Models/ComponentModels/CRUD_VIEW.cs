using Generator.Shared.Models.ComponentModels.Abstracts;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[MessagePackObject]
// ReSharper disable once InconsistentNaming
public class CRUD_VIEW : VIEW_BASE_M
{
    public CRUD_VIEW()
    {
        VBM_TYPE = nameof(CRUD_VIEW);
        VBM_TITLE = "CRUD";
    }

    [Key(6)]
    public bool CV_CREATE { get; set; }

    [Key(7)]
    public bool CV_UPDATE { get; set; }

    [Key(8)]
    public bool CV_DELETE { get; set; }

}