using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ToucanTesting.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestSuites",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestSuites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestModules",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    TestSuiteId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestModules_TestSuites_TestSuiteId",
                        column: x => x.TestSuiteId,
                        principalTable: "TestSuites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestRuns",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    TestSuiteId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestRuns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestRuns_TestSuites_TestSuiteId",
                        column: x => x.TestSuiteId,
                        principalTable: "TestSuites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestCases",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    IsAutomated = table.Column<bool>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    LastTested = table.Column<DateTime>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    TestModuleId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestCases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestCases_TestModules_TestModuleId",
                        column: x => x.TestModuleId,
                        principalTable: "TestModules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TestCaseId = table.Column<long>(nullable: false),
                    TestModuleId = table.Column<long>(nullable: false),
                    TestRunId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_TestRuns_TestRunId",
                        column: x => x.TestRunId,
                        principalTable: "TestRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpectedResults",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    TestCaseId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpectedResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpectedResults_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestActions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Sequence = table.Column<int>(nullable: false),
                    TestCaseId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestActions_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestConditions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    TestCaseId = table.Column<long>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestConditions_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestIssues",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Reference = table.Column<string>(maxLength: 16, nullable: false),
                    TestCaseDescription = table.Column<string>(nullable: true),
                    TestCaseId = table.Column<long>(nullable: false),
                    TestModuleName = table.Column<string>(nullable: true),
                    TestRunName = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestIssues_TestCases_TestCaseId",
                        column: x => x.TestCaseId,
                        principalTable: "TestCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpectedResults_TestCaseId",
                table: "ExpectedResults",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestActions_TestCaseId",
                table: "TestActions",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestCases_TestModuleId",
                table: "TestCases",
                column: "TestModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TestConditions_TestCaseId",
                table: "TestConditions",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestIssues_TestCaseId",
                table: "TestIssues",
                column: "TestCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TestModules_TestSuiteId",
                table: "TestModules",
                column: "TestSuiteId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestRunId",
                table: "TestResults",
                column: "TestRunId");

            migrationBuilder.CreateIndex(
                name: "IX_TestRuns_TestSuiteId",
                table: "TestRuns",
                column: "TestSuiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpectedResults");

            migrationBuilder.DropTable(
                name: "TestActions");

            migrationBuilder.DropTable(
                name: "TestConditions");

            migrationBuilder.DropTable(
                name: "TestIssues");

            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.DropTable(
                name: "TestCases");

            migrationBuilder.DropTable(
                name: "TestRuns");

            migrationBuilder.DropTable(
                name: "TestModules");

            migrationBuilder.DropTable(
                name: "TestSuites");
        }
    }
}
