using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class gridBaseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GB_CREATE",
                table: "GRID_M",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GB_CRUD_SOURCE",
                table: "GRID_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GB_DELETE",
                table: "GRID_M",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GB_UPDATE",
                table: "GRID_M",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GB_CREATE",
                table: "GRID_M");

            migrationBuilder.DropColumn(
                name: "GB_CRUD_SOURCE",
                table: "GRID_M");

            migrationBuilder.DropColumn(
                name: "GB_DELETE",
                table: "GRID_M");

            migrationBuilder.DropColumn(
                name: "GB_UPDATE",
                table: "GRID_M");
        }
    }
}
