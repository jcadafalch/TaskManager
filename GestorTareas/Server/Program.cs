using GestorTareas.Server.Controllers;
using GestorTareas.Server.Interfaces;
using GestorTareas.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("BloggingDatabase");
builder.Services.AddDbContext<GestorTareasDbContext>(options =>
{
    options.UseSqlServer(connectionString);

    if(builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
    }
});

//builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRazorPages();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ITareaCacheService, TareaCacheService>();
builder.Services.AddSingleton<IEtiquetaCacheService, EtiquetaCacheService>();

var app = builder.Build();

// Si estamos ejecutando en modo desarrollo permitimos el uso de Swagger
if (builder.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:5130")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowCredentials()
);

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
