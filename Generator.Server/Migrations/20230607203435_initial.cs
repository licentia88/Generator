using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Server.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUTH_BASE",
                columns: table => new
                {
                    AUTH_CODE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AUTH_DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AUTH_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTH_BASE", x => x.AUTH_CODE);
                });

            migrationBuilder.CreateTable(
                name: "COMPONENTS_BASE",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CB_TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CB_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CB_IDENTIFIER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CB_DATABASE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CB_QUERY_OR_METHOD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CB_COMMAND_TYPE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPONENTS_BASE", x => x.CB_ROWID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    U_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    U_USERNAME = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.U_ROWID);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    AUTH_CODE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.AUTH_CODE);
                    table.ForeignKey(
                        name: "FK_ROLES_AUTH_BASE_AUTH_CODE",
                        column: x => x.AUTH_CODE,
                        principalTable: "AUTH_BASE",
                        principalColumn: "AUTH_CODE");
                });

            migrationBuilder.CreateTable(
                name: "GRID_M",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false),
                    GB_EDIT_MODE = table.Column<int>(type: "int", nullable: false),
                    GB_EDIT_TRIGGER = table.Column<int>(type: "int", nullable: false),
                    GB_MAX_WIDTH = table.Column<int>(type: "int", nullable: false),
                    GB_DENSE = table.Column<bool>(type: "bit", nullable: false),
                    GB_ROWS_PER_PAGE = table.Column<int>(type: "int", nullable: false),
                    GB_ENABLE_SORTING = table.Column<bool>(type: "bit", nullable: false),
                    GB_ENABLE_FILTERING = table.Column<bool>(type: "bit", nullable: false),
                    GB_STRIPED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRID_M", x => x.CB_ROWID);
                    table.ForeignKey(
                        name: "FK_GRID_M_COMPONENTS_BASE_CB_ROWID",
                        column: x => x.CB_ROWID,
                        principalTable: "COMPONENTS_BASE",
                        principalColumn: "CB_ROWID");
                });

            migrationBuilder.CreateTable(
                name: "PERMISSIONS",
                columns: table => new
                {
                    AUTH_CODE = table.Column<int>(type: "int", nullable: false),
                    PER_COMPONENT_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSIONS", x => x.AUTH_CODE);
                    table.ForeignKey(
                        name: "FK_PERMISSIONS_AUTH_BASE_AUTH_CODE",
                        column: x => x.AUTH_CODE,
                        principalTable: "AUTH_BASE",
                        principalColumn: "AUTH_CODE");
                    table.ForeignKey(
                        name: "FK_PERMISSIONS_COMPONENTS_BASE_PER_COMPONENT_REFNO",
                        column: x => x.PER_COMPONENT_REFNO,
                        principalTable: "COMPONENTS_BASE",
                        principalColumn: "CB_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USER_AUTHORIZATIONS",
                columns: table => new
                {
                    UA_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UA_USER_REFNO = table.Column<int>(type: "int", nullable: false),
                    UA_AUTH_CODE = table.Column<int>(type: "int", nullable: true),
                    USERSU_ROWID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_AUTHORIZATIONS", x => x.UA_ROWID);
                    table.ForeignKey(
                        name: "FK_USER_AUTHORIZATIONS_AUTH_BASE_UA_AUTH_CODE",
                        column: x => x.UA_AUTH_CODE,
                        principalTable: "AUTH_BASE",
                        principalColumn: "AUTH_CODE");
                    table.ForeignKey(
                        name: "FK_USER_AUTHORIZATIONS_USERS_USERSU_ROWID",
                        column: x => x.USERSU_ROWID,
                        principalTable: "USERS",
                        principalColumn: "U_ROWID");
                });

            migrationBuilder.CreateTable(
                name: "GRID_D",
                columns: table => new
                {
                    CB_ROWID = table.Column<int>(type: "int", nullable: false),
                    GD_M_REFNO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRID_D", x => x.CB_ROWID);
                    table.ForeignKey(
                        name: "FK_GRID_D_GRID_M_CB_ROWID",
                        column: x => x.CB_ROWID,
                        principalTable: "GRID_M",
                        principalColumn: "CB_ROWID");
                    table.ForeignKey(
                        name: "FK_GRID_D_GRID_M_GD_M_REFNO",
                        column: x => x.GD_M_REFNO,
                        principalTable: "GRID_M",
                        principalColumn: "CB_ROWID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ROLES_D",
                columns: table => new
                {
                    RD_ROWID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RD_M_REFNO = table.Column<int>(type: "int", nullable: false),
                    RD_PERMISSION_REFNO = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES_D", x => x.RD_ROWID);
                    table.ForeignKey(
                        name: "FK_ROLES_D_PERMISSIONS_RD_PERMISSION_REFNO",
                        column: x => x.RD_PERMISSION_REFNO,
                        principalTable: "PERMISSIONS",
                        principalColumn: "AUTH_CODE");
                    table.ForeignKey(
                        name: "FK_ROLES_D_ROLES_RD_M_REFNO",
                        column: x => x.RD_M_REFNO,
                        principalTable: "ROLES",
                        principalColumn: "AUTH_CODE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GRID_D_GD_M_REFNO",
                table: "GRID_D",
                column: "GD_M_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSIONS_PER_COMPONENT_REFNO",
                table: "PERMISSIONS",
                column: "PER_COMPONENT_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_ROLES_D_RD_M_REFNO",
                table: "ROLES_D",
                column: "RD_M_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_ROLES_D_RD_PERMISSION_REFNO",
                table: "ROLES_D",
                column: "RD_PERMISSION_REFNO");

            migrationBuilder.CreateIndex(
                name: "IX_USER_AUTHORIZATIONS_UA_AUTH_CODE",
                table: "USER_AUTHORIZATIONS",
                column: "UA_AUTH_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_USER_AUTHORIZATIONS_USERSU_ROWID",
                table: "USER_AUTHORIZATIONS",
                column: "USERSU_ROWID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GRID_D");

            migrationBuilder.DropTable(
                name: "ROLES_D");

            migrationBuilder.DropTable(
                name: "USER_AUTHORIZATIONS");

            migrationBuilder.DropTable(
                name: "GRID_M");

            migrationBuilder.DropTable(
                name: "PERMISSIONS");

            migrationBuilder.DropTable(
                name: "ROLES");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "COMPONENTS_BASE");

            migrationBuilder.DropTable(
                name: "AUTH_BASE");
        }
    }
}
