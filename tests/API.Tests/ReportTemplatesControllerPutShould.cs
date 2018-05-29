using API.Core.Dtos;
using API.Core.Models;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class ReportTemplatesControllerPutShould : IClassFixture<ReportTemplateControllerFixture>
    {
        public ReportTemplatesControllerPutShould(ReportTemplateControllerFixture fixture)
        {
            Fixture = fixture;
        }

        public ReportTemplateControllerFixture Fixture { get; }

        [Fact]
        public async Task UpdateTheItem()
        {
            var item = new ReportTemplateDto
            {
                Id = 1,
                Name = "Test"
            };

            var result = await Fixture.Controller.PutReportTemplate(1, item);

            Assert.Contains<ReportTemplate>(Fixture.Context.ReportTemplates, rt => rt.Name == item.Name);
        }
    }
}
