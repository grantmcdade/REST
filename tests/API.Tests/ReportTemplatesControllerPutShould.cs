using API.Core.Dtos;
using API.Core.Models;
using API.Handlers.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace API.Tests
{
    public class ReportTemplatesControllerPutShould : IClassFixture<ReportTemplateControllerFixture>
    {
        public ReportTemplatesControllerPutShould(ReportTemplateControllerFixture fixture)
        {
            Fixture = fixture;
            SeedData.PopulateTestData(Fixture.Context);
        }

        public ReportTemplateControllerFixture Fixture { get; }

        [Fact]
        public async Task UpdateTheItem()
        {
            var item = new ReportTemplateUpdate
            {
                Id = 1001,
                Name = "Test",
                Tags = new List<string> { "Tag" }.AsEnumerable()
            };

            var result = await Fixture.Controller.PutReportTemplate(1001, item);

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
