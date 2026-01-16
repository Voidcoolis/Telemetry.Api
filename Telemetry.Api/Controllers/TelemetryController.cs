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

    // GET /api/Telemetry?deviceId=...&skip=0&take=100
    [HttpGet]
    public async Task<ActionResult<List<TelemetryEvent>>> Get(
        [FromQuery] string? deviceId,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 100)
    {
        // Guardrails
        if (skip < 0) skip = 0;
        if (take <= 0) take = 100;
        if (take > 500) take = 500; // prevent huge responses

        var query = _db.TelemetryEvents.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(deviceId))
            query = query.Where(x => x.DeviceId == deviceId);

        var items = await query
            .OrderByDescending(x => x.TimestampUtc)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return Ok(items);
    }

    // GET /api/Telemetry/device/{deviceId}?skip=0&take=100
    [HttpGet("device/{deviceId}")]
    public async Task<ActionResult<List<TelemetryEvent>>> GetByDevice(
        string deviceId,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 100)
        => await Get(deviceId, skip, take);

}
