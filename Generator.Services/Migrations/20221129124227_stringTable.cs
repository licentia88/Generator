using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Service.Migrations
{
    /// <inheritdoc />
    public partial class stringTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STRING_TABLE",
                columns: table => new
                {
                    CTROWID = table.Column<string>(name: "CT_ROWID", type: "nvarchar(450)", nullable: false),
                    CTDESC = table.Column<string>(name: "CT_DESC", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STRING_TABLE", x => x.CTROWID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STRING_TABLE");
        }
    }
}
