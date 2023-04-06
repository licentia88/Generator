using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations.Generator
{
    public partial class AddedIndexToPageD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PERMISSIONS_COMPONENT_BASE_PER_COMP_REFNO",
                table: "PERMISSIONS");

            migrationBuilder.DropColumn(
                name: "PM_DATABASE",
                table: "PAGES_M");

            migrationBuilder.RenameColumn(
                name: "PER_COMP_REFNO",
                table: "PERMISSIONS",
                newName: "PER_COMPONENT_REFNO");

            migrationBuilder.RenameIndex(
                name: "IX_PERMISSIONS_PER_COMP_REFNO_PER_AUTH_CODE",
                table: "PERMISSIONS",
                newName: "IX_PERMISSIONS_PER_COMPONENT_REFNO_PER_AUTH_CODE");

            migrationBuilder.AddColumn<string>(
                name: "CB_COMMAND_TYPE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CB_DATABASE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DISPLAY_FIELDS",
                columns: table => new
                {
                    DF_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DF_COMPONENT_REFNO = table.Column<int>(type: "int", nullable: false),
                    DF_FIELD_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DF_ALIAS_FIELD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DF_DATABASE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DF_TABLE_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISPLAY_FIELDS", x => x.DF_ROWID);
                    table.ForeignKey(
                        name: "FK_DISPLAY_FIELDS_COMPONENT_BASE_DF_COMPONENT_REFNO",
                        column: x => x.DF_COMPONENT_REFNO,
                        principalTable: "COMPONENT_BASE",
                        principalColumn: "CB_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PAGES_D_PD_M_REFNO",
                table: "PAGES_D",
                column: "PD_M_REFNO",
                unique: true,
                filter: "[PD_M_REFNO] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DISPLAY_FIELDS_DF_COMPONENT_REFNO",
                table: "DISPLAY_FIELDS",
                column: "DF_COMPONENT_REFNO");

            migrationBuilder.AddForeignKey(
                name: "FK_PERMISSIONS_COMPONENT_BASE_PER_COMPONENT_REFNO",
                table: "PERMISSIONS",
                column: "PER_COMPONENT_REFNO",
                principalTable: "COMPONENT_BASE",
                principalColumn: "CB_ROWID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PERMISSIONS_COMPONENT_BASE_PER_COMPONENT_REFNO",
                table: "PERMISSIONS");

            migrationBuilder.DropTable(
                name: "DISPLAY_FIELDS");

            migrationBuilder.DropIndex(
                name: "IX_PAGES_D_PD_M_REFNO",
                table: "PAGES_D");

            migrationBuilder.DropColumn(
                name: "CB_COMMAND_TYPE",
                table: "COMPONENT_BASE");

            migrationBuilder.DropColumn(
                name: "CB_DATABASE",
                table: "COMPONENT_BASE");

            migrationBuilder.RenameColumn(
                name: "PER_COMPONENT_REFNO",
                table: "PERMISSIONS",
                newName: "PER_COMP_REFNO");

            migrationBuilder.RenameIndex(
                name: "IX_PERMISSIONS_PER_COMPONENT_REFNO_PER_AUTH_CODE",
                table: "PERMISSIONS",
                newName: "IX_PERMISSIONS_PER_COMP_REFNO_PER_AUTH_CODE");

            migrationBuilder.AddColumn<string>(
                name: "PM_DATABASE",
                table: "PAGES_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PERMISSIONS_COMPONENT_BASE_PER_COMP_REFNO",
                table: "PERMISSIONS",
                column: "PER_COMP_REFNO",
                principalTable: "COMPONENT_BASE",
                principalColumn: "CB_ROWID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
