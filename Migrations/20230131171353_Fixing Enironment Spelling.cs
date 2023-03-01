using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChemStoreWebApp.Migrations
{
    public partial class FixingEnironmentSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "Enviornmental");

            migrationBuilder.InsertData(
                table: "Hazard",
                columns: new[] { "HazardId", "Description" },
                values: new object[] { "Environment", "Enviornmental Hazard" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hazard",
                keyColumn: "HazardId",
                keyValue: "Environment");

            migrationBuilder.InsertData(
                table: "Hazard",
                columns: new[] { "HazardId", "Description" },
                values: new object[] { "Enviornmental", "Enviornmental Hazard" });
        }
    }
}
