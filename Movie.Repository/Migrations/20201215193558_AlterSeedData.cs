using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie.Repository.Migrations
{
    public partial class AlterSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateOfBirth", "LastName", "Name" },
                values: new object[] { new DateTime(1983, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hemsworth", "Chris" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Actors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DateOfBirth", "LastName", "Name" },
                values: new object[] { new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "EvansNew", "ChrisNew" });
        }
    }
}
