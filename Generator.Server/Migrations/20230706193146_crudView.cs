using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class crudView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CRUD_VIEW",
                columns: table => new
                {
                    VBM_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CV_CREATE = table.Column<bool>(type: "bit", nullable: false),
                    CV_UPDATE = table.Column<bool>(type: "bit", nullable: false),
                    CV_DELETE = table.Column<bool>(type: "bit", nullable: false),
                    VBM_TYPE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VBM_PAGE_REFNO = table.Column<int>(type: "int", nullable: false),
                    VBM_TITLE = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VBM_SOURCE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRUD_VIEW", x => x.VBM_ROWID);
                    table.ForeignKey(
                        name: "FK_CRUD_VIEW_GRID_M_VBM_PAGE_REFNO",
                        column: x => x.VBM_PAGE_REFNO,
                        principalTable: "GRID_M",
                        principalColumn: "CB_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GRID_FIELDS",
                columns: table => new
                {
                    GF_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GF_VIEW_REFNO = table.Column<int>(type: "int", nullable: false),
                    GF_CONTROL_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_BINDINGFIELD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_LABEL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_XS = table.Column<int>(type: "int", nullable: true),
                    GF_SM = table.Column<int>(type: "int", nullable: true),
                    GF_MD = table.Column<int>(type: "int", nullable: true),
                    GF_LG = table.Column<int>(type: "int", nullable: true),
                    GF_XLG = table.Column<int>(type: "int", nullable: true),
                    GF_XXLG = table.Column<int>(type: "int", nullable: true),
                    GF_EDITOR_VISIBLE = table.Column<bool>(type: "bit", nullable: false),
                    GF_EDITOR_ENABLED = table.Column<bool>(type: "bit", nullable: false),
                    GF_GRID_VISIBLE = table.Column<bool>(type: "bit", nullable: false),
                    GF_USE_AS_SEARCHFIELD = table.Column<bool>(type: "bit", nullable: false),
                    GF_REQUIRED = table.Column<bool>(type: "bit", nullable: false),
                    GF_DATABASE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_SOURCE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_VALUEFIELD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_DISPLAYFIELD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_DATASOURCE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_DATASOURCE_QUERY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_TRUE_TEXT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_FALSE_TEXT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_FORMAT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GF_INPUT_TYPE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRID_FIELDS", x => x.GF_ROWID);
                    table.ForeignKey(
                        name: "FK_GRID_FIELDS_CRUD_VIEW_GF_VIEW_REFNO",
                        column: x => x.GF_VIEW_REFNO,
                        principalTable: "CRUD_VIEW",
                        principalColumn: "VBM_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRUD_VIEW_VBM_PAGE_REFNO_VBM_TYPE_VBM_TITLE",
                table: "CRUD_VIEW",
                columns: new[] { "VBM_PAGE_REFNO", "VBM_TYPE", "VBM_TITLE" },
                unique: true,
                filter: "[VBM_TITLE] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GRID_FIELDS_GF_VIEW_REFNO",
                table: "GRID_FIELDS",
                column: "GF_VIEW_REFNO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GRID_FIELDS");

            migrationBuilder.DropTable(
                name: "CRUD_VIEW");
        }
    }
}
