using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using ProtoBuf.Grpc.Configuration;

namespace Generator.Shared.Services;

[Service]
public interface IHeaderButtonService : IGenericServiceBase<HEADER_BUTTON> { }
