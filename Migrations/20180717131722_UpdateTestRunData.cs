using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ToucanTesting.Migrations
{
    public partial class UpdateTestRunData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestCaseDescription",
                table: "TestIssues");

            migrationBuilder.DropColumn(
                name: "TestModuleName",
                table: "TestIssues");

            migrationBuilder.DropColumn(
                name: "TestRunName",
                table: "TestIssues");

            migrationBuilder.AddColumn<long>(
                name: "TestRunId",
                table: "TestIssues",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestIssues_TestRunId",
                table: "TestIssues",
                column: "TestRunId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestIssues_TestRuns_TestRunId",
                table: "TestIssues",
                column: "TestRunId",
                principalTable: "TestRuns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestIssues_TestRuns_TestRunId",
                table: "TestIssues");

            migrationBuilder.DropIndex(
                name: "IX_TestIssues_TestRunId",
                table: "TestIssues");

            migrationBuilder.DropColumn(
                name: "TestRunId",
                table: "TestIssues");

            migrationBuilder.AddColumn<string>(
                name: "TestCaseDescription",
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
    }
}
