using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class stringCusID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2022, 2, 15, 21, 2, 41, 729, DateTimeKind.Local).AddTicks(4810));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1023",
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 2, 15, 21, 2, 41, 729, DateTimeKind.Unspecified).AddTicks(4591), new TimeSpan(0, 4, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CustomerID",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2022, 2, 15, 20, 56, 26, 614, DateTimeKind.Local).AddTicks(4103));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1023",
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 2, 15, 20, 56, 26, 614, DateTimeKind.Unspecified).AddTicks(3835), new TimeSpan(0, 4, 0, 0, 0)));
        }
    }
}
