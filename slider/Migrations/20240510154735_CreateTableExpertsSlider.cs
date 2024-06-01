using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace slider.Migrations
{
    public partial class CreateTableExpertsSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Experts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 5, 10, 19, 47, 35, 470, DateTimeKind.Local).AddTicks(8641));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 5, 10, 19, 47, 35, 470, DateTimeKind.Local).AddTicks(8659));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 5, 10, 19, 47, 35, 470, DateTimeKind.Local).AddTicks(8660));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Experts");

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 5, 7, 17, 7, 6, 907, DateTimeKind.Local).AddTicks(1960));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 5, 7, 17, 7, 6, 907, DateTimeKind.Local).AddTicks(1976));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 5, 7, 17, 7, 6, 907, DateTimeKind.Local).AddTicks(1977));
        }
    }
}
