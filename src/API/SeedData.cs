using System;
using API.Data;
using API.Data.Models;

namespace API
{
    internal class SeedData
    {
        internal static void PopulateTestData(ApplicationDbContext applicationDbContext)
        {
            var tags = new ReportTemplateTag[]
                {
                    new ReportTemplateTag { Id = 1, Name = "Scenario" },
                    new ReportTemplateTag { Id = 2, Name = "Count" },
                    new ReportTemplateTag { Id = 3, Name = "Status" },
                    new ReportTemplateTag { Id = 4, Name = "Unused" }
                };
            applicationDbContext.ReportTemplateTags.AddRange(tags);

            var reports = new ReportTemplate[] {
                new ReportTemplate {
                                        Id = 1,
                    Name = "Test Execution Result By Test Scenario",
                    Description = "The test execution result by test scenario",
                    Tags = null,
                    CreationDate = new DateTime(2018, 5, 15),
                    LastModifiedDate = new DateTime(2018, 5, 15),
                    ThumbnailImage = "/Sample-data/TestExecutionResultByTestScenario/thumbnail.png",
                    FullSizeImage = "/Sample-data/TestExecutionResultByTestScenario/image.png",
                    PdfFile = "/Sample-data/TestExecutionResultByTestScenario/TestExecutionResultByTestScenario.pdf",
                    ZipFile = "/Sample-data/TestExecutionResultByTestScenario/TestExecutionResultByTestScenario.zip",
                },
                new ReportTemplate
                {
                    Id = 2,
                    Name = "Total Count Of Defects On Each Day Sorted By Status",
                    Description = "The total count of defects on each day sorted by status",
                    Tags = null,
                    CreationDate = new DateTime(2018, 5, 15),
                    LastModifiedDate = new DateTime(2018, 5, 15),
                    ThumbnailImage = "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/thumbnail.png",
                    FullSizeImage = "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/image.png",
                    PdfFile = "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/TotalCountOfDefectsOnEachDaySortedByStatus.pdf",
                    ZipFile = "/Sample-data/TotalCountOfDefectsOnEachDaySortedByStatus/TotalCountOfDefectsOnEachDaySortedByStatus.zip",
                }
            };
            applicationDbContext.ReportTemplates.AddRange(reports);

            applicationDbContext.ReportTemplateReportTemplateTags.AddRange(new ReportTemplateReportTemplateTag[]
            {
                new ReportTemplateReportTemplateTag
                {
                    ReportTemplateId = reports[0].Id,
                    ReportTemplateTagId = tags[0].Id
                },
                new ReportTemplateReportTemplateTag
                {
                    ReportTemplateId = reports[0].Id,
                    ReportTemplateTagId = tags[2].Id
                },
                new ReportTemplateReportTemplateTag
                {
                    ReportTemplateId = reports[1].Id,
                    ReportTemplateTagId = tags[1].Id
                }
            });

            applicationDbContext.SaveChanges();
        }
    }
}