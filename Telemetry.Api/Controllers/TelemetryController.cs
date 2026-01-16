using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telemetry.Api.Data;
using Telemetry.Api.Models;

namespace Telemetry.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TelemetryController : ControllerBase
{
    private readonly TelemetryDbContext _db;

    public TelemetryController(TelemetryDbContext db)
    {
        _db = db;
    }

    // POST: api/telemetry
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TelemetryEvent telemetry)
    {
        telemetry.TimestampUtc = DateTime.UtcNow;

        _db.TelemetryEvents.Add(telemetry);
        await _db.SaveChangesAsync();

        return Ok(telemetry);
    }

    // GET: api/telemetry
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _db.TelemetryEvents
            .OrderByDescending(x => x.TimestampUtc)
            .Take(100)
            .ToListAsync();

        return Ok(data);
    }

    // GET: api/telemetry/device/{deviceId}
    [HttpGet("device/{deviceId}")]
    public async Task<IActionResult> GetByDevice(string deviceId)
    {
        var data = await _db.TelemetryEvents
            .Where(x => x.DeviceId == deviceId)
            .OrderByDescending(x => x.TimestampUtc)
            .ToListAsync();

        return Ok(data);
    }
}
