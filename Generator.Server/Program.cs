using Generator.Server.Database;
using Generator.Server.Extensions;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(opt =>
//{
//    opt.ListenLocalhost(5002, o =>
//    {
//        o.Protocols = HttpProtocols.Http2;
//        //o.UseHttps(certificate);
//    });

//    opt.ListenLocalhost(5001, o =>
//    {
//          o.Protocols = HttpProtocols.Http1;
//    });

//});

 
builder.Services.AddGrpc();
builder.Services.AddMagicOnion(x => x.IsReturnExceptionStackTraceInErrorDetail = true);

builder.Services.AddDbContext<TestContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnection("DefaultConnection")));

builder.Services.AddDbContext<GeneratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnection("GeneratorConnection"),
    b => b.MigrationsAssembly("Generator.Server")));

builder.Services.RegisterGenServer();

var app = builder.Build();

//app.UseRouting();


//app.MapMagicOnionHttpGateway("_", app.Services.GetService<MagicOnion.Server.MagicOnionServiceDefinition>().MethodHandlers, GrpcChannel.ForAddress("http://localhost:5002")); // Use HTTP instead of HTTPS
//app.MapMagicOnionSwagger("swagger", app.Services.GetService<MagicOnion.Server.MagicOnionServiceDefinition>().MethodHandlers, "/_/");

app.MapMagicOnionService();



// Configure the HTTP request pipeline.
//app.MapMagicOnionService();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

