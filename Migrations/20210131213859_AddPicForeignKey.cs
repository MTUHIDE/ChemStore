using Microsoft.EntityFrameworkCore.Migrations;

namespace ChemStoreWebApp.Migrations
{
    public partial class AddPicForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PicId",
                table: "container",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PicId",
                table: "container");
        }
    }
}
