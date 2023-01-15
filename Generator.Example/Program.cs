using Generator.Shared.Services;
using MudBlazor.Services;
using Generator.Shared.Extensions;
using Generator.Examples.Shared;

var builder = WebApplication.CreateBuilder(args);

CryptoService.HashKey = builder.Configuration.GetSection("HashKey").Value;


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

//builder.Services.RegisterGrpcService<IGenericServiceBase>();
//builder.Services.RegisterGrpcService<ITestService>();
//builder.Services.RegisterGrpcService<IDatabaseService>();
//builder.Services.RegisterGrpcService<IHeaderButtonService>();
//builder.Services.RegisterGrpcService<IFooterButtonService>();
//builder.Services.RegisterGrpcService<IGridsMService>();
//builder.Services.RegisterGrpcService<IGridsDService>();
//builder.Services.RegisterGrpcService<IUserService>();
//builder.Services.RegisterGrpcService<IOrdersMService>();
//builder.Services.RegisterGrpcService<IOrdersDService>();

builder.Services.RegisterGrpcServiceWithSsl2<ITestService>("https://localhost:7178");
builder.Services.RegisterGrpcServiceWithSsl2<IDatabaseService>("https://localhost:7178");
builder.Services.RegisterGrpcServiceWithSsl2<IHeaderButtonService>("https://localhost:7178");
builder.Services.RegisterGrpcServiceWithSsl2<IFooterButtonService>("https://localhost:7178");
builder.Services.RegisterGrpcServiceWithSsl2<IGridsMService>("https://localhost:7178");
builder.Services.RegisterGrpcServiceWithSsl2<IGridsDService>("https://localhost:7178");
builder.Services.RegisterGrpcServiceWithSsl2<IUserService>("https://localhost:7178");
builder.Services.RegisterGrpcServiceWithSsl2<IOrdersMService>("https://localhost:7178");
builder.Services.RegisterGrpcServiceWithSsl2<IOrdersDService>("https://localhost:7178");



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

