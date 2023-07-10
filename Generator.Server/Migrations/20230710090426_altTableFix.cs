using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class altTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CRUD_VIEW_GRID_M_VBM_PAGE_REFNO",
                table: "CRUD_VIEW");

            migrationBuilder.DropColumn(
                name: "CV_CREATE",
                table: "CRUD_VIEW");

            migrationBuilder.DropColumn(
                name: "CV_DELETE",
                table: "CRUD_VIEW");

            migrationBuilder.DropColumn(
                name: "CV_UPDATE",
                table: "CRUD_VIEW");

            migrationBuilder.DropColumn(
                name: "VBM_SOURCE",
                table: "CRUD_VIEW");

            migrationBuilder.RenameColumn(
                name: "VBM_PAGE_REFNO",
                table: "CRUD_VIEW",
                newName: "VBM_GRID_REFNO");

            migrationBuilder.RenameIndex(
                name: "IX_CRUD_VIEW_VBM_PAGE_REFNO_VBM_TYPE_VBM_TITLE",
                table: "CRUD_VIEW",
                newName: "IX_CRUD_VIEW_VBM_GRID_REFNO_VBM_TYPE_VBM_TITLE");

            migrationBuilder.RenameIndex(
                name: "IX_CRUD_VIEW_VBM_PAGE_REFNO",
                table: "CRUD_VIEW",
                newName: "IX_CRUD_VIEW_VBM_GRID_REFNO");

            migrationBuilder.AddForeignKey(
                name: "FK_CRUD_VIEW_GRID_M_VBM_GRID_REFNO",
                table: "CRUD_VIEW",
                column: "VBM_GRID_REFNO",
                principalTable: "GRID_M",
                principalColumn: "CB_ROWID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CRUD_VIEW_GRID_M_VBM_GRID_REFNO",
                table: "CRUD_VIEW");

            migrationBuilder.RenameColumn(
                name: "VBM_GRID_REFNO",
                table: "CRUD_VIEW",
                newName: "VBM_PAGE_REFNO");

            migrationBuilder.RenameIndex(
                name: "IX_CRUD_VIEW_VBM_GRID_REFNO_VBM_TYPE_VBM_TITLE",
                table: "CRUD_VIEW",
                newName: "IX_CRUD_VIEW_VBM_PAGE_REFNO_VBM_TYPE_VBM_TITLE");

            migrationBuilder.RenameIndex(
                name: "IX_CRUD_VIEW_VBM_GRID_REFNO",
                table: "CRUD_VIEW",
                newName: "IX_CRUD_VIEW_VBM_PAGE_REFNO");

            migrationBuilder.AddColumn<bool>(
                name: "CV_CREATE",
                table: "CRUD_VIEW",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CV_DELETE",
                table: "CRUD_VIEW",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CV_UPDATE",
                table: "CRUD_VIEW",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "VBM_SOURCE",
                table: "CRUD_VIEW",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CRUD_VIEW_GRID_M_VBM_PAGE_REFNO",
                table: "CRUD_VIEW",
                column: "VBM_PAGE_REFNO",
                principalTable: "GRID_M",
                principalColumn: "CB_ROWID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
