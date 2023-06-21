using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class allowDeleteOnRolesDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLES_DETAILS_PERMISSIONS_RD_PERMISSION_REFNO",
                table: "ROLES_DETAILS");

            migrationBuilder.AlterColumn<int>(
                name: "RD_PERMISSION_REFNO",
                table: "ROLES_DETAILS",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ROLES_DETAILS_PERMISSIONS_RD_PERMISSION_REFNO",
                table: "ROLES_DETAILS",
                column: "RD_PERMISSION_REFNO",
                principalTable: "PERMISSIONS",
                principalColumn: "AUTH_ROWID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ROLES_DETAILS_PERMISSIONS_RD_PERMISSION_REFNO",
                table: "ROLES_DETAILS");

            migrationBuilder.AlterColumn<int>(
                name: "RD_PERMISSION_REFNO",
                table: "ROLES_DETAILS",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ROLES_DETAILS_PERMISSIONS_RD_PERMISSION_REFNO",
                table: "ROLES_DETAILS",
                column: "RD_PERMISSION_REFNO",
                principalTable: "PERMISSIONS",
                principalColumn: "AUTH_ROWID");
        }
    }
}
