using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Service.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TEST_TABLE",
                columns: table => new
                {
                    TTROWID = table.Column<int>(name: "TT_ROWID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TTDESC = table.Column<string>(name: "TT_DESC", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEST_TABLE", x => x.TTROWID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TEST_TABLE");
        }
    }
}
