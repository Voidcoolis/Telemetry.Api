using Microsoft.EntityFrameworkCore;
using Telemetry.Api.Models;

namespace Telemetry.Api.Data;

public sealed class TelemetryDbContext : DbContext
{
    public TelemetryDbContext(DbContextOptions<TelemetryDbContext> options)
        : base(options) { }

    public DbSet<TelemetryEvent> TelemetryEvents => Set<TelemetryEvent>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TelemetryEvent>()
            .HasIndex(x => x.DeviceId);

        modelBuilder.Entity<TelemetryEvent>()
            .HasIndex(x => x.TimestampUtc);

        // Optimizes: WHERE DeviceId = ... ORDER BY TimestampUtc DESC
        modelBuilder.Entity<TelemetryEvent>()
            .HasIndex(x => new { x.DeviceId, x.TimestampUtc });
    }
}
