using System.ComponentModel.DataAnnotations.Schema;
using Generator.Equals;
using Generator.Shared.Models.ComponentModels.Abstracts;
using MemoryPack;
using MessagePack;
using Microsoft.EntityFrameworkCore;

namespace Generator.Shared.Models.ComponentModels;

[Equatable]
[MemoryPackable]
[Table(nameof(GRID_VIEW))]
[Index(nameof(VB_GRID_REFNO),IsUnique =true)] //one per page allowed
public partial class GRID_VIEW : VIEW_BASE
{
    public GRID_VIEW()
    {
        VB_TYPE = nameof(GRID_VIEW);
        //VBM_TITLE = "CRUD";
    }

    //public bool CV_CREATE { get; set; }

    //public bool CV_UPDATE { get; set; }

    //public bool CV_DELETE { get; set; }

}