using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class crudviewLimitPerPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CRUD_VIEW_VBM_PAGE_REFNO",
                table: "CRUD_VIEW",
                column: "VBM_PAGE_REFNO",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CRUD_VIEW_VBM_PAGE_REFNO",
                table: "CRUD_VIEW");
        }
    }
}
