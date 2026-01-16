using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telemetry.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTelemetryIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEvents_DeviceId",
                table: "TelemetryEvents",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEvents_DeviceId_TimestampUtc",
                table: "TelemetryEvents",
                columns: new[] { "DeviceId", "TimestampUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_TelemetryEvents_TimestampUtc",
                table: "TelemetryEvents",
                column: "TimestampUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TelemetryEvents_DeviceId",
                table: "TelemetryEvents");

            migrationBuilder.DropIndex(
                name: "IX_TelemetryEvents_DeviceId_TimestampUtc",
                table: "TelemetryEvents");

            migrationBuilder.DropIndex(
                name: "IX_TelemetryEvents_TimestampUtc",
                table: "TelemetryEvents");
        }
    }
}
