using Telemetry.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connStr = builder.Configuration.GetConnectionString("Postgres");
if (string.IsNullOrWhiteSpace(connStr))
    throw new InvalidOperationException("Connection string 'Postgres' is not configured.");

builder.Services.AddDbContext<TelemetryDbContext>(options =>
    options.UseNpgsql(connStr));

builder.Services.AddHealthChecks()
    .AddNpgSql(connStr, name: "postgres");

var app = builder.Build();

// Swagger only in Development (Docker is Development because of docker-compose env var)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // In production you usually want HTTPS
    app.UseHttpsRedirection();
}

app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
