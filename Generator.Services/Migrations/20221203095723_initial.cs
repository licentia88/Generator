using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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

            migrationBuilder.CreateTable(
                name: "PARENT_CLASS",
                columns: table => new
                {
                    PCROWID = table.Column<int>(name: "PC_ROWID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PCDESC = table.Column<string>(name: "PC_DESC", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARENT_CLASS", x => x.PCROWID);
                });

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

            migrationBuilder.CreateTable(
                name: "TEST_TABLE",
                columns: table => new
                {
                    TTROWID = table.Column<int>(name: "TT_ROWID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TTDESC = table.Column<string>(name: "TT_DESC", type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEST_TABLE", x => x.TTROWID);
                });

            migrationBuilder.CreateTable(
                name: "CHILD_CLASS",
                columns: table => new
                {
                    CCROWID = table.Column<int>(name: "CC_ROWID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CCDESC = table.Column<string>(name: "CC_DESC", type: "nvarchar(max)", nullable: true),
                    CCPARENTREFNO = table.Column<int>(name: "CC_PARENT_REFNO", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHILD_CLASS", x => x.CCROWID);
                    table.ForeignKey(
                        name: "FK_CHILD_CLASS_PARENT_CLASS_CC_PARENT_REFNO",
                        column: x => x.CCPARENTREFNO,
                        principalTable: "PARENT_CLASS",
                        principalColumn: "PC_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHILD_CLASS_CC_PARENT_REFNO",
                table: "CHILD_CLASS",
                column: "CC_PARENT_REFNO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHILD_CLASS");

            migrationBuilder.DropTable(
                name: "COMPUTED_TABLE");

            migrationBuilder.DropTable(
                name: "STRING_TABLE");

            migrationBuilder.DropTable(
                name: "TEST_TABLE");

            migrationBuilder.DropTable(
                name: "PARENT_CLASS");
        }
    }
}
