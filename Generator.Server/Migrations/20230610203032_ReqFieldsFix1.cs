using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class ReqFieldsFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_COMPONENTS_BASE_CB_IDENTIFIER",
                table: "COMPONENTS_BASE");

            migrationBuilder.AlterColumn<string>(
                name: "CB_IDENTIFIER",
                table: "COMPONENTS_BASE",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_COMPONENTS_BASE_CB_IDENTIFIER",
                table: "COMPONENTS_BASE",
                column: "CB_IDENTIFIER",
                unique: true,
                filter: "[CB_IDENTIFIER] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_COMPONENTS_BASE_CB_IDENTIFIER",
                table: "COMPONENTS_BASE");

            migrationBuilder.AlterColumn<string>(
                name: "CB_IDENTIFIER",
                table: "COMPONENTS_BASE",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_COMPONENTS_BASE_CB_IDENTIFIER",
                table: "COMPONENTS_BASE",
                column: "CB_IDENTIFIER",
                unique: true);
        }
    }
}
