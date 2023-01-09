using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations
{
    public partial class realTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    U_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    U_AGE = table.Column<int>(type: "int", nullable: false),
                    U_REGISTER_DATE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.U_ROWID);
                });

            migrationBuilder.CreateTable(
                name: "ORDERS_M",
                columns: table => new
                {
                    OM_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OM_USER_REFNO = table.Column<int>(type: "int", nullable: false),
                    OM_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS_M", x => x.OM_ROWID);
                    table.ForeignKey(
                        name: "FK_ORDERS_M_USER_OM_USER_REFNO",
                        column: x => x.OM_USER_REFNO,
                        principalTable: "USER",
                        principalColumn: "U_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORDERS_D",
                columns: table => new
                {
                    OD_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OD_M_REFNO = table.Column<int>(type: "int", nullable: false),
                    OD_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OD_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    OD_PRICE = table.Column<decimal>(type: "decimal(19,2)", precision: 19, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS_D", x => x.OD_ROWID);
                    table.ForeignKey(
                        name: "FK_ORDERS_D_ORDERS_M_OD_M_REFNO",
                        column: x => x.OD_M_REFNO,
                        principalTable: "ORDERS_M",
                        principalColumn: "OM_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_D_OD_M_REFNO",
                table: "ORDERS_D",
                column: "OD_M_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_M_OM_USER_REFNO",
                table: "ORDERS_M",
                column: "OM_USER_REFNO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDERS_D");

            migrationBuilder.DropTable(
                name: "ORDERS_M");

            migrationBuilder.DropTable(
                name: "USER");
        }
    }
}
