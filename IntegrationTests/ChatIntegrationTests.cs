using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Web.Controllers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Infrastructure.Models;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Net;

namespace IntegrationTests
{
    public class ChatIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private WebApplicationFactory<Program> factory;

        public ChatIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task GetAllChats_ReturnsOk()
        {
            var client = factory.CreateClient();
            var requestUri = "/api/chat"; 
            var response = await client.GetAsync(requestUri);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //insert any valid chatId before running the next test
        /*
        [Fact]
        public async Task GetChatById_WithValidId_ReturnsOk()
        {
            var client = factory.CreateClient();
            int validChatId = 2; 
            var requestUri = $"/api/chat/{validChatId}";
            var response = await client.GetAsync(requestUri);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        */
    }
}