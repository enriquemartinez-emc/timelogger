using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace TimeLogger.Api.IntegrationTests
{
    public class TimeLogsTests : IClassFixture<WebApplicationFactory<Timelogger.Api.Startup>>
    {
        private readonly WebApplicationFactory<Timelogger.Api.Startup> _factory;

        public TimeLogsTests(WebApplicationFactory<Timelogger.Api.Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task Create_New_TimeLog()
        {
            // Arrange
            var client = _factory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "api/1/timelogs")
            {
                Content = new StringContent(JsonSerializer.Serialize(new
                {
                    description = "Initial Interviews",
                    duration = 50
                }), Encoding.UTF8, "application/json")
            };

            // Act
            var response = await client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
