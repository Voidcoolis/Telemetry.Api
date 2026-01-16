using Microsoft.EntityFrameworkCore;
using Telemetry.Api.Models;

namespace Telemetry.Api.Data;

public sealed class TelemetryDbContext : DbContext
{
    public TelemetryDbContext(DbContextOptions<TelemetryDbContext> options)
        : base(options) { }

    public DbSet<TelemetryEvent> TelemetryEvents => Set<TelemetryEvent>();
}
