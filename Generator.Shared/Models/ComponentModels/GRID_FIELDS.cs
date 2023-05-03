using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels
{
    [ProtoContract]
	public class GRID_FIELDS
	{
		[ProtoMember(1)]
		[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GF_ROWID { get; set; }

		[ProtoMember(2)]
		public int GF_VIEW_REFNO { get; set; }

		[ProtoMember(3)]
		public string GF_CONTROL_TYPE { get; set; }

        [ProtoMember(4)]
        public string GF_BINDINGFIELD { get; set; }

        [ProtoMember(5)]
        public string GF_LABEL { get; set; }


        [ProtoMember(6)]
        [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
        public int? GF_XS { get; set; }  

        [ProtoMember(7)]
        [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
        public int? GF_SM { get; set; } 

        [ProtoMember(8)]
        [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
        public int? GF_MD { get; set; }  

        [ProtoMember(9)]
        [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
        public int? GF_LG { get; set; }  

        [ProtoMember(10)]
        [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
        public int? GF_XLG { get; set; }  

        [ProtoMember(11)]
        [Range(1, 12, ErrorMessage = "Value must be between 1-12")]
        public int? GF_XXLG { get; set; }  

        [ProtoMember(12)]
        public bool GF_EDITOR_VISIBLE { get; set; }

        [ProtoMember(13)]
        public bool GF_EDITOR_ENABLED { get; set; }

        [ProtoMember(14)]
        public bool GF_GRID_VISIBLE { get; set; }

        [ProtoMember(15)]
        public bool GF_USE_AS_SEARCHFIELD { get; set; }

        [ProtoMember(16)]
        public bool GF_REQUIRED { get; set; }

        [ProtoMember(17)]
        public string GF_DATABASE { get; set; }

        [ProtoMember(18)]
        public string GF_SOURCE { get; set; }

        //Combo
        [ProtoMember(19)]
        public string GF_VALUEFIELD { get; set; }

        [ProtoMember(20)]
        public string GF_DISPLAYFIELD { get; set; }

        [ProtoMember(21)]
        public string GF_DATASOURCE { get; set; }


        //CheckBox
        [ProtoMember(22)]
        public string GF_TRUE_TEXT { get; set; }

        [ProtoMember(23)]
        public string GF_FALSE_TEXT { get; set; }

        //DATEPICKER
        [ProtoMember(24)]
        public string GF_FORMAT { get; set; }

        [ProtoMember(25)]
        public int GF_INPUT_TYPE { get; set; }

    }
}

