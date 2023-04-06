using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations.Generator
{
    public partial class gentables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BUTTONS_BASE",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false),
                    BB_TOOLTIP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BUTTONS_BASE", x => x.CB_ROWID);
                    table.ForeignKey(
                        name: "FK_BUTTONS_BASE_COMPONENT_BASE_CB_ROWID",
                        column: x => x.CB_ROWID,
                        principalTable: "COMPONENT_BASE",
                        principalColumn: "CB_ROWID");
                });

            migrationBuilder.CreateTable(
                name: "CRUD_BUTTONS",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false),
                    BB_PAGE_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRUD_BUTTONS", x => x.CB_ROWID);
                    table.ForeignKey(
                        name: "FK_CRUD_BUTTONS_BUTTONS_BASE_CB_ROWID",
                        column: x => x.CB_ROWID,
                        principalTable: "BUTTONS_BASE",
                        principalColumn: "CB_ROWID");
                    table.ForeignKey(
                        name: "FK_CRUD_BUTTONS_PAGES_M_BB_PAGE_REFNO",
                        column: x => x.BB_PAGE_REFNO,
                        principalTable: "PAGES_M",
                        principalColumn: "CB_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GRID_BUTTONS",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false),
                    BB_PAGE_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRID_BUTTONS", x => x.CB_ROWID);
                    table.ForeignKey(
                        name: "FK_GRID_BUTTONS_BUTTONS_BASE_CB_ROWID",
                        column: x => x.CB_ROWID,
                        principalTable: "BUTTONS_BASE",
                        principalColumn: "CB_ROWID");
                });

            migrationBuilder.CreateTable(
                name: "HEADER_BUTTONS",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HEADER_BUTTONS", x => x.CB_ROWID);
                    table.ForeignKey(
                        name: "FK_HEADER_BUTTONS_BUTTONS_BASE_CB_ROWID",
                        column: x => x.CB_ROWID,
                        principalTable: "BUTTONS_BASE",
                        principalColumn: "CB_ROWID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRUD_BUTTONS_BB_PAGE_REFNO",
                table: "CRUD_BUTTONS",
                column: "BB_PAGE_REFNO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRUD_BUTTONS");

            migrationBuilder.DropTable(
                name: "GRID_BUTTONS");

            migrationBuilder.DropTable(
                name: "HEADER_BUTTONS");

            migrationBuilder.DropTable(
                name: "BUTTONS_BASE");
        }
    }
}
