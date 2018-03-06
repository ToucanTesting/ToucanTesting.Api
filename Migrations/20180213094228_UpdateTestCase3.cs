using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TucanTesting.Migrations
{
    public partial class UpdateTestCase3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestCases_TestResults_TestResultId",
                table: "TestCases");

            migrationBuilder.DropIndex(
                name: "IX_TestCases_TestResultId",
                table: "TestCases");

            migrationBuilder.DropColumn(
                name: "TestResultId",
                table: "TestCases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TestResultId",
                table: "TestCases",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestCases_TestResultId",
                table: "TestCases",
                column: "TestResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestCases_TestResults_TestResultId",
                table: "TestCases",
                column: "TestResultId",
                principalTable: "TestResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
