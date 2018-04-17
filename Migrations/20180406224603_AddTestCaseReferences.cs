using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ToucanTesting.Migrations
{
    public partial class AddTestCaseReferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestCaseName",
                table: "TestIssues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestModuleName",
                table: "TestIssues",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TestRunName",
                table: "TestIssues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestCaseName",
                table: "TestIssues");

            migrationBuilder.DropColumn(
                name: "TestModuleName",
                table: "TestIssues");

            migrationBuilder.DropColumn(
                name: "TestRunName",
                table: "TestIssues");
        }
    }
}
