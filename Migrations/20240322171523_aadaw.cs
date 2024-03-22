using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChemStoreWebApp.Migrations
{
    public partial class aadaw : Migration
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
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<int>(type: "int", nullable: false),
                    SignalWord = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HazardStatement", x => x.HCode);
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
                name: "X_Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsHidden = table.Column<bool>(type: "bit", nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_X_Location_X_Location_ParentID",
                        column: x => x.ParentID,
                        principalTable: "X_Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "X_Chemical",
                columns: table => new
                {
                    ChemicalID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cas_Num = table.Column<int>(type: "int", nullable: false),
                    H_Code = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_X_Chemical", x => x.ChemicalID);
                    table.ForeignKey(
                        name: "FK_X_Chemical_HazardStatement_H_Code",
                        column: x => x.H_Code,
                        principalTable: "HazardStatement",
                        principalColumn: "HCode",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_X_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "X_Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "X_Container",
                columns: table => new
                {
                    ContainerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "OverridePermissions",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    LocationID = table.Column<int>(type: "int", nullable: false),
                    Permission = table.Column<int>(type: "int", nullable: false),
                    HasPermission = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverridePermissions", x => new { x.UserID, x.LocationID, x.Permission });
                    table.ForeignKey(
                        name: "FK_OverridePermissions_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OverridePermissions_X_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "X_Location",
                        principalColumn: "LocationID",
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
                name: "ContainerChemicals",
                columns: table => new
                {
                    ChemicalID = table.Column<long>(type: "bigint", nullable: false),
                    ContainerID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatalogNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreferredUnit = table.Column<int>(type: "int", nullable: false),
                    StateOfMatter = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContainerChemicals", x => new { x.ChemicalID, x.ContainerID });
                    table.ForeignKey(
                        name: "FK_ContainerChemicals_X_Container_ContainerID",
                        column: x => x.ContainerID,
                        principalTable: "X_Container",
                        principalColumn: "ContainerID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_ContainerChemicals_ContainerID",
                table: "ContainerChemicals",
                column: "ContainerID");

            migrationBuilder.CreateIndex(
                name: "IX_Log_UserID",
                table: "Log",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_OverridePermissions_LocationID",
                table: "OverridePermissions",
                column: "LocationID");

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
                name: "IX_X_Chemical_H_Code",
                table: "X_Chemical",
                column: "H_Code");

            migrationBuilder.CreateIndex(
                name: "IX_X_Container_LocationID",
                table: "X_Container",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_X_Location_DepartmentID",
                table: "X_Location",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_X_Location_ParentID",
                table: "X_Location",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_X_Log_UserID",
                table: "X_Log",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChemicalHazards");

            migrationBuilder.DropTable(
                name: "Container");

            migrationBuilder.DropTable(
                name: "ContainerChemicals");

            migrationBuilder.DropTable(
                name: "HazardPrecaution");

            migrationBuilder.DropTable(
                name: "LocationAttribute");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "OverridePermissions");

            migrationBuilder.DropTable(
                name: "PrecautionaryStatement");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "StatementPictogram");

            migrationBuilder.DropTable(
                name: "X_Chemical");

            migrationBuilder.DropTable(
                name: "X_Log");

            migrationBuilder.DropTable(
                name: "Hazard");

            migrationBuilder.DropTable(
                name: "Chemical");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "X_Container");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "HazardPictogram");

            migrationBuilder.DropTable(
                name: "HazardStatement");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "X_Location");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
