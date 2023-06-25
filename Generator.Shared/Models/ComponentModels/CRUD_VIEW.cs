using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;
using MessagePack;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[MemoryPackable]
[Table(nameof(CRUD_VIEW))]
public partial class CRUD_VIEW : VIEW_BASE_M
{
    public CRUD_VIEW()
    {
        VBM_TYPE = nameof(CRUD_VIEW);
        VBM_TITLE = "CRUD";
    }

    public bool CV_CREATE { get; set; }

    public bool CV_UPDATE { get; set; }

    public bool CV_DELETE { get; set; }

}