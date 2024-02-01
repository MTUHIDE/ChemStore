using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChemStoreWebApp.Migrations
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: true),
                    Supervisor = table.Column<bool>(type: "bit", nullable: false),
                    Department = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    BuildingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Abbr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coords = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.BuildingId);
                });

            migrationBuilder.CreateTable(
                name: "Chemical",
                columns: table => new
                {
                    CasNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChemicalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatalogNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chemical", x => x.CasNumber);
                });

            migrationBuilder.CreateTable(
                name: "Hazard",
                columns: table => new
                {
                    HazardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hazard", x => x.HazardId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingName = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    IDLog = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContainerID = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.IDLog);
                    table.ForeignKey(
                        name: "FK_Log_Account_UserID",
                        column: x => x.UserID,
                        principalTable: "Account",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "ChemicalHazards",
                columns: table => new
                {
                    IDChemicalHazard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CasNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HazardId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CasNumberNavigationCasNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemicalHazards", x => x.IDChemicalHazard);
                    table.ForeignKey(
                        name: "FK_ChemicalHazards_Chemical_CasNumberNavigationCasNumber",
                        column: x => x.CasNumberNavigationCasNumber,
                        principalTable: "Chemical",
                        principalColumn: "CasNumber");
                    table.ForeignKey(
                        name: "FK_ChemicalHazards_Hazard_HazardId",
                        column: x => x.HazardId,
                        principalTable: "Hazard",
                        principalColumn: "HazardId");
                });

            migrationBuilder.CreateTable(
                name: "Container",
                columns: table => new
                {
                    ContainerId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Retired = table.Column<bool>(type: "bit", nullable: false),
                    CasNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    CasNumberNavigationCasNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Container", x => x.ContainerId);
                    table.ForeignKey(
                        name: "FK_Container_Account_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Account",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Container_Chemical_CasNumberNavigationCasNumber",
                        column: x => x.CasNumberNavigationCasNumber,
                        principalTable: "Chemical",
                        principalColumn: "CasNumber");
                    table.ForeignKey(
                        name: "FK_Container_Location_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Location",
                        principalColumn: "RoomId");
                });

            migrationBuilder.InsertData(
                table: "Hazard",
                columns: new[] { "HazardId", "Description" },
                values: new object[,]
                {
                    { "Corrosion", "Corrosive" },
                    { "Environment", "Enviornmental Hazard" },
                    { "ExclamationMark", "Exclamation Mark" },
                    { "ExplodingBomb", "Exploding Bomb" },
                    { "Flame", "Flame" },
                    { "FlameOverCircle", "Flame Over Circle" },
                    { "GasCylinder", "Gas Cylinder" },
                    { "HealthHazard", "HealthHazard" },
                    { "Skull", "Skull" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChemicalHazards_CasNumberNavigationCasNumber",
                table: "ChemicalHazards",
                column: "CasNumberNavigationCasNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ChemicalHazards_HazardId",
                table: "ChemicalHazards",
                column: "HazardId");

            migrationBuilder.CreateIndex(
                name: "IX_Container_CasNumberNavigationCasNumber",
                table: "Container",
                column: "CasNumberNavigationCasNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Container_RoomId",
                table: "Container",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Container_SupervisorId",
                table: "Container",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Log_UserID",
                table: "Log",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "ChemicalHazards");

            migrationBuilder.DropTable(
                name: "Container");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Hazard");

            migrationBuilder.DropTable(
                name: "Chemical");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
