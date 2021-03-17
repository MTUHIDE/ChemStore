using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ChemStoreWebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "building",
                columns: table => new
                {
                    building_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    building_name = table.Column<string>(unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_building", x => x.building_id);
                });

            migrationBuilder.CreateTable(
                name: "chem_in_container",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chem_in_container", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "chemical",
                columns: table => new
                {
                    cas_number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    chem_name = table.Column<string>(unicode: false, maxLength: 45, nullable: true),
                    hazard_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY_CAS", x => x.cas_number);
                });

            migrationBuilder.CreateTable(
                name: "has_location",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_has_location", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hazard",
                columns: table => new
                {
                    hazard_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    hazard_details = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hazard", x => x.hazard_id);
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
                name: "container",
                columns: table => new
                {
                    container_id = table.Column<int>(type: "int", nullable: false),
                    unit = table.Column<int>(unicode: false, maxLength: 45, nullable: false),
                    size = table.Column<double>(nullable: true),
                    chem_id = table.Column<int>(type: "int", nullable: true),
                    location_id = table.Column<int>(type: "int", nullable: true),
                    pic_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_container", x => x.container_id);
                    table.ForeignKey(
                        name: "container_id",
                        column: x => x.container_id,
                        principalTable: "has_location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "location",
                columns: table => new
                {
                    location_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    department = table.Column<int>(type: "int", nullable: false),
                    room = table.Column<int>(type: "int", nullable: true),
                    building = table.Column<int>(type: "int", nullable: false),
                    location_fid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_location", x => x.location_id);
                    table.ForeignKey(
                        name: "location_fid",
                        column: x => x.location_fid,
                        principalTable: "has_location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Currently stores the location explicitly, not the foreign keys of each respective table.");

            migrationBuilder.CreateTable(
                name: "person_in_charge",
                columns: table => new
                {
                    pic_id = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(unicode: false, maxLength: 40, nullable: true),
                    pic_name = table.Column<string>(unicode: false, maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY_ID", x => x.pic_id);
                    table.ForeignKey(
                        name: "pic_id",
                        column: x => x.pic_id,
                        principalTable: "has_location",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "has_hazard",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    hazard_id = table.Column<int>(type: "int", nullable: true),
                    chemical_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_has_hazard", x => x.id);
                    table.ForeignKey(
                        name: "chemical_id",
                        column: x => x.chemical_id,
                        principalTable: "chemical",
                        principalColumn: "cas_number",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "hazard_id",
                        column: x => x.hazard_id,
                        principalTable: "hazard",
                        principalColumn: "hazard_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "chemical_id_idx",
                table: "has_hazard",
                column: "chemical_id");

            migrationBuilder.CreateIndex(
                name: "hazard_id_idx",
                table: "has_hazard",
                column: "hazard_id");

            migrationBuilder.CreateIndex(
                name: "location_fid_idx",
                table: "location",
                column: "location_fid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "building");

            migrationBuilder.DropTable(
                name: "chem_in_container");

            migrationBuilder.DropTable(
                name: "container");

            migrationBuilder.DropTable(
                name: "has_hazard");

            migrationBuilder.DropTable(
                name: "location");

            migrationBuilder.DropTable(
                name: "person_in_charge");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "chemical");

            migrationBuilder.DropTable(
                name: "hazard");

            migrationBuilder.DropTable(
                name: "has_location");
        }
    }
}
