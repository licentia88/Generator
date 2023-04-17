using MudBlazor.Services;
using Generator.Shared.Services;
using Generator.Shared.Extensions;
using Generator.Client;
using System.Net;
using Generator.Components.Extensions;
using Generator.UI.Models;
using System.Collections.ObjectModel;
using Generator.UI.Pages;
using MudBlazor;

var builder = WebApplication.CreateBuilder(args);

CryptoService.HashKey = builder.Configuration.GetSection("HashKey").Value;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.RegisterGeneratorComponents()
    ;
builder.Services.AddScoped<NotificationsView>();
builder.Services.AddScoped<List<NotificationVM>>();


//builder.Services.RegisterGrpcService<IDisplayFieldsService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
//builder.Services.RegisterGrpcService<IPagesMService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
//builder.Services.RegisterGrpcService<IPagesDService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
builder.Services.RegisterGrpcService<IDatabaseService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);


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

