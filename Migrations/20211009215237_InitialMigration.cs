using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ChemStoreWebApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    Account_ID = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    Role = table.Column<int>(nullable: true),
                    Supervisor = table.Column<bool>(nullable: false),
                    Department = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.Account_ID);
                });

            migrationBuilder.CreateTable(
                name: "chemical",
                columns: table => new
                {
                    CAS_Number = table.Column<string>(maxLength: 255, nullable: false),
                    Chemical_Name = table.Column<string>(maxLength: 255, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Manufacturer = table.Column<string>(maxLength: 255, nullable: false),
                    Catalog_Number = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.CAS_Number);
                });

            migrationBuilder.CreateTable(
                name: "hazard",
                columns: table => new
                {
                    Hazard_ID = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hazard", x => x.Hazard_ID);
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    Room_ID = table.Column<string>(maxLength: 255, nullable: false),
                    Room_Number = table.Column<string>(maxLength: 255, nullable: false),
                    Building_Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Room_ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    NumberOfUsers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chemical_hazards",
                columns: table => new
                {
                    CAS_Number = table.Column<string>(maxLength: 255, nullable: false),
                    Hazard_ID = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.CAS_Number, x.Hazard_ID });
                    table.ForeignKey(
                        name: "chemical_hazards_ibfk_1",
                        column: x => x.CAS_Number,
                        principalTable: "chemical",
                        principalColumn: "CAS_Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "chemical_hazards_ibfk_2",
                        column: x => x.Hazard_ID,
                        principalTable: "hazard",
                        principalColumn: "Hazard_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "container",
                columns: table => new
                {
                    Container_ID = table.Column<long>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Unit = table.Column<int>(nullable: false, comment: "ex. ML, g, gallons"),
                    Amount = table.Column<int>(nullable: false, comment: "Amount of the unit (200 g)"),
                    Retired = table.Column<bool>(nullable: false),
                    CAS_Number = table.Column<string>(maxLength: 255, nullable: false, comment: "FK(Chemical.id)"),
                    Room_ID = table.Column<string>(maxLength: 255, nullable: true),
                    Supervisor_ID = table.Column<long>(nullable: false, comment: "FK(Supervisor.ID)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_container", x => x.Container_ID);
                    table.ForeignKey(
                        name: "container_ibfk_1",
                        column: x => x.CAS_Number,
                        principalTable: "chemical",
                        principalColumn: "CAS_Number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "container_ibfk_2",
                        column: x => x.Room_ID,
                        principalTable: "location",
                        principalColumn: "Room_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "container_ibfk_3",
                        column: x => x.Supervisor_ID,
                        principalTable: "account",
                        principalColumn: "Account_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Hazard_ID",
                table: "chemical_hazards",
                column: "Hazard_ID");

            migrationBuilder.CreateIndex(
                name: "CAS_Number",
                table: "container",
                column: "CAS_Number");

            migrationBuilder.CreateIndex(
                name: "Room_ID",
                table: "container",
                column: "Room_ID");

            migrationBuilder.CreateIndex(
                name: "Supervisor_ID",
                table: "container",
                column: "Supervisor_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chemical_hazards");

            migrationBuilder.DropTable(
                name: "container");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "hazard");

            migrationBuilder.DropTable(
                name: "chemical");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "account");
        }
    }
}
