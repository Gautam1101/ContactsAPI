using ContactsApi.Models;
using ContactsApi.Repositories;
using System.Collections.Generic;

namespace ContactsApi.Services
{
    public class ContactService
    {
        private readonly ContactRepository _repository;

        public ContactService(ContactRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Contact> GetContacts(int pageNumber, int pageSize, string searchTerm)
        {
            return _repository.GetAll(pageNumber, pageSize, searchTerm);
        }

        public int GetTotalCount(string searchTerm)
        {
            return _repository.GetTotalCount(searchTerm);
        }

        public Contact GetContactById(int id)
        {
            return _repository.GetById(id);
        }

        public void AddContact(Contact contact)
        {
            _repository.Add(contact);
        }

        public void UpdateContact(Contact contact)
        {
            _repository.Update(contact);
        }

        public void DeleteContact(int id)
        {
            _repository.Delete(id);
        }
    }
}
