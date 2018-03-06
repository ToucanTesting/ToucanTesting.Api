using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TucanTesting.Migrations
{
    public partial class AddExpectedResultsToTestCases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExpectedResults_TestCaseId",
                table: "ExpectedResults",
                column: "TestCaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpectedResults_TestCases_TestCaseId",
                table: "ExpectedResults",
                column: "TestCaseId",
                principalTable: "TestCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpectedResults_TestCases_TestCaseId",
                table: "ExpectedResults");

            migrationBuilder.DropIndex(
                name: "IX_ExpectedResults_TestCaseId",
                table: "ExpectedResults");
        }
    }
}
