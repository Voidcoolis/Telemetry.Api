using Telemetry.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Read PostgreSQL connection string from configuration (appsettings / environment variables)
var connStr = builder.Configuration.GetConnectionString("Postgres");
// Fail fast if database configuration is missing
if (string.IsNullOrWhiteSpace(connStr))
    throw new InvalidOperationException("Connection string 'Postgres' is not configured.");

builder.Services.AddDbContext<TelemetryDbContext>(options =>
    options.UseNpgsql(connStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
