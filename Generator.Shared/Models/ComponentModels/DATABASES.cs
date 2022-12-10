using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using Generator.Shared.Services;
using ProtoBuf;

namespace Generator.Shared.Models.ComponentModels;

[ProtoContract]
public class DATABASES :IValidatableObject
{
   
    [Key]
    [ProtoMember(1)]
    public string DatabaseIdentifier { get; set; }

    [NotMapped]
    [ProtoMember(2)]
    public string _ConnectionString;

    [ProtoMember(3)]
    public string ConnectionString {
        get => CryptoService.Decrypt(_ConnectionString);
        set => _ConnectionString= CryptoService.Encrypt(value);
    }

    public string DecryptedConnectionString()
    {
        return CryptoService.Decrypt(ConnectionString);
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
         var builder = new DbConnectionStringBuilder();


        return default;
    }
}
