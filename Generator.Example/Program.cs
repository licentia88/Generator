using Generator.Shared.Services;
using MudBlazor.Services;
using Generator.Examples.Shared;
using Generator.Components.Extensions;
using Generator.Client;
using System.Net;
using Generator.Examples.Shared.Models;
using Generator.Examples.Shared.Services;
using Generator.Client.Extensions;

var builder = WebApplication.CreateBuilder(args);

CryptoService.HashKey = builder.Configuration.GetSection("HashKey").Value;


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.RegisterGeneratorComponents();
builder.Services.AddSingleton<Lazy<List<USER>>>();
//builder.Services.AddSingleton<InjectionClass>();
 builder.Services.RegisterExampleServices();

 

// builder.Services.RegisterGrpcService<ITestService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
//builder.Services.RegisterGrpcService<IUserService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
//builder.Services.RegisterGrpcService<IOrdersMService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
//builder.Services.RegisterGrpcService<IOrdersDService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);



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

