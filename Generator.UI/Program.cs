using MudBlazor.Services;
using Grpc.Net.Client.Web;
using System.Net;
using ProtoBuf.Grpc.ClientFactory;
using Generator.Shared.Services;
using Grpc.Net.Client;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Generator.UI;
using Generator.Shared.Extensions;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

CryptoService.HashKey = builder.Configuration.GetSection("HashKey").Value;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
 
builder.Services.AddScoped(typeof(List<>));
builder.Services.RegisterGrpcService<IDatabaseService>();


 

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

