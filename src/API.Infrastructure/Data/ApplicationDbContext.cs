using System;
using System.Collections.Generic;
using System.Text;
using API.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ReportTemplateReportTemplateTag>()
                .HasKey(rtti => new { rtti.ReportTemplateId, rtti.ReportTemplateTagId });

            builder.Entity<ReportTemplateReportTemplateTag>()
                .HasOne(rtti => rtti.ReportTemplate)
                .WithMany(rtti => rtti.Tags)
                .HasForeignKey(rtti => rtti.ReportTemplateId);

            builder.Entity<ReportTemplateReportTemplateTag>()
                .HasOne(rtti => rtti.ReportTemplateTag)
                .WithMany(rtt => rtt.Reports)
                .HasForeignKey(rtti => rtti.ReportTemplateTagId);

            var tags = new ReportTemplateTag[]
                {
                    new ReportTemplateTag { Id = 1, Name = "Scenario" },
                    new ReportTemplateTag { Id = 2, Name = "Count" },
                    new ReportTemplateTag { Id = 3, Name = "Status" },
                    new ReportTemplateTag { Id = 4, Name = "Unused" }
                };
            builder.Entity<ReportTemplateTag>().HasData(tags);

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
            builder.Entity<ReportTemplate>().HasData(reports);

            builder.Entity<ReportTemplateReportTemplateTag>().HasData(new ReportTemplateReportTemplateTag[]
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

            foreach (var item in builder.Model.GetEntityTypes())
            {
                item.Relational().TableName =
                    $"API_{ item.Relational().TableName }";
            }
        }

        public DbSet<ReportTemplate> ReportTemplates { get; set; }
        public DbSet<ReportTemplateTag> ReportTemplateTags { get; set; }
        public DbSet<ReportTemplateReportTemplateTag> ReportTemplateReportTemplateTags { get; set; }
    }
}
