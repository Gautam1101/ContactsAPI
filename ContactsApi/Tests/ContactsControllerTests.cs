using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ContactsApi.Controllers;
using ContactsApi.Services;
using ContactsApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactsApi.Tests
{
    public class ContactsControllerTests
    {
        [Fact]
        public void GetContacts_ReturnsOkResult_WithContacts()
        {
            // Arrange
            var mockService = new Mock<ContactService>(MockBehavior.Strict);
            mockService.Setup(s => s.GetContacts(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                       .Returns(GetSampleContacts());
            mockService.Setup(s => s.GetTotalCount(It.IsAny<string>()))
                       .Returns(3);

            var controller = new ContactsController(mockService.Object);

            // Act
            var result = controller.GetContacts(1, 10, "").Result as OkObjectResult;

            // Assert
            var response = Assert.IsType<dynamic>(result.Value);
            var contacts = Assert.IsType<List<Contact>>(response.Contacts);
            Assert.Equal(3, contacts.Count);
            Assert.Equal(1, response.TotalPages);
        }

        [Fact]
        public void GetContact_ReturnsOkResult_WithContact()
        {
            // Arrange
            var mockService = new Mock<ContactService>();
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            mockService.Setup(s => s.GetContactById(1)).Returns(contact);

            var controller = new ContactsController(mockService.Object);

            // Act
            var result = controller.GetContact(1).Result as OkObjectResult;

            // Assert
            var returnedContact = Assert.IsType<Contact>(result.Value);
            Assert.Equal("John", returnedContact.FirstName);
        }

        [Fact]
        public void PostContact_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mockService = new Mock<ContactService>();
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            mockService.Setup(s => s.AddContact(contact));

            var controller = new ContactsController(mockService.Object);

            // Act
            var result = controller.PostContact(contact).Result as CreatedAtActionResult;

            // Assert
            var returnedContact = Assert.IsType<Contact>(result.Value);
            Assert.Equal(contact, returnedContact);
            Assert.Equal("GetContact", result.ActionName);
        }

        [Fact]
        public void PutContact_ReturnsNoContent()
        {
            // Arrange
            var mockService = new Mock<ContactService>();
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            mockService.Setup(s => s.UpdateContact(contact));

            var controller = new ContactsController(mockService.Object);

            // Act
            var result = controller.PutContact(1, contact) as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteContact_ReturnsNoContent()
        {
            // Arrange
            var mockService = new Mock<ContactService>();
            mockService.Setup(s => s.DeleteContact(1));

            var controller = new ContactsController(mockService.Object);

            // Act
            var result = controller.DeleteContact(1) as NoContentResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        private IEnumerable<Contact> GetSampleContacts()
        {
            return new List<Contact>
            {
                new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Contact { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" },
                new Contact { Id = 3, FirstName = "Sam", LastName = "Smith", Email = "sam.smith@example.com" },
            };
        }
    }
}
