using Generator.Shared.Services;
using MudBlazor.Services;
using Generator.Shared.Extensions;
using Generator.Examples.Shared;
using Generator.Components.Extensions;
using System;
using System.Collections.Generic;
using Generator.Example.InjectionTest;
using Generator.Client;
using System.Net;
using Generator.Examples.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

CryptoService.HashKey = builder.Configuration.GetSection("HashKey").Value;


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.RegisterGeneratorComponents();
builder.Services.AddSingleton<Lazy<List<USER>>>();
builder.Services.AddSingleton<InjectionClass>();


 

builder.Services.RegisterGrpcService<ITestService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
builder.Services.RegisterGrpcService<IUserService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
builder.Services.RegisterGrpcService<IOrdersMService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
builder.Services.RegisterGrpcService<IOrdersDService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);



var app = builder.Build();

var serv = app.Services.GetService<InjectionClass>();
serv.FillTable();
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

