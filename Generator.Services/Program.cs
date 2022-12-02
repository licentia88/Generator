using System.IO.Compression;
using Generator.Service.Services;
using Generator.Services;
using Generator.Shared.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProtoBuf.Grpc.Configuration;
using ProtoBuf.Grpc.Server;
using ProtoBuf.Meta;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(opt =>
{
    opt.ListenLocalhost(5010, o => o.Protocols = HttpProtocols.Http2);
});

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<TestContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddCodeFirstGrpc(config =>
{
    config.ResponseCompressionLevel = CompressionLevel.Optimal;
    config.EnableDetailedErrors = true;
    config.MaxSendMessageSize = null; //30000000
    config.MaxReceiveMessageSize = null;//30000000
}
);
var app = builder.Build();


app.UseGrpcWeb();

// Configure the HTTP request pipeline.

app.MapGrpcService<GenericService>().EnableGrpcWeb();
app.MapGrpcService<TestService>().EnableGrpcWeb();

// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

