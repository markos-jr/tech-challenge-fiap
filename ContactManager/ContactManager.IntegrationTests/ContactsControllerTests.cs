using System.Net.Http.Json;
using ContactManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ContactManager.IntegrationTests
{
    public class ContactsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ContactsControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Then_GetAll_ShouldReturnInsertedContact()
        {
            var newContact = new Contact
            {
                Name = "Teste Integrado",
                Email = "teste@dev.com",
                Phone = "999999999",
                DDD = "21"
            };

            var postResponse = await _client.PostAsJsonAsync("/api/contacts", newContact);
            postResponse.EnsureSuccessStatusCode();

            var getResponse = await _client.GetAsync("/api/contacts");
            getResponse.EnsureSuccessStatusCode();

            var list = await getResponse.Content.ReadFromJsonAsync<List<Contact>>();
            Assert.Contains(list, c => c.Email == "teste@dev.com");
        }
    }
}
