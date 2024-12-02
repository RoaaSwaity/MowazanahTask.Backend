using Data;
using Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.Interfaces;
using Service.Repositories;
using Service.Services;

var builder = WebApplication.CreateBuilder(args);
IWebHostEnvironment env = builder.Environment;

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Data")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IStockService, StockService>();

//CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin() // Allow all origins (for development only)
            .AllowAnyMethod()
            .AllowAnyHeader());
});
//End CORS Policy

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Mowazaneh Task API", Version = "v1" });
});
var app = builder.Build();


app.UseCors("CorsPolicy");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeederManager.Initialize(services);
}

app.Run();
