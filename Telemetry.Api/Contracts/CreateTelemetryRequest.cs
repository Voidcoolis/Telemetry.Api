using System.ComponentModel.DataAnnotations;

namespace Telemetry.Api.Contracts;

public sealed class CreateTelemetryRequest
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string DeviceId { get; set; } = default!;

    // Accept realistic temperatures; adjust if you want
    [Range(-100, 200)]
    public double Temperature { get; set; }

    // CPU usage should be 0..1 (or switch to 0..100 if you prefer percent)
    [Range(0, 1)]
    public double CpuUsage { get; set; }
}
