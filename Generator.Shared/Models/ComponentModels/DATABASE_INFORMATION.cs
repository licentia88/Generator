using System.ComponentModel.DataAnnotations.Schema;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels
{
    [NotMapped]
    [ProtoContract]
    public class DATABASE_INFORMATION
    {
        [ProtoMember(1)]
        public string DI_DATABASE_NAME { get; set; }
    }

    [NotMapped]
    [ProtoContract]
    public class DISPLAY_FIELD_INFORMATION
    {
        [ProtoMember(1)]
        public string DFI_NAME { get; set; }
    }

    [NotMapped]
    public class COMMAND_TYPES
    {
        public int CT_ROWID { get; set; }

        public string CT_DESC { get; set; }
    }

    [NotMapped]
    [ProtoContract]
    public class STORED_PROCEDURES
    {
        [ProtoMember(1)]
        public string SP_NAME { get; set; }
    }
}

