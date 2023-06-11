﻿using System.Text;
using Generator.Server;
using Generator.Server.Database;
using Generator.Server.Extensions;
using Generator.Server.FIlters;
using Generator.Server.Services.Authentication;
using Grpc.Net.Client;
using LitJWT;
using LitJWT.Algorithms;
using MagicOnion;
using MagicOnion.Server;
using MagicOnion.Server.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
 
builder.Services.AddGrpc();

 
builder.Services.AddMagicOnion(x => x.IsReturnExceptionStackTraceInErrorDetail = true );

builder.Services.AddDbContext<TestContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnection("DefaultConnection")));

builder.Services.AddDbContext<GeneratorContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnection("GeneratorConnection"),
    b => b.MigrationsAssembly("Generator.Server")));

builder.Services.AddDbContext<MemoryContext>(options =>
     options.UseInMemoryDatabase(nameof(MemoryContext)));
builder.Services.RegisterGenServer();

//builder.Services.AddSingleton<IMagicOnionFilterFactory<MagicAuthAttribute>, MagicAuthMiddleware>();

builder.Services.AddSingleton<FastJwtTokenService>(x =>
{
    var key = HS256Algorithm.GenerateRandomRecommendedKey();

    var encoder = new JwtEncoder(new HS256Algorithm(key));
    var decoder = new JwtDecoder(encoder.SignAlgorithm);

    return new FastJwtTokenService
    {
        Encoder = encoder,
        Decoder = decoder
    };
});

  

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseRouting();

app.MapMagicOnionHttpGateway("_", app.Services.GetService<MagicOnion.Server.MagicOnionServiceDefinition>().MethodHandlers, GrpcChannel.ForAddress("http://localhost:5002")); // Use HTTP instead of HTTPS
app.MapMagicOnionSwagger("swagger", app.Services.GetService<MagicOnion.Server.MagicOnionServiceDefinition>().MethodHandlers, "/_/");

app.MapMagicOnionService();



// Configure the HTTP request pipeline.
//app.MapMagicOnionService();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

