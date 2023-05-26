//using System.Collections.Generic;
//using MessagePack;

//namespace Generator.Shared.Models.ComponentModels;

//[MessagePackObject]
////[Union(0, typeof(PAGES_D))]
//[Shema.Table(nameof(PAGES_M))]
//public class PAGES_M : COMPONENTS_BASE
//{
//    public PAGES_M()
//    {
//        //CB_TYPE = nameof(PAGES_M);
//    }

//    [Key(8)]
//    public int GB_EDIT_MODE { get; set; }

//    [Key(9)]
//    public int GB_EDIT_TRIGGER { get; set; }

//    [Key(10)]
//    public int GB_MAX_WIDTH { get; set; }

//    [Key(11)]
//    public bool GB_DENSE { get; set; }

//    [Key(12)]
//    [Annotation.Required(ErrorMessage = "*")]
//    public int? GB_ROWS_PER_PAGE { get; set; }

//    [Key(13)]
//    public bool GB_ENABLE_SORTING { get; set; }

//    [Key(14)]
//    public bool GB_ENABLE_FILTERING { get; set; }

//    [Key(15)]
//    public bool GB_STRIPED { get; set; }

//    [Key(16)]
//    [Shema.ForeignKey(nameof(ComponentModels.VIEW_BASE.VB_PAGE_REFNO))]
//    public ICollection<VIEW_BASE> VIEW_BASE { get; set; }

//    [Key(17)]
//    [Shema.ForeignKey(nameof(ComponentModels.PAGES_D.PD_M_REFNO))]
//    public ICollection<PAGES_D> PAGES_D { get; set; }
//}
