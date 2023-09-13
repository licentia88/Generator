using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations.Test
{
    /// <inheritdoc />
    public partial class gendertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMPUTED_TABLE",
                columns: table => new
                {
                    CT_ROWID = table.Column<int>(type: "int", nullable: false),
                    CT_DESC = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPUTED_TABLE", x => x.CT_ROWID);
                });

            migrationBuilder.CreateTable(
                name: "GENDER",
                columns: table => new
                {
                    GEN_CODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GEN_DESC = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENDER", x => x.GEN_CODE);
                });

            migrationBuilder.CreateTable(
                name: "STRING_TABLE",
                columns: table => new
                {
                    CT_ROWID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CT_DESC = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STRING_TABLE", x => x.CT_ROWID);
                });

            migrationBuilder.CreateTable(
                name: "TEST_TABLE",
                columns: table => new
                {
                    TT_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TT_DESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TT_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TT_NULLABLE_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TT_BOOLEAN = table.Column<bool>(type: "bit", nullable: false),
                    TT_DEFAULT_VALUE_STRING = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TT_STRING_TABLE_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TEST_TABLE", x => x.TT_ROWID);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    U_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    U_LASTNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    U_AGE = table.Column<int>(type: "int", nullable: false),
                    U_REGISTER_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    U_IS_MARRIED = table.Column<bool>(type: "bit", nullable: false),
                    U_GEN_CODE = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.U_ROWID);
                    table.ForeignKey(
                        name: "FK_USER_GENDER_U_GEN_CODE",
                        column: x => x.U_GEN_CODE,
                        principalTable: "GENDER",
                        principalColumn: "GEN_CODE");
                });

            migrationBuilder.CreateTable(
                name: "PARENT_CLASS",
                columns: table => new
                {
                    PC_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PC_DESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PC_STRING_TABLE_CODE = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARENT_CLASS", x => x.PC_ROWID);
                    table.ForeignKey(
                        name: "FK_PARENT_CLASS_STRING_TABLE_PC_STRING_TABLE_CODE",
                        column: x => x.PC_STRING_TABLE_CODE,
                        principalTable: "STRING_TABLE",
                        principalColumn: "CT_ROWID");
                });

            migrationBuilder.CreateTable(
                name: "ORDERS_M",
                columns: table => new
                {
                    OM_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OM_USER_REFNO = table.Column<int>(type: "int", nullable: false),
                    OM_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OM_MORE_FIELD_ONE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OM_MORE_FIELD_TWO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OM_MORE_FIELD_THREE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OM_MORE_FIELD_FOUR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OM_MORE_FIELD_FIVE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OM_MORE_FIELD_SIX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OM_MORE_FIELD_SEVEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OM_MORE_FIELD_EIGHT = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "CHILD_CLASS",
                columns: table => new
                {
                    CC_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CC_DESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CC_PARENT_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHILD_CLASS", x => x.CC_ROWID);
                    table.ForeignKey(
                        name: "FK_CHILD_CLASS_PARENT_CLASS_CC_PARENT_REFNO",
                        column: x => x.CC_PARENT_REFNO,
                        principalTable: "PARENT_CLASS",
                        principalColumn: "PC_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORDERS_D",
                columns: table => new
                {
                    OD_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OD_M_REFNO = table.Column<int>(type: "int", nullable: false),
                    OD_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "IX_CHILD_CLASS_CC_PARENT_REFNO",
                table: "CHILD_CLASS",
                column: "CC_PARENT_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_D_OD_M_REFNO",
                table: "ORDERS_D",
                column: "OD_M_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_M_OM_USER_REFNO",
                table: "ORDERS_M",
                column: "OM_USER_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_PARENT_CLASS_PC_STRING_TABLE_CODE",
                table: "PARENT_CLASS",
                column: "PC_STRING_TABLE_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_USER_U_GEN_CODE",
                table: "USER",
                column: "U_GEN_CODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHILD_CLASS");

            migrationBuilder.DropTable(
                name: "COMPUTED_TABLE");

            migrationBuilder.DropTable(
                name: "ORDERS_D");

            migrationBuilder.DropTable(
                name: "TEST_TABLE");

            migrationBuilder.DropTable(
                name: "PARENT_CLASS");

            migrationBuilder.DropTable(
                name: "ORDERS_M");

            migrationBuilder.DropTable(
                name: "STRING_TABLE");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "GENDER");
        }
    }
}
