using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations
{
    public partial class codeTableRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PC_STRING_TABLE_CODE",
                table: "PARENT_CLASS",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PARENT_CLASS_PC_STRING_TABLE_CODE",
                table: "PARENT_CLASS",
                column: "PC_STRING_TABLE_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_PARENT_CLASS_STRING_TABLE_PC_STRING_TABLE_CODE",
                table: "PARENT_CLASS",
                column: "PC_STRING_TABLE_CODE",
                principalTable: "STRING_TABLE",
                principalColumn: "CT_ROWID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PARENT_CLASS_STRING_TABLE_PC_STRING_TABLE_CODE",
                table: "PARENT_CLASS");

            migrationBuilder.DropIndex(
                name: "IX_PARENT_CLASS_PC_STRING_TABLE_CODE",
                table: "PARENT_CLASS");

            migrationBuilder.DropColumn(
                name: "PC_STRING_TABLE_CODE",
                table: "PARENT_CLASS");
        }
    }
}
