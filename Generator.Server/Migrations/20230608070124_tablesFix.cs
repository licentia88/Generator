using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class tablesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AUTH_DESCRIPTION",
                table: "AUTH_BASE");

            migrationBuilder.AddColumn<string>(
                name: "PER_DESCRIPTION",
                table: "PERMISSIONS",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PER_DESCRIPTION",
                table: "PERMISSIONS");

            migrationBuilder.AddColumn<string>(
                name: "AUTH_DESCRIPTION",
                table: "AUTH_BASE",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
