using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Generator.Example.Data;
using System.Net;
using Grpc.Net.Client.Web;
using ProtoBuf.Grpc.ClientFactory;
using Grpc.Net.Client;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Generator.Shared.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

 

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddMudServices();

var httpHandler = new SocketsHttpHandler
{
    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
    EnableMultipleHttp2Connections = true
};

var hand = new GrpcWebHandler(GrpcWebMode.GrpcWeb, httpHandler);
//hand.HttpVersion = HttpVersion.Version11;



builder.Services.AddCodeFirstGrpcClient<IGenericService>(x =>
{
    x.ChannelOptionsActions.Add(x => new GrpcChannelOptions
    {
        HttpHandler = hand,

        MaxReceiveMessageSize = null, //30000000
        MaxSendMessageSize = null, //30000000
        Credentials = ChannelCredentials.Insecure,
        UnsafeUseInsecureChannelCallCredentials = true,
        ServiceConfig = new ServiceConfig { LoadBalancingConfigs = { new RoundRobinConfig() } }
    });

    x.Address = new Uri("http://localhost:5010");

})
.ConfigurePrimaryHttpMessageHandler(x => hand);

builder.Services.AddCodeFirstGrpcClient<ITestService>(x =>
{
    x.ChannelOptionsActions.Add(x => new GrpcChannelOptions
    {
        HttpHandler = hand,

        MaxReceiveMessageSize = null, //30000000
        MaxSendMessageSize = null, //30000000
        Credentials = ChannelCredentials.Insecure,
        UnsafeUseInsecureChannelCallCredentials = true,
        ServiceConfig = new ServiceConfig { LoadBalancingConfigs = { new RoundRobinConfig() } }
    });

    x.Address = new Uri("http://localhost:5010");

})
.ConfigurePrimaryHttpMessageHandler(x => hand);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

