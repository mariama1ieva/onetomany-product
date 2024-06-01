using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace slider.Migrations
{
    public partial class CreateTableInstagramSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 5, 10, 21, 1, 6, 449, DateTimeKind.Local).AddTicks(8712));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 5, 10, 21, 1, 6, 449, DateTimeKind.Local).AddTicks(8728));

            migrationBuilder.UpdateData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 5, 10, 21, 1, 6, 449, DateTimeKind.Local).AddTicks(8730));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
