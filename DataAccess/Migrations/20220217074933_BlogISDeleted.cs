using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class BlogISDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BlogCategories",
                type: "bit",
                nullable: false,
                defaultValue: false);
            migrationBuilder.AddColumn<bool>(
              name: "IsDeleted",
              table: "Blogs",
              type: "bit",
              nullable: false,
              defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2022, 2, 17, 11, 49, 32, 748, DateTimeKind.Local).AddTicks(5824));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1023",
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 2, 17, 11, 49, 32, 748, DateTimeKind.Unspecified).AddTicks(5566), new TimeSpan(0, 4, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BlogCategories");
            migrationBuilder.DropColumn(
               name: "IsDeleted",
               table: "Blogs");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishDate",
                value: new DateTime(2022, 2, 17, 11, 48, 53, 401, DateTimeKind.Local).AddTicks(9522));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "1023",
                column: "LockoutEnd",
                value: new DateTimeOffset(new DateTime(2022, 2, 17, 11, 48, 53, 401, DateTimeKind.Unspecified).AddTicks(9268), new TimeSpan(0, 4, 0, 0, 0)));
        }
    }
}
