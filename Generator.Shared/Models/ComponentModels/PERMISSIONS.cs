using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels
{
    [ProtoContract]
    [Index(nameof(PER_COMPONENT_REFNO),nameof(PER_AUTH_CODE), IsUnique =true)]
    public class PERMISSIONS
    {
        public PERMISSIONS()
        {

        }

        public PERMISSIONS(string ComponentType, string Description, string AuthCode)
        {
            PER_COMP_TYPE = ComponentType;
            PER_COMP_AUTH_CODE = AuthCode;
            PER_DESCRIPTION = Description;
            PER_AUTH_CODE = Description + PER_COMP_AUTH_CODE;
        }

        /// <summary>
        /// PrimaryKey
        /// </summary>
        [ProtoMember(1)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PER_ROWID { get; set; }


        /// <summary>
        /// Component RefNo
        /// </summary>
        [ProtoMember(2)]
        public int PER_COMPONENT_REFNO { get; set; }


        /// <summary>
        /// Component Type
        /// </summary>
        ///
        [Required]
        [MaxLength(20)]
        [ProtoMember(3)]
        public string PER_COMP_TYPE { get; set; }


        /// <summary>
        /// Parent Component Auth Code
        /// </summary>
        [ProtoMember(4)]
        public string PER_COMP_AUTH_CODE { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [ProtoMember(5)]
        public string PER_DESCRIPTION { get; set; }

        /// <summary>
        /// Permission AuthCode (System generated)
        /// </summary>
        [ProtoMember(6)]
        public string PER_AUTH_CODE { get; set; }

        public void ChangeAuthCode(string AuthCode)
        {
            PER_COMP_AUTH_CODE = AuthCode;
            PER_AUTH_CODE = PER_COMP_AUTH_CODE;
        }
	}
}

