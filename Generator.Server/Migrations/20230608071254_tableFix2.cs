using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class tableFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PERMISSIONS_AUTH_BASE_AUTH_CODE",
                table: "PERMISSIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLES_AUTH_BASE_AUTH_CODE",
                table: "ROLES");

            migrationBuilder.RenameColumn(
                name: "AUTH_CODE",
                table: "ROLES",
                newName: "AUTH_ROWID");

            migrationBuilder.RenameColumn(
                name: "AUTH_CODE",
                table: "PERMISSIONS",
                newName: "AUTH_ROWID");

            migrationBuilder.RenameColumn(
                name: "AUTH_CODE",
                table: "AUTH_BASE",
                newName: "AUTH_ROWID");

            migrationBuilder.AddForeignKey(
                name: "FK_PERMISSIONS_AUTH_BASE_AUTH_ROWID",
                table: "PERMISSIONS",
                column: "AUTH_ROWID",
                principalTable: "AUTH_BASE",
                principalColumn: "AUTH_ROWID");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLES_AUTH_BASE_AUTH_ROWID",
                table: "ROLES",
                column: "AUTH_ROWID",
                principalTable: "AUTH_BASE",
                principalColumn: "AUTH_ROWID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PERMISSIONS_AUTH_BASE_AUTH_ROWID",
                table: "PERMISSIONS");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLES_AUTH_BASE_AUTH_ROWID",
                table: "ROLES");

            migrationBuilder.RenameColumn(
                name: "AUTH_ROWID",
                table: "ROLES",
                newName: "AUTH_CODE");

            migrationBuilder.RenameColumn(
                name: "AUTH_ROWID",
                table: "PERMISSIONS",
                newName: "AUTH_CODE");

            migrationBuilder.RenameColumn(
                name: "AUTH_ROWID",
                table: "AUTH_BASE",
                newName: "AUTH_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_PERMISSIONS_AUTH_BASE_AUTH_CODE",
                table: "PERMISSIONS",
                column: "AUTH_CODE",
                principalTable: "AUTH_BASE",
                principalColumn: "AUTH_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLES_AUTH_BASE_AUTH_CODE",
                table: "ROLES",
                column: "AUTH_CODE",
                principalTable: "AUTH_BASE",
                principalColumn: "AUTH_CODE");
        }
    }
}
