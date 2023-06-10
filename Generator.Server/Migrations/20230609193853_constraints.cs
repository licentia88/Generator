using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class constraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_COMPONENTS_BASE_CB_IDENTIFIER",
                table: "COMPONENTS_BASE");

            migrationBuilder.AlterColumn<string>(
                name: "CB_QUERY_OR_METHOD",
                table: "COMPONENTS_BASE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CB_IDENTIFIER",
                table: "COMPONENTS_BASE",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CB_DATABASE",
                table: "COMPONENTS_BASE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_COMPONENTS_BASE_CB_IDENTIFIER",
                table: "COMPONENTS_BASE",
                column: "CB_IDENTIFIER",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_COMPONENTS_BASE_CB_IDENTIFIER",
                table: "COMPONENTS_BASE");

            migrationBuilder.AlterColumn<string>(
                name: "CB_QUERY_OR_METHOD",
                table: "COMPONENTS_BASE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CB_IDENTIFIER",
                table: "COMPONENTS_BASE",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CB_DATABASE",
                table: "COMPONENTS_BASE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_COMPONENTS_BASE_CB_IDENTIFIER",
                table: "COMPONENTS_BASE",
                column: "CB_IDENTIFIER");
        }
    }
}
