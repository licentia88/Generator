using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Generator.Services.Migrations
{
    public partial class datefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TT_DATE",
                table: "TEST_TABLE",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TT_NULLABLE_DATE",
                table: "TEST_TABLE",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TT_DATE",
                table: "TEST_TABLE");

            migrationBuilder.DropColumn(
                name: "TT_NULLABLE_DATE",
                table: "TEST_TABLE");
        }
    }
}
