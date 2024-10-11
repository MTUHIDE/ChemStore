using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChemStoreWebApp.Migrations
{
    public partial class ModelUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_X_Location_X_Location_ParentID",
                table: "X_Location");

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

            migrationBuilder.DropIndex(
                name: "IX_X_Location_ParentID",
                table: "X_Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContainerChemicals",
                table: "ContainerChemicals");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "Product_Name",
                table: "X_Container",
                newName: "ProductName");

            migrationBuilder.AlterColumn<short>(
                name: "ParentID",
                table: "X_Location",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte>(
                name: "Level",
                table: "X_Location",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SupervisorID",
                table: "X_Location",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ChemicalCAS",
                table: "ContainerChemicals",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContainerChemicals",
                table: "ContainerChemicals",
                columns: new[] { "ContainerID", "PubchemCID" });

            migrationBuilder.CreateIndex(
                name: "IX_X_Location_SupervisorID",
                table: "X_Location",
                column: "SupervisorID");

            migrationBuilder.AddForeignKey(
                name: "FK_X_Location_User_SupervisorID",
                table: "X_Location",
                column: "SupervisorID",
                principalTable: "User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_X_Location_User_SupervisorID",
                table: "X_Location");

            migrationBuilder.DropIndex(
                name: "IX_X_Location_SupervisorID",
                table: "X_Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContainerChemicals",
                table: "ContainerChemicals");

            migrationBuilder.DropColumn(
                name: "SupervisorID",
                table: "X_Location");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "X_Container",
                newName: "Product_Name");

            migrationBuilder.AlterColumn<int>(
                name: "ParentID",
                table: "X_Location",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "X_Location",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ChemicalCAS",
                table: "ContainerChemicals",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContainerChemicals",
                table: "ContainerChemicals",
                columns: new[] { "ContainerID", "ChemicalCAS" });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: true),
                    Supervisor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Chemical",
                columns: table => new
                {
                    CasNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CatalogNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChemicalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    BuildingName = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<int>(type: "int", nullable: true),
                    ContainerID = table.Column<long>(type: "bigint", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    table = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CasNumberNavigationCasNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    HazardId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CasNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    CasNumberNavigationCasNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoomId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    CasNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Retired = table.Column<bool>(type: "bit", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_X_Location_ParentID",
                table: "X_Location",
                column: "ParentID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_X_Location_X_Location_ParentID",
                table: "X_Location",
                column: "ParentID",
                principalTable: "X_Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
