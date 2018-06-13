using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Infrastructure.Data.Migrations
{
    public partial class AppTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "API_ReportTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    ThumbnailImage = table.Column<string>(nullable: true),
                    FullSizeImage = table.Column<string>(nullable: true),
                    PdfFile = table.Column<string>(nullable: true),
                    ZipFile = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_ReportTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_ReportTemplateTags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_ReportTemplateTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "API_ReportTemplateReportTemplateTags",
                columns: table => new
                {
                    ReportTemplateId = table.Column<int>(nullable: false),
                    ReportTemplateTagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API_ReportTemplateReportTemplateTags", x => new { x.ReportTemplateId, x.ReportTemplateTagId });
                    table.ForeignKey(
                        name: "FK_API_ReportTemplateReportTemplateTags_API_ReportTemplates_ReportTemplateId",
                        column: x => x.ReportTemplateId,
                        principalTable: "API_ReportTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_API_ReportTemplateReportTemplateTags_API_ReportTemplateTags_ReportTemplateTagId",
                        column: x => x.ReportTemplateTagId,
                        principalTable: "API_ReportTemplateTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "API_ReportTemplateTags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Scenario" },
                    { 2, "Count" },
                    { 3, "Status" },
                    { 4, "Unused" }
                });

            migrationBuilder.InsertData(
                table: "API_ReportTemplates",
                columns: new[] { "Id", "CreationDate", "Description", "FullSizeImage", "LastModifiedDate", "Name", "PdfFile", "ThumbnailImage", "ZipFile" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The test execution result by test scenario", "/Sample-data/TestExecutionResultByTestScenario/image.png", new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test Execution Result By Test Scenario", "/Sample-data/TestExecutionResultByTestScenario/TestExecutionResultByTestScenario.pdf", "/Sample-data/TestExecutionResultByTestScenario/thumbnail.png", "/Sample-data/TestExecutionResultByTestScenario/TestExecutionResultByTestScenario.zip" },
                    { 2, new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "The total count of defects on each day sorted by status", "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/image.png", new DateTime(2018, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Total Count Of Defects On Each Day Sorted By Status", "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/TotalCountOfDefectsOnEachDaySortedByStatus.pdf", "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/thumbnail.png", "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/TotalCountOfDefectsOnEachDaySortedByStatus.zip" }
                });

            migrationBuilder.InsertData(
                table: "API_ReportTemplateReportTemplateTags",
                columns: new[] { "ReportTemplateId", "ReportTemplateTagId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "API_ReportTemplateReportTemplateTags",
                columns: new[] { "ReportTemplateId", "ReportTemplateTagId" },
                values: new object[] { 2, 2 });

            migrationBuilder.InsertData(
                table: "API_ReportTemplateReportTemplateTags",
                columns: new[] { "ReportTemplateId", "ReportTemplateTagId" },
                values: new object[] { 1, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_API_ReportTemplateReportTemplateTags_ReportTemplateTagId",
                table: "API_ReportTemplateReportTemplateTags",
                column: "ReportTemplateTagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "API_ReportTemplateReportTemplateTags");

            migrationBuilder.DropTable(
                name: "API_ReportTemplates");

            migrationBuilder.DropTable(
                name: "API_ReportTemplateTags");
        }
    }
}
