using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations
{
    /// <inheritdoc />
    public partial class moreflds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OM_MORE_FIELD_EIGHT",
                table: "ORDERS_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OM_MORE_FIELD_FIVE",
                table: "ORDERS_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OM_MORE_FIELD_FOUR",
                table: "ORDERS_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OM_MORE_FIELD_ONE",
                table: "ORDERS_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OM_MORE_FIELD_SEVEN",
                table: "ORDERS_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OM_MORE_FIELD_SIX",
                table: "ORDERS_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OM_MORE_FIELD_THREE",
                table: "ORDERS_M",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OM_MORE_FIELD_TWO",
                table: "ORDERS_M",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OM_MORE_FIELD_EIGHT",
                table: "ORDERS_M");

            migrationBuilder.DropColumn(
                name: "OM_MORE_FIELD_FIVE",
                table: "ORDERS_M");

            migrationBuilder.DropColumn(
                name: "OM_MORE_FIELD_FOUR",
                table: "ORDERS_M");

            migrationBuilder.DropColumn(
                name: "OM_MORE_FIELD_ONE",
                table: "ORDERS_M");

            migrationBuilder.DropColumn(
                name: "OM_MORE_FIELD_SEVEN",
                table: "ORDERS_M");

            migrationBuilder.DropColumn(
                name: "OM_MORE_FIELD_SIX",
                table: "ORDERS_M");

            migrationBuilder.DropColumn(
                name: "OM_MORE_FIELD_THREE",
                table: "ORDERS_M");

            migrationBuilder.DropColumn(
                name: "OM_MORE_FIELD_TWO",
                table: "ORDERS_M");
        }
    }
}
