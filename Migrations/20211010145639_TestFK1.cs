using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ChemStoreWebApp.Migrations
{
    public partial class TestFK1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "container_ibfk_3",
                table: "container");

            migrationBuilder.AlterColumn<int>(
                name: "Account_ID",
                table: "account",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Supervisor_ID",
                table: "container",
                nullable: false,
                comment: "FK(Supervisor.ID)",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "FK(Supervisor.ID)");

            migrationBuilder.AddForeignKey(
                name: "container_ibfk_3",
                table: "container",
                column: "Supervisor_ID",
                principalTable: "account",
                principalColumn: "Account_ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "container_ibfk_3",
                table: "container");

            migrationBuilder.AlterColumn<long>(
                name: "Account_ID",
                table: "account",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<long>(
                name: "Supervisor_ID",
                table: "container",
                type: "bigint",
                nullable: false,
                comment: "FK(Supervisor.ID)",
                oldClrType: typeof(int),
                oldComment: "FK(Supervisor.ID)");

            migrationBuilder.AddForeignKey(
                name: "container_ibfk_3",
                table: "container",
                column: "Supervisor_ID",
                principalTable: "account",
                principalColumn: "Account_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
