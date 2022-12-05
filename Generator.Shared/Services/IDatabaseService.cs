using Generator.Shared.Models;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Shared.Services;

[Service]
public interface IDatabaseService : IGenericServiceBase<DATABASE>{}
