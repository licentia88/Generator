using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class gridFieldsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRID_FIELDS_CRUD_VIEW_GF_VIEW_REFNO",
                table: "GRID_FIELDS");

            migrationBuilder.DropTable(
                name: "CRUD_VIEW");

            migrationBuilder.RenameColumn(
                name: "GF_VIEW_REFNO",
                table: "GRID_FIELDS",
                newName: "GF_COMPONENT_REFNO");

            migrationBuilder.RenameIndex(
                name: "IX_GRID_FIELDS_GF_VIEW_REFNO",
                table: "GRID_FIELDS",
                newName: "IX_GRID_FIELDS_GF_COMPONENT_REFNO");

            migrationBuilder.AddForeignKey(
                name: "FK_GRID_FIELDS_COMPONENTS_BASE_GF_COMPONENT_REFNO",
                table: "GRID_FIELDS",
                column: "GF_COMPONENT_REFNO",
                principalTable: "COMPONENTS_BASE",
                principalColumn: "CB_ROWID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRID_FIELDS_COMPONENTS_BASE_GF_COMPONENT_REFNO",
                table: "GRID_FIELDS");

            migrationBuilder.RenameColumn(
                name: "GF_COMPONENT_REFNO",
                table: "GRID_FIELDS",
                newName: "GF_VIEW_REFNO");

            migrationBuilder.RenameIndex(
                name: "IX_GRID_FIELDS_GF_COMPONENT_REFNO",
                table: "GRID_FIELDS",
                newName: "IX_GRID_FIELDS_GF_VIEW_REFNO");

            migrationBuilder.CreateTable(
                name: "CRUD_VIEW",
                columns: table => new
                {
                    VBM_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VBM_GRID_REFNO = table.Column<int>(type: "int", nullable: false),
                    VBM_TITLE = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VBM_TYPE = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRUD_VIEW", x => x.VBM_ROWID);
                    table.ForeignKey(
                        name: "FK_CRUD_VIEW_GRID_M_VBM_GRID_REFNO",
                        column: x => x.VBM_GRID_REFNO,
                        principalTable: "GRID_M",
                        principalColumn: "CB_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CRUD_VIEW_VBM_GRID_REFNO",
                table: "CRUD_VIEW",
                column: "VBM_GRID_REFNO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CRUD_VIEW_VBM_GRID_REFNO_VBM_TYPE_VBM_TITLE",
                table: "CRUD_VIEW",
                columns: new[] { "VBM_GRID_REFNO", "VBM_TYPE", "VBM_TITLE" },
                unique: true,
                filter: "[VBM_TITLE] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GRID_FIELDS_CRUD_VIEW_GF_VIEW_REFNO",
                table: "GRID_FIELDS",
                column: "GF_VIEW_REFNO",
                principalTable: "CRUD_VIEW",
                principalColumn: "VBM_ROWID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
