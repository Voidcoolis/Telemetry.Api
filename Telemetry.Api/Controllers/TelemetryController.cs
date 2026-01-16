using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telemetry.Api.Contracts;
using Telemetry.Api.Data;
using Telemetry.Api.Models;

namespace Telemetry.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TelemetryController : ControllerBase
{
    private readonly TelemetryDbContext _db;

    public TelemetryController(TelemetryDbContext db) => _db = db;

    [HttpPost]
    public async Task<ActionResult<TelemetryEvent>> Create([FromBody] CreateTelemetryRequest request)
    {
        // Model validation is automatic because of [ApiController]
        var entity = new TelemetryEvent
        {
            Id = Guid.NewGuid(),
            DeviceId = request.DeviceId,
            Temperature = request.Temperature,
            CpuUsage = request.CpuUsage,
            TimestampUtc = DateTime.UtcNow
        };

        _db.TelemetryEvents.Add(entity);
        await _db.SaveChangesAsync();

        return Ok(entity);
    }

    [HttpGet]
    public async Task<ActionResult<List<TelemetryEvent>>> GetAll()
        => await _db.TelemetryEvents
            .OrderByDescending(x => x.TimestampUtc)
            .ToListAsync();

    [HttpGet("device/{deviceId}")]
    public async Task<ActionResult<List<TelemetryEvent>>> GetByDevice(string deviceId)
        => await _db.TelemetryEvents
            .Where(x => x.DeviceId == deviceId)
            .OrderByDescending(x => x.TimestampUtc)
            .ToListAsync();
}
