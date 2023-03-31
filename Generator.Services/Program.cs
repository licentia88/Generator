using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Generator.Server;
using Generator.Server.Dependency;
using Generator.Server.Extensions;
using Generator.Server.Seed;
using Generator.Services;
using Generator.Services.Services;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var certificate =
  X509Certificate2.CreateFromPemFile("/Users/asimgunduz/server.crt", Path.ChangeExtension("/Users/asimgunduz/server.crt", "key"));

var verf = certificate.Verify();
 

builder.WebHost.ConfigureKestrel(opt =>
{
    opt.ListenLocalhost(7178, o =>
    {
        o.Protocols = HttpProtocols.Http1;
        o.UseHttps(certificate);
    });

});

CryptoService.HashKey = builder.Configuration.GetSection("HashKey").Value;

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682




// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<TestContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnection("DefaultConnection")));

builder.Services.AddDbContext<GeneratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnection("GeneratorConnection"),
    b => b.MigrationsAssembly("Generator.Service")));

builder.Services.RegisterGenerator();
builder.Services.RegisterGenServer();

var app = builder.Build();


app.UseGrpcWeb();


await app.Services.CreateAsyncScope().ServiceProvider.GetService<SeedData>().FillComponentsAsync();
// Configure the HTTP request pipeline.

app.RegisterGeneratorServices();
app.MapGrpcService<TestService>().EnableGrpcWeb();
app.MapGrpcService<UserService>().EnableGrpcWeb();
app.MapGrpcService<OrdersMService>().EnableGrpcWeb();



app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

