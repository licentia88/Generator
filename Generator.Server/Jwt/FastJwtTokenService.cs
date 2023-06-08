using Grpc.Core;
using LitJWT;
using MagicOnion;
using MagicOnion.Server;

namespace Generator.Server.Jwt;

public class FastJwtTokenService
{
    public  JwtEncoder Encoder { get; set; }

    public  JwtDecoder Decoder { get; set; }

    public   byte[] CreateToken(int userId, params int[] roles) //where TModel :class
    {
        var token = Encoder.EncodeAsUtf8Bytes((userId,roles),
            TimeSpan.FromMinutes(1),
            (x, writer) => writer.Write(Utf8Json.JsonSerializer.SerializeUnsafe(x)));

        return token;
    }

    internal bool DecodeToken(byte[] token, int[] roles, ServiceContext context)
    {
        var result = Decoder.TryDecode(token, x => Utf8Json.JsonSerializer.Deserialize<(int userId,List<int> Roles)>(x.ToArray()), out var TokenResult);

        if (result != DecodeResult.Success)
            throw new ReturnStatusException(StatusCode.Cancelled, result.ToString());

        //var rolesMatch = !roles.Any() || TokenResult.Roles.Any(x => roles.Contains(x));

        //if (!rolesMatch) throw new ReturnStatusException(StatusCode.Unauthenticated, "Bu İşlemi Yapmaya yetkiniz yok.");

        //context.Items[nameof(MyToken)] = TokenResult;

        return true;
    }
}