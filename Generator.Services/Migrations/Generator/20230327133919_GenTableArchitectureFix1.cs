using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations.Generator
{
    public partial class GenTableArchitectureFix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CRUD_BUTTONS");

            migrationBuilder.DropIndex(
                name: "IX_PERMISSIONS_PER_COMP_REFNO",
                table: "PERMISSIONS");

            migrationBuilder.DropColumn(
                name: "BB_PAGE_REFNO",
                table: "GRID_BUTTONS");

            migrationBuilder.DropColumn(
                name: "BB_TOOLTIP",
                table: "BUTTONS_BASE");

            migrationBuilder.RenameColumn(
                name: "PER_DESC",
                table: "PERMISSIONS",
                newName: "PER_DESCRIPTION");

            migrationBuilder.RenameColumn(
                name: "PB_UPDATE",
                table: "PAGES_M",
                newName: "PM_UPDATE");

            migrationBuilder.RenameColumn(
                name: "PB_READ",
                table: "PAGES_M",
                newName: "PM_READ");

            migrationBuilder.RenameColumn(
                name: "PB_DESC",
                table: "PAGES_M",
                newName: "PM_TABLE");

            migrationBuilder.RenameColumn(
                name: "PB_DELETE",
                table: "PAGES_M",
                newName: "PM_DELETE");

            migrationBuilder.RenameColumn(
                name: "PB_CREATE",
                table: "PAGES_M",
                newName: "PM_CREATE");

            migrationBuilder.AlterColumn<string>(
                name: "PER_COMP_TYPE",
                table: "PERMISSIONS",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PER_AUTH_CODE",
                table: "PERMISSIONS",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PER_COMP_AUTH_CODE",
                table: "PERMISSIONS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PM_DATABASE",
                table: "PAGES_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CB_CODE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CB_DESCRIPTION",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CB_SQL_COMMAND",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CB_TITLE",
                table: "COMPONENT_BASE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BB_PAGE_REFNO",
                table: "BUTTONS_BASE",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSIONS_PER_COMP_REFNO_PER_AUTH_CODE",
                table: "PERMISSIONS",
                columns: new[] { "PER_COMP_REFNO", "PER_AUTH_CODE" },
                unique: true,
                filter: "[PER_AUTH_CODE] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BUTTONS_BASE_BB_PAGE_REFNO",
                table: "BUTTONS_BASE",
                column: "BB_PAGE_REFNO");

            migrationBuilder.AddForeignKey(
                name: "FK_BUTTONS_BASE_PAGES_M_BB_PAGE_REFNO",
                table: "BUTTONS_BASE",
                column: "BB_PAGE_REFNO",
                principalTable: "PAGES_M",
                principalColumn: "CB_ROWID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BUTTONS_BASE_PAGES_M_BB_PAGE_REFNO",
                table: "BUTTONS_BASE");

            migrationBuilder.DropIndex(
                name: "IX_PERMISSIONS_PER_COMP_REFNO_PER_AUTH_CODE",
                table: "PERMISSIONS");

            migrationBuilder.DropIndex(
                name: "IX_BUTTONS_BASE_BB_PAGE_REFNO",
                table: "BUTTONS_BASE");

            migrationBuilder.DropColumn(
                name: "PER_COMP_AUTH_CODE",
                table: "PERMISSIONS");

            migrationBuilder.DropColumn(
                name: "PM_DATABASE",
                table: "PAGES_M");

            migrationBuilder.DropColumn(
                name: "CB_CODE",
                table: "COMPONENT_BASE");

            migrationBuilder.DropColumn(
                name: "CB_DESCRIPTION",
                table: "COMPONENT_BASE");

            migrationBuilder.DropColumn(
                name: "CB_SQL_COMMAND",
                table: "COMPONENT_BASE");

            migrationBuilder.DropColumn(
                name: "CB_TITLE",
                table: "COMPONENT_BASE");

            migrationBuilder.DropColumn(
                name: "BB_PAGE_REFNO",
                table: "BUTTONS_BASE");

            migrationBuilder.RenameColumn(
                name: "PER_DESCRIPTION",
                table: "PERMISSIONS",
                newName: "PER_DESC");

            migrationBuilder.RenameColumn(
                name: "PM_UPDATE",
                table: "PAGES_M",
                newName: "PB_UPDATE");

            migrationBuilder.RenameColumn(
                name: "PM_TABLE",
                table: "PAGES_M",
                newName: "PB_DESC");

            migrationBuilder.RenameColumn(
                name: "PM_READ",
                table: "PAGES_M",
                newName: "PB_READ");

            migrationBuilder.RenameColumn(
                name: "PM_DELETE",
                table: "PAGES_M",
                newName: "PB_DELETE");

            migrationBuilder.RenameColumn(
                name: "PM_CREATE",
                table: "PAGES_M",
                newName: "PB_CREATE");

            migrationBuilder.AlterColumn<string>(
                name: "PER_COMP_TYPE",
                table: "PERMISSIONS",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PER_AUTH_CODE",
                table: "PERMISSIONS",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BB_PAGE_REFNO",
                table: "GRID_BUTTONS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BB_TOOLTIP",
                table: "BUTTONS_BASE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CRUD_BUTTONS",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false),
                    BB_PAGE_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CRUD_BUTTONS", x => x.CB_ROWID);
                    table.ForeignKey(
                        name: "FK_CRUD_BUTTONS_BUTTONS_BASE_CB_ROWID",
                        column: x => x.CB_ROWID,
                        principalTable: "BUTTONS_BASE",
                        principalColumn: "CB_ROWID");
                    table.ForeignKey(
                        name: "FK_CRUD_BUTTONS_PAGES_M_BB_PAGE_REFNO",
                        column: x => x.BB_PAGE_REFNO,
                        principalTable: "PAGES_M",
                        principalColumn: "CB_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSIONS_PER_COMP_REFNO",
                table: "PERMISSIONS",
                column: "PER_COMP_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_CRUD_BUTTONS_BB_PAGE_REFNO",
                table: "CRUD_BUTTONS",
                column: "BB_PAGE_REFNO");
        }
    }
}
