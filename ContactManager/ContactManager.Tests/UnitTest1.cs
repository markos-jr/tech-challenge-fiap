using ContactManager.API.Controllers;
using ContactManager.Application.Interfaces;
using ContactManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace ContactManager.Tests
{
    public class ContactsControllerTests
    {
        [Fact]
        public async Task GetAll_ReturnsContacts()
        {
            // Arrange
            var repositoryMock = new Mock<IContactRepository>();
            repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Contact>
            {
                new Contact { Name = "John Doe", Phone = "123456789", Email = "johndoe@example.com", DDD = "21" }
            });

            var cacheMock = new Mock<IMemoryCache>();
            var cacheEntryMock = new Mock<ICacheEntry>();
            cacheMock.Setup(m => m.CreateEntry(It.IsAny<object>())).Returns(cacheEntryMock.Object);

            var controller = new ContactsController(repositoryMock.Object, cacheMock.Object);

            // Act
            var result = await controller.GetAll(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result); // Agora esperamos pelo tipo OkObjectResult
            var contacts = Assert.IsType<List<Contact>>(okResult.Value); // Verifica se o valor é uma lista de contatos
            Assert.Single(contacts); // Confirma que há apenas um contato na lista
        }
    }
}
