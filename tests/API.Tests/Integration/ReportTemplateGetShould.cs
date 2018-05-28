using API.Data.Models;
using API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Itegration
{
    public class ReportTemplateGetShould : IClassFixture<ApiApplicationFactory<Startup>>
    {
        public ReportTemplateGetShould(ApiApplicationFactory<Startup> fixture)
        {
            Client = fixture.CreateClient();
            Client.BaseAddress = new Uri("https://localhost");
        }

        protected HttpClient Client { get; }

        [Fact]
        public async Task ReturnAllReportTemplates()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/api/reporttemplates");

            var content = await response.Content.ReadAsStringAsync();

            var serializer = new JsonSerializer();
            var reportTemplates = serializer.Deserialize<List<ReportTemplateDto>>(new JTokenReader(JToken.Parse(content)));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(reportTemplates);
            Assert.Collection(reportTemplates,
                new Action<ReportTemplateDto>(rt =>
                Assert.Equal("Test Execution Result By Test Scenario", rt.Name)),
                new Action<ReportTemplateDto>(rt =>
                Assert.Equal("Total Count Of Defects On Each Day Sorted By Status", rt.Name)));
        }

        [Fact]
        public async Task ReturnTheCorrectReportTemplate()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/api/reporttemplates/1");

            var content = await response.Content.ReadAsStringAsync();

            var serializer = new JsonSerializer();
            var reportTemplate = serializer.Deserialize<ReportTemplateDto>(new JTokenReader(JToken.Parse(content)));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(reportTemplate);
            Assert.Equal("Test Execution Result By Test Scenario", reportTemplate.Name);
            Assert.Collection(reportTemplate.Tags, 
                new Action<string>(s => Assert.Equal("Scenario", s)),
                new Action<string>(s => Assert.Equal("Status", s)));
        }
    }
}
