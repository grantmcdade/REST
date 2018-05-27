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

namespace API.Tests
{
    public class ReportTemplateControllerGetShould : IClassFixture<ApiApplicationFactory<Startup>>
    {
        public ReportTemplateControllerGetShould(ApiApplicationFactory<Startup> fixture)
        {
            Client = fixture.CreateClient();
            Client.BaseAddress = new Uri("https://localhost");
        }

        protected HttpClient Client { get; }

        [Fact]
        public async Task GetHomePage()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReturnAllReports()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/api/reporttemplates");

            var content = await response.Content.ReadAsStringAsync();

            var serializer = new JsonSerializer();
            var rts = serializer.Deserialize<List<ReportTemplateDto>>(new JTokenReader(JToken.Parse(content)));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, rts.Count);
        }
    }
}
