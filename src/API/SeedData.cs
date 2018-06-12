using System;
using System.Linq;
using API.Core.Models;
using API.Infrastructure;

namespace API
{
    internal class SeedData
    {
        internal static void PopulateTestData(ApplicationDbContext applicationDbContext)
        {
            var tags = new ReportTemplateTag[]
                {
                    new ReportTemplateTag { Id = 1001, Name = "Scenario" },
                    new ReportTemplateTag { Id = 1002, Name = "Count" },
                    new ReportTemplateTag { Id = 1003, Name = "Status" },
                    new ReportTemplateTag { Id = 1004, Name = "Unused" }
                };
            if (!applicationDbContext.ReportTemplateTags.Any())
            {
                applicationDbContext.ReportTemplateTags.AddRange(tags);
            }


            var reports = new ReportTemplate[] {
                new ReportTemplate {
                                        Id = 1001,
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
                    Id = 1002,
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
            if (!applicationDbContext.ReportTemplates.Any())
            {
                applicationDbContext.ReportTemplates.AddRange(reports);
            }

            if (!applicationDbContext.ReportTemplateReportTemplateTags.Any())
            {

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
            }

            applicationDbContext.SaveChanges();
        }
    }
}