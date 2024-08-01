using Xunit;
using Moq;
using ContactsApi.Models;
using ContactsApi.Repositories;
using ContactsApi.Services;
using System.Collections.Generic;

namespace ContactsApi.Tests
{
    public class ContactServiceTests
    {
        [Fact]
        public void GetContacts_ReturnsContacts_FromRepository()
        {
            // Arrange
            var mockRepo = new Mock<ContactRepository>();
            var contacts = GetSampleContacts();
            mockRepo.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                    .Returns(contacts);
            var service = new ContactService(mockRepo.Object);

            // Act
            var result = service.GetContacts(1, 10, "");

            // Assert
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetTotalCount_ReturnsTotalCount_FromRepository()
        {
            // Arrange
            var mockRepo = new Mock<ContactRepository>();
            mockRepo.Setup(repo => repo.GetTotalCount(It.IsAny<string>()))
                    .Returns(3);
            var service = new ContactService(mockRepo.Object);

            // Act
            var result = service.GetTotalCount("");

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void GetContactById_ReturnsContact_FromRepository()
        {
            // Arrange
            var mockRepo = new Mock<ContactRepository>();
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            mockRepo.Setup(repo => repo.GetById(1)).Returns(contact);
            var service = new ContactService(mockRepo.Object);

            // Act
            var result = service.GetContactById(1);

            // Assert
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public void AddContact_CallsRepositoryAdd()
        {
            // Arrange
            var mockRepo = new Mock<ContactRepository>();
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var service = new ContactService(mockRepo.Object);

            // Act
            service.AddContact(contact);

            // Assert
            mockRepo.Verify(repo => repo.Add(contact), Times.Once);
        }

        [Fact]
        public void UpdateContact_CallsRepositoryUpdate()
        {
            // Arrange
            var mockRepo = new Mock<ContactRepository>();
            var contact = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            var service = new ContactService(mockRepo.Object);

            // Act
            service.UpdateContact(contact);

            // Assert
            mockRepo.Verify(repo => repo.Update(contact), Times.Once);
        }

        [Fact]
        public void DeleteContact_CallsRepositoryDelete()
        {
            // Arrange
            var mockRepo = new Mock<ContactRepository>();
            var service = new ContactService(mockRepo.Object);

            // Act
            service.DeleteContact(1);

            // Assert
            mockRepo.Verify(repo => repo.Delete(1), Times.Once);
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
