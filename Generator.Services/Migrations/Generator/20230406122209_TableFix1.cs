using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations.Generator
{
    public partial class TableFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PM_TABLE",
                table: "PAGES_M");

            migrationBuilder.DropColumn(
                name: "DF_ALIAS_FIELD",
                table: "DISPLAY_FIELDS");

            migrationBuilder.RenameColumn(
                name: "DF_TABLE_NAME",
                table: "DISPLAY_FIELDS",
                newName: "DF_STORED_PROCEDURE");

            migrationBuilder.AddColumn<string>(
                name: "DF_TITLE",
                table: "DISPLAY_FIELDS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CB_TITLE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CB_SQL_COMMAND",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CB_DATABASE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CB_CODE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CB_TABLE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DF_TITLE",
                table: "DISPLAY_FIELDS");

            migrationBuilder.DropColumn(
                name: "CB_TABLE",
                table: "COMPONENT_BASE");

            migrationBuilder.RenameColumn(
                name: "DF_STORED_PROCEDURE",
                table: "DISPLAY_FIELDS",
                newName: "DF_TABLE_NAME");

            migrationBuilder.AddColumn<string>(
                name: "PM_TABLE",
                table: "PAGES_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DF_ALIAS_FIELD",
                table: "DISPLAY_FIELDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CB_TITLE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CB_SQL_COMMAND",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CB_DATABASE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CB_CODE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
