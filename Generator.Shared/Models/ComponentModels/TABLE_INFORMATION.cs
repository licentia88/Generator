using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels
{
    [NotMapped]
    [ProtoContract]
    public class TABLE_INFORMATION
    {
        [ProtoMember(1)]
        public string TI_TABLE_NAME { get; set; }
    }
}

