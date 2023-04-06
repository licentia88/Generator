using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations.Generator
{
    public partial class newTablesGen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMPONENT_BASE",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CB_COMPONENT_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPONENT_BASE", x => x.CB_ROWID);
                });

            migrationBuilder.CreateTable(
                name: "PAGES_M",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false),
                    PB_DESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PB_CREATE = table.Column<bool>(type: "bit", nullable: false),
                    PB_READ = table.Column<bool>(type: "bit", nullable: false),
                    PB_UPDATE = table.Column<bool>(type: "bit", nullable: false),
                    PB_DELETE = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAGES_M", x => x.CB_ROWID);
                    table.ForeignKey(
                        name: "FK_PAGES_M_COMPONENT_BASE_CB_ROWID",
                        column: x => x.CB_ROWID,
                        principalTable: "COMPONENT_BASE",
                        principalColumn: "CB_ROWID");
                });

            migrationBuilder.CreateTable(
                name: "PERMISSIONS",
                columns: table => new
                {
                    PER_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PER_COMP_REFNO = table.Column<int>(type: "int", nullable: false),
                    PER_COMP_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PER_DESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PER_AUTH_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSIONS", x => x.PER_ROWID);
                    table.ForeignKey(
                        name: "FK_PERMISSIONS_COMPONENT_BASE_PER_COMP_REFNO",
                        column: x => x.PER_COMP_REFNO,
                        principalTable: "COMPONENT_BASE",
                        principalColumn: "CB_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PAGES_D",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false),
                    PD_M_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAGES_D", x => x.CB_ROWID);
                    table.ForeignKey(
                        name: "FK_PAGES_D_PAGES_M_CB_ROWID",
                        column: x => x.CB_ROWID,
                        principalTable: "PAGES_M",
                        principalColumn: "CB_ROWID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSIONS_PER_COMP_REFNO",
                table: "PERMISSIONS",
                column: "PER_COMP_REFNO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PAGES_D");

            migrationBuilder.DropTable(
                name: "PERMISSIONS");

            migrationBuilder.DropTable(
                name: "PAGES_M");

            migrationBuilder.DropTable(
                name: "COMPONENT_BASE");
        }
    }
}
