using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ToucanTesting.Migrations
{
    public partial class UpdateTestCaseMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BugId",
                table: "TestCases",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTested",
                table: "TestCases",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BugId",
                table: "TestCases");

            migrationBuilder.DropColumn(
                name: "LastTested",
                table: "TestCases");
        }
    }
}
