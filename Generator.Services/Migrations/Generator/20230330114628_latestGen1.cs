using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations.Generator
{
    public partial class latestGen1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CB_COMMAND_TYPE",
                table: "COMPONENT_BASE",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PAGES_D_PAGES_M_PD_M_REFNO",
                table: "PAGES_D",
                column: "PD_M_REFNO",
                principalTable: "PAGES_M",
                principalColumn: "CB_ROWID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PAGES_D_PAGES_M_PD_M_REFNO",
                table: "PAGES_D");

            migrationBuilder.AlterColumn<string>(
                name: "CB_COMMAND_TYPE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
