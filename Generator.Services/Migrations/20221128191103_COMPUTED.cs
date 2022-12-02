using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Service.Migrations
{
    /// <inheritdoc />
    public partial class COMPUTED : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMPUTED_TABLE",
                columns: table => new
                {
                    CTROWID = table.Column<int>(name: "CT_ROWID", type: "int", nullable: false),
                    CTDESC = table.Column<string>(name: "CT_DESC", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPUTED_TABLE", x => x.CTROWID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COMPUTED_TABLE");
        }
    }
}
