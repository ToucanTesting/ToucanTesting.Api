using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TucanTesting.Migrations
{
    public partial class IncludeTestResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestRunId",
                table: "TestResults",
                column: "TestRunId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestResults_TestRuns_TestRunId",
                table: "TestResults",
                column: "TestRunId",
                principalTable: "TestRuns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestResults_TestRuns_TestRunId",
                table: "TestResults");

            migrationBuilder.DropIndex(
                name: "IX_TestResults_TestRunId",
                table: "TestResults");
        }
    }
}
