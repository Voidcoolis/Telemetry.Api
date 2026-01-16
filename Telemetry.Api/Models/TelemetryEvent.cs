namespace Telemetry.Api.Models;

public sealed class TelemetryEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string DeviceId { get; set; } = default!;

    public double Temperature { get; set; }

    public double CpuUsage { get; set; }

    public DateTime TimestampUtc { get; set; }
}
