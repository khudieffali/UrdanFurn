using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class BlogPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BlogPhoto",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2022, 2, 17, 10, 56, 56, 402, DateTimeKind.Local).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1023",
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 2, 17, 10, 56, 56, 402, DateTimeKind.Unspecified).AddTicks(4016), new TimeSpan(0, 4, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogPhoto",
                table: "Blogs");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2022, 2, 17, 10, 54, 42, 761, DateTimeKind.Local).AddTicks(5678));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1023",
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 2, 17, 10, 54, 42, 761, DateTimeKind.Unspecified).AddTicks(5407), new TimeSpan(0, 4, 0, 0, 0)));
        }
    }
}
