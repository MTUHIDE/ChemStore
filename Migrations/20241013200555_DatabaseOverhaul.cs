using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChemStoreWebApp.Migrations
{
    public partial class DatabaseOverhaul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentID);
                });

            migrationBuilder.CreateTable(
                name: "HazardPictogram",
                columns: table => new
                {
                    GHCode = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pictogram = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HazardPictogram", x => x.GHCode);
                });

            migrationBuilder.CreateTable(
                name: "HazardStatement",
                columns: table => new
                {
                    HCode = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Statements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignalWord = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HazardStatement", x => x.HCode);
                });

            migrationBuilder.CreateTable(
                name: "PrecautionaryStatement",
                columns: table => new
                {
                    PCode = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecautionaryStatement", x => x.PCode);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "HazardPrecaution",
                columns: table => new
                {
                    HCode = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    PCode = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HazardPrecaution", x => new { x.HCode, x.PCode });
                    table.ForeignKey(
                        name: "FK_HazardPrecaution_HazardStatement_HCode",
                        column: x => x.HCode,
                        principalTable: "HazardStatement",
                        principalColumn: "HCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StatementPictogram",
                columns: table => new
                {
                    GHCode = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    HCode = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementPictogram", x => new { x.GHCode, x.HCode });
                    table.ForeignKey(
                        name: "FK_StatementPictogram_HazardPictogram_GHCode",
                        column: x => x.GHCode,
                        principalTable: "HazardPictogram",
                        principalColumn: "GHCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatementPictogram_HazardStatement_HCode",
                        column: x => x.HCode,
                        principalTable: "HazardStatement",
                        principalColumn: "HCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID");
                    table.ForeignKey(
                        name: "FK_User_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "X_Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentID = table.Column<short>(type: "smallint", nullable: false),
                    Level = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: false),
                    SupervisorID = table.Column<int>(type: "int", nullable: false),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_X_Location", x => x.LocationID);
                    table.ForeignKey(
                        name: "FK_X_Location_Department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Department",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_X_Location_User_SupervisorID",
                        column: x => x.SupervisorID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "X_Log",
                columns: table => new
                {
                    LogID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_X_Log", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_X_Log_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocationAttribute",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationAttribute", x => new { x.LocationID, x.Key });
                    table.ForeignKey(
                        name: "FK_LocationAttribute_X_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "X_Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    Permission = table.Column<int>(type: "int", nullable: false),
                    HasPermission = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleID, x.LocationID, x.Permission });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_X_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "X_Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "X_Container",
                columns: table => new
                {
                    ContainerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_X_Container", x => x.ContainerID);
                    table.ForeignKey(
                        name: "FK_X_Container_X_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "X_Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContainerChemicals",
                columns: table => new
                {
                    ContainerID = table.Column<int>(type: "int", nullable: false),
                    PubchemCID = table.Column<int>(type: "int", nullable: false),
                    ChemicalCAS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatalogNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredUnit = table.Column<int>(type: "int", nullable: false),
                    StateOfMatter = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerChemicals", x => new { x.ContainerID, x.PubchemCID });
                    table.ForeignKey(
                        name: "FK_ContainerChemicals_X_Container_ContainerID",
                        column: x => x.ContainerID,
                        principalTable: "X_Container",
                        principalColumn: "ContainerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContainerHazards",
                columns: table => new
                {
                    ContainerID = table.Column<int>(type: "int", nullable: false),
                    HCode = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerHazards", x => x.ContainerID);
                    table.ForeignKey(
                        name: "FK_ContainerHazards_HazardStatement_HCode",
                        column: x => x.HCode,
                        principalTable: "HazardStatement",
                        principalColumn: "HCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContainerHazards_X_Container_ContainerID",
                        column: x => x.ContainerID,
                        principalTable: "X_Container",
                        principalColumn: "ContainerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContainerHazards_HCode",
                table: "ContainerHazards",
                column: "HCode");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_LocationID",
                table: "RolePermissions",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_StatementPictogram_HCode",
                table: "StatementPictogram",
                column: "HCode");

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentID",
                table: "User",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_X_Container_LocationID",
                table: "X_Container",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_X_Location_DepartmentID",
                table: "X_Location",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_X_Location_SupervisorID",
                table: "X_Location",
                column: "SupervisorID");

            migrationBuilder.CreateIndex(
                name: "IX_X_Log_UserID",
                table: "X_Log",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContainerChemicals");

            migrationBuilder.DropTable(
                name: "ContainerHazards");

            migrationBuilder.DropTable(
                name: "HazardPrecaution");

            migrationBuilder.DropTable(
                name: "LocationAttribute");

            migrationBuilder.DropTable(
                name: "PrecautionaryStatement");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "StatementPictogram");

            migrationBuilder.DropTable(
                name: "X_Log");

            migrationBuilder.DropTable(
                name: "X_Container");

            migrationBuilder.DropTable(
                name: "HazardPictogram");

            migrationBuilder.DropTable(
                name: "HazardStatement");

            migrationBuilder.DropTable(
                name: "X_Location");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
