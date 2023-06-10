using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class authName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_AUTHORIZATIONS_USERS_USERSU_ROWID",
                table: "USER_AUTHORIZATIONS");

            migrationBuilder.DropIndex(
                name: "IX_USER_AUTHORIZATIONS_USERSU_ROWID",
                table: "USER_AUTHORIZATIONS");

            migrationBuilder.DropColumn(
                name: "USERSU_ROWID",
                table: "USER_AUTHORIZATIONS");

            migrationBuilder.AddColumn<string>(
                name: "AUTH_NAME",
                table: "AUTH_BASE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_AUTHORIZATIONS_UA_USER_REFNO",
                table: "USER_AUTHORIZATIONS",
                column: "UA_USER_REFNO");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_AUTHORIZATIONS_USERS_UA_USER_REFNO",
                table: "USER_AUTHORIZATIONS",
                column: "UA_USER_REFNO",
                principalTable: "USERS",
                principalColumn: "U_ROWID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_AUTHORIZATIONS_USERS_UA_USER_REFNO",
                table: "USER_AUTHORIZATIONS");

            migrationBuilder.DropIndex(
                name: "IX_USER_AUTHORIZATIONS_UA_USER_REFNO",
                table: "USER_AUTHORIZATIONS");

            migrationBuilder.DropColumn(
                name: "AUTH_NAME",
                table: "AUTH_BASE");

            migrationBuilder.AddColumn<int>(
                name: "USERSU_ROWID",
                table: "USER_AUTHORIZATIONS",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_USER_AUTHORIZATIONS_USERSU_ROWID",
                table: "USER_AUTHORIZATIONS",
                column: "USERSU_ROWID");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_AUTHORIZATIONS_USERS_USERSU_ROWID",
                table: "USER_AUTHORIZATIONS",
                column: "USERSU_ROWID",
                principalTable: "USERS",
                principalColumn: "U_ROWID");
        }
    }
}
