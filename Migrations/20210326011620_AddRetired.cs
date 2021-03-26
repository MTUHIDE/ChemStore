using Microsoft.EntityFrameworkCore.Migrations;

namespace ChemStoreWebApp.Migrations
{
    public partial class AddRetired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Retired",
                table: "container",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Retired",
                table: "container");
        }
    }
}
