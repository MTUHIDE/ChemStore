using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChemStoreWebApp.Migrations
{
    public partial class SeededHazards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hazard",
                columns: new[] { "HazardId", "Description" },
                values: new object[,]
                {
                    { "Corrosion", "Corrosive" },
                    { "Enviornmental", "Enviornmental Hazard" },
                    { "ExclamationMark", "Exclamation Mark" },
                    { "ExplodingBomb", "Exploding Bomb" },
                    { "Flame", "Flame" },
                    { "FlameOverCircle", "Flame Over Circle" },
                    { "GasCylinder", "Gas Cylinder" },
                    { "HealthHazard", "HealthHazard" },
                    { "Skull", "Skull" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "Corrosion");

            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "Enviornmental");

            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "ExclamationMark");

            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "ExplodingBomb");

            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "Flame");

            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "FlameOverCircle");

            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "GasCylinder");

            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "HealthHazard");

            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "Skull");
        }
    }
}
