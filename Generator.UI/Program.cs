﻿using MudBlazor.Services;
using Generator.Shared.Services;
//using Generator.Client;
using Generator.Components.Extensions;
using Generator.UI.Models;
using Generator.UI.Pages;
using Generator.Client.Extensions;
using Generator.Client.Services;
using Generator.UI.Extensions;
using Generator.Shared.Models.ComponentModels;
using Generator.UI.Startup;
using MessagePipe;

var builder = WebApplication.CreateBuilder(args);

//builder.Services
//    .AddAuth0WebAppAuthentication(options => {
//        options.Domain = builder.Configuration["Auth0:Domain"];
//        options.ClientId = builder.Configuration["Auth0:ClientId"];
//    });


CryptoService.HashKey = builder.Configuration.GetSection("HashKey").Value;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.RegisterGeneratorComponents();
builder.Services.AddScoped<NotificationsView>();
builder.Services.AddScoped<List<NotificationVM>>();
builder.Services.AddMagicServices();
builder.Services.AddMagicHubs();
builder.Services.RegisterStaticData();
builder.Services.AddSingleton<HubInitializer>();
builder.Services.AddMessagePipe();
//builder.Services.RegisterGrpcService<IGRidCrudViewService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
//builder.Services.RegisterGrpcService<IGridMService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
//builder.Services.RegisterGrpcService<IGridFieldsService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);
//builder.Services.RegisterGrpcService<IDatabaseService>("https://localhost:7178", "/Users/asimgunduz/server.crt", HttpVersion.Version11);


var app = builder.Build();

await Task.Delay(5000);

var hubService = app.Services.GetRequiredService<HubInitializer>();

await hubService.ConnectAsync();
await hubService.ReadAsync();

//await app.Services.FillAsync<GRID_M, GridMService, IGridMService>();
await app.Services.FillAsync<GRID_D, IGridDService>();

//await app.Services.FillAsync<PERMISSIONS, PermissionsService, IPermissionsService>();
await app.Services.FillAsync<ROLES, IRolesService>();


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

