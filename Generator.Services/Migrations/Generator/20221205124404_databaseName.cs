using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations.Generator
{
    public partial class databaseName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COMPONENT_DATABASE_COMP_DATABASE",
                table: "COMPONENT");

            migrationBuilder.DropTable(
                name: "DATABASE");

            migrationBuilder.CreateTable(
                name: "DATABASES",
                columns: table => new
                {
                    DatabaseIdentifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATABASES", x => x.DatabaseIdentifier);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_COMPONENT_DATABASES_COMP_DATABASE",
                table: "COMPONENT",
                column: "COMP_DATABASE",
                principalTable: "DATABASES",
                principalColumn: "DatabaseIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_COMPONENT_DATABASES_COMP_DATABASE",
                table: "COMPONENT");

            migrationBuilder.DropTable(
                name: "DATABASES");

            migrationBuilder.CreateTable(
                name: "DATABASE",
                columns: table => new
                {
                    DatabaseIdentifier = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DATABASE", x => x.DatabaseIdentifier);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_COMPONENT_DATABASE_COMP_DATABASE",
                table: "COMPONENT",
                column: "COMP_DATABASE",
                principalTable: "DATABASE",
                principalColumn: "DatabaseIdentifier");
        }
    }
}
