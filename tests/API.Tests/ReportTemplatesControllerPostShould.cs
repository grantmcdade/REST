using API.Core.Models;
using API.Handlers.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ReportTemplatesControllerPostShould : IClassFixture<ReportTemplateControllerFixture>
    {
        public ReportTemplatesControllerPostShould(ReportTemplateControllerFixture fixture)
        {
            Fixture = fixture;
        }

        private ReportTemplateControllerFixture Fixture { get; }

        [Fact]
        public async Task CreateTheItem()
        {
            var item = new ReportTemplateCreate
            {
                Name = "Test",
                Description = "Test",
                Tags = new List<string> { "Tag" }.AsEnumerable()
            };

            var result = await Fixture.Controller.PostReportTemplate(item);
            var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);

            Assert.Contains(Fixture.Context.ReportTemplates, rt => rt.Name == item.Name);
            Assert.Collection(Fixture.Context.ReportTemplates
                .Include(rt => rt.Tags)
                .ThenInclude(rtrtt => rtrtt.ReportTemplateTag)
                .First()
                .Tags,
                new Action<ReportTemplateReportTemplateTag>(tag => Assert.Equal("Tag", tag.ReportTemplateTag.Name)));
        }

    }
}
