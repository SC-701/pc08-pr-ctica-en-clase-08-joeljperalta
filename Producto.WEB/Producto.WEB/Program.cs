using Reglas;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

builder.Services.AddScoped<ProductoReglas>();

var app = builder.Build();

app.UseStaticFiles();

app.MapRazorPages();

app.Run();