using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations.Generator
{
    public partial class initialMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FOOTER_BUTTONS");

            migrationBuilder.DropTable(
                name: "HEADER_BUTTONS");

            migrationBuilder.CreateTable(
                name: "FOOTER_BUTTON",
                columns: table => new
                {
                    COMP_ROWID = table.Column<int>(type: "int", nullable: false),
                    FB_GRID_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOOTER_BUTTON", x => x.COMP_ROWID);
                    table.ForeignKey(
                        name: "FK_FOOTER_BUTTON_COMPONENT_COMP_ROWID",
                        column: x => x.COMP_ROWID,
                        principalTable: "COMPONENT",
                        principalColumn: "COMP_ROWID");
                    table.ForeignKey(
                        name: "FK_FOOTER_BUTTON_GRIDS_M_FB_GRID_REFNO",
                        column: x => x.FB_GRID_REFNO,
                        principalTable: "GRIDS_M",
                        principalColumn: "COMP_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HEADER_BUTTON",
                columns: table => new
                {
                    COMP_ROWID = table.Column<int>(type: "int", nullable: false),
                    HB_GRID_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HEADER_BUTTON", x => x.COMP_ROWID);
                    table.ForeignKey(
                        name: "FK_HEADER_BUTTON_COMPONENT_COMP_ROWID",
                        column: x => x.COMP_ROWID,
                        principalTable: "COMPONENT",
                        principalColumn: "COMP_ROWID");
                    table.ForeignKey(
                        name: "FK_HEADER_BUTTON_GRIDS_M_HB_GRID_REFNO",
                        column: x => x.HB_GRID_REFNO,
                        principalTable: "GRIDS_M",
                        principalColumn: "COMP_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FOOTER_BUTTON_FB_GRID_REFNO",
                table: "FOOTER_BUTTON",
                column: "FB_GRID_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_HEADER_BUTTON_HB_GRID_REFNO",
                table: "HEADER_BUTTON",
                column: "HB_GRID_REFNO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FOOTER_BUTTON");

            migrationBuilder.DropTable(
                name: "HEADER_BUTTON");

            migrationBuilder.CreateTable(
                name: "FOOTER_BUTTONS",
                columns: table => new
                {
                    COMP_ROWID = table.Column<int>(type: "int", nullable: false),
                    FB_GRID_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FOOTER_BUTTONS", x => x.COMP_ROWID);
                    table.ForeignKey(
                        name: "FK_FOOTER_BUTTONS_COMPONENT_COMP_ROWID",
                        column: x => x.COMP_ROWID,
                        principalTable: "COMPONENT",
                        principalColumn: "COMP_ROWID");
                    table.ForeignKey(
                        name: "FK_FOOTER_BUTTONS_GRIDS_M_FB_GRID_REFNO",
                        column: x => x.FB_GRID_REFNO,
                        principalTable: "GRIDS_M",
                        principalColumn: "COMP_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HEADER_BUTTONS",
                columns: table => new
                {
                    COMP_ROWID = table.Column<int>(type: "int", nullable: false),
                    HB_GRID_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HEADER_BUTTONS", x => x.COMP_ROWID);
                    table.ForeignKey(
                        name: "FK_HEADER_BUTTONS_COMPONENT_COMP_ROWID",
                        column: x => x.COMP_ROWID,
                        principalTable: "COMPONENT",
                        principalColumn: "COMP_ROWID");
                    table.ForeignKey(
                        name: "FK_HEADER_BUTTONS_GRIDS_M_HB_GRID_REFNO",
                        column: x => x.HB_GRID_REFNO,
                        principalTable: "GRIDS_M",
                        principalColumn: "COMP_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FOOTER_BUTTONS_FB_GRID_REFNO",
                table: "FOOTER_BUTTONS",
                column: "FB_GRID_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_HEADER_BUTTONS_HB_GRID_REFNO",
                table: "HEADER_BUTTONS",
                column: "HB_GRID_REFNO");
        }
    }
}
