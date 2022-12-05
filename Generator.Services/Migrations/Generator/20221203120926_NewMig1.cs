using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations.Generator
{
    public partial class NewMig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DATABASES",
                columns: table => new
                {
                    DatabaseIdentifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATABASES", x => x.DatabaseIdentifier);
                });

            migrationBuilder.CreateTable(
                name: "COMPONENT",
                columns: table => new
                {
                    COMP_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COMP_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COMP_DATABASE = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    COMP_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPONENT", x => x.COMP_ROWID);
                    table.ForeignKey(
                        name: "FK_COMPONENT_DATABASES_COMP_DATABASE",
                        column: x => x.COMP_DATABASE,
                        principalTable: "DATABASES",
                        principalColumn: "DatabaseIdentifier");
                });

            migrationBuilder.CreateTable(
                name: "GRIDS_M",
                columns: table => new
                {
                    COMP_ROWID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRIDS_M", x => x.COMP_ROWID);
                    table.ForeignKey(
                        name: "FK_GRIDS_M_COMPONENT_COMP_ROWID",
                        column: x => x.COMP_ROWID,
                        principalTable: "COMPONENT",
                        principalColumn: "COMP_ROWID");
                });

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
                name: "GRIDS_D",
                columns: table => new
                {
                    COMP_ROWID = table.Column<int>(type: "int", nullable: false),
                    GD_M_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRIDS_D", x => x.COMP_ROWID);
                    table.ForeignKey(
                        name: "FK_GRIDS_D_GRIDS_M_COMP_ROWID",
                        column: x => x.COMP_ROWID,
                        principalTable: "GRIDS_M",
                        principalColumn: "COMP_ROWID");
                    table.ForeignKey(
                        name: "FK_GRIDS_D_GRIDS_M_GD_M_REFNO",
                        column: x => x.GD_M_REFNO,
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
                name: "IX_COMPONENT_COMP_DATABASE",
                table: "COMPONENT",
                column: "COMP_DATABASE");

            migrationBuilder.CreateIndex(
                name: "IX_FOOTER_BUTTONS_FB_GRID_REFNO",
                table: "FOOTER_BUTTONS",
                column: "FB_GRID_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_GRIDS_D_GD_M_REFNO",
                table: "GRIDS_D",
                column: "GD_M_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_HEADER_BUTTONS_HB_GRID_REFNO",
                table: "HEADER_BUTTONS",
                column: "HB_GRID_REFNO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FOOTER_BUTTONS");

            migrationBuilder.DropTable(
                name: "GRIDS_D");

            migrationBuilder.DropTable(
                name: "HEADER_BUTTONS");

            migrationBuilder.DropTable(
                name: "GRIDS_M");

            migrationBuilder.DropTable(
                name: "COMPONENT");

            migrationBuilder.DropTable(
                name: "DATABASES");
        }
    }
}
