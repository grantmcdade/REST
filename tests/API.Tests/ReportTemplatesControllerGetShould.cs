using API.Core.Dtos;
using API.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ReportTemplatesControllerGetShould : IClassFixture<ReportTemplateControllerFixture>
    {
        public ReportTemplatesControllerGetShould(ReportTemplateControllerFixture fixture)
        {
            Fixture = fixture;
            SeedData.PopulateTestData(Fixture.Context);
        }

        private ReportTemplateControllerFixture Fixture { get; }

        [Fact]
        public async Task ReturnAllReportTemplates()
        {
            var result = Assert.IsType<ActionResult<IEnumerable<ReportTemplateDto>>>(await Fixture.Controller.GetReportTemplates());
            var jsonResult = Assert.IsType<JsonResult>(result.Result);
            var json = jsonResult.Value as IEnumerable<ReportTemplateDto>;

            Assert.Collection(json,
                new Action<ReportTemplateDto>(rt =>
                {
                    Assert.Equal("Test Execution Result By Test Scenario", rt.Name);
                    Assert.Collection(rt.Tags,
                        new Action<string>(s => Assert.Equal("Scenario", s)),
                        new Action<string>(s => Assert.Equal("Status", s)));
                }), new Action<ReportTemplateDto>(rt =>
                {
                    Assert.Equal("Total Count Of Defects On Each Day Sorted By Status", rt.Name);
                    Assert.Collection(rt.Tags,
                        new Action<string>(s => Assert.Equal("Count", s)));
                }));
        }

        [Fact]
        public async Task ReturnTheCorrectReportTemplate()
        {
            var result = Assert.IsType<ActionResult<ReportTemplateDto>>(await Fixture.Controller.GetReportTemplate(1001));
            var jsonResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<ReportTemplateDto>(jsonResult.Value);

            Assert.Equal("Test Execution Result By Test Scenario", dto.Name);
            Assert.Collection(dto.Tags,
                new Action<string>(s => Assert.Equal("Scenario", s)),
                new Action<string>(s => Assert.Equal("Status", s)));
        }
    }
}
