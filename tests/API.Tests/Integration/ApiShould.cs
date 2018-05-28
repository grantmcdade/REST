using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests.Itegration
{
    public class ApiShould : IClassFixture<ApiApplicationFactory<Startup>>
    {
        public ApiShould(ApiApplicationFactory<Startup> fixture)
        {
            Client = fixture.CreateClient();
            // Client.BaseAddress = new Uri("https://localhost");
        }

        protected HttpClient Client { get; }

        [Fact]
        public async Task GetHomePageCorrectly()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ContainTheSwaggerUI()
        {
            // Arrange & Act
            var response = await Client.GetAsync("/swagger");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //[Fact]
        //public async Task RedirectToHttps()
        //{
        //    var response = await Client.GetAsync("http://localhost/");

        //    Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        //    Assert.Equal("https", response.Headers.Location.Scheme);
        //}
    }
}
