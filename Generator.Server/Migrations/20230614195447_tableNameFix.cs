using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class tableNameFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLES_D_PERMISSIONS_RD_PERMISSION_REFNO",
                table: "ROLES_D");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLES_D_ROLES_RD_M_REFNO",
                table: "ROLES_D");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ROLES_D",
                table: "ROLES_D");

            migrationBuilder.RenameTable(
                name: "ROLES_D",
                newName: "ROLES_DETAILS");

            migrationBuilder.RenameIndex(
                name: "IX_ROLES_D_RD_PERMISSION_REFNO",
                table: "ROLES_DETAILS",
                newName: "IX_ROLES_DETAILS_RD_PERMISSION_REFNO");

            migrationBuilder.RenameIndex(
                name: "IX_ROLES_D_RD_M_REFNO",
                table: "ROLES_DETAILS",
                newName: "IX_ROLES_DETAILS_RD_M_REFNO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ROLES_DETAILS",
                table: "ROLES_DETAILS",
                column: "RD_ROWID");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLES_DETAILS_PERMISSIONS_RD_PERMISSION_REFNO",
                table: "ROLES_DETAILS",
                column: "RD_PERMISSION_REFNO",
                principalTable: "PERMISSIONS",
                principalColumn: "AUTH_ROWID");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLES_DETAILS_ROLES_RD_M_REFNO",
                table: "ROLES_DETAILS",
                column: "RD_M_REFNO",
                principalTable: "ROLES",
                principalColumn: "AUTH_ROWID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLES_DETAILS_PERMISSIONS_RD_PERMISSION_REFNO",
                table: "ROLES_DETAILS");

            migrationBuilder.DropForeignKey(
                name: "FK_ROLES_DETAILS_ROLES_RD_M_REFNO",
                table: "ROLES_DETAILS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ROLES_DETAILS",
                table: "ROLES_DETAILS");

            migrationBuilder.RenameTable(
                name: "ROLES_DETAILS",
                newName: "ROLES_D");

            migrationBuilder.RenameIndex(
                name: "IX_ROLES_DETAILS_RD_PERMISSION_REFNO",
                table: "ROLES_D",
                newName: "IX_ROLES_D_RD_PERMISSION_REFNO");

            migrationBuilder.RenameIndex(
                name: "IX_ROLES_DETAILS_RD_M_REFNO",
                table: "ROLES_D",
                newName: "IX_ROLES_D_RD_M_REFNO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ROLES_D",
                table: "ROLES_D",
                column: "RD_ROWID");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLES_D_PERMISSIONS_RD_PERMISSION_REFNO",
                table: "ROLES_D",
                column: "RD_PERMISSION_REFNO",
                principalTable: "PERMISSIONS",
                principalColumn: "AUTH_ROWID");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLES_D_ROLES_RD_M_REFNO",
                table: "ROLES_D",
                column: "RD_M_REFNO",
                principalTable: "ROLES",
                principalColumn: "AUTH_ROWID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
