using ContactsApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ContactsApi.Repositories
{
    public class ContactRepository
    {
        private readonly string _filePath = "data.json";

        public ContactRepository()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(new List<Contact>()));
            }
        }

        public IEnumerable<Contact> GetAll(int pageNumber, int pageSize, string searchTerm)
        {
            var json = File.ReadAllText(_filePath);
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(json) ?? new List<Contact>();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                contacts = contacts.Where(c => c.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return contacts.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public int GetTotalCount(string searchTerm)
        {
            var json = File.ReadAllText(_filePath);
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(json) ?? new List<Contact>();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                contacts = contacts.Where(c => c.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return contacts.Count;
        }

        public Contact GetById(int id)
        {
            var contacts = GetAll(int.MaxValue, int.MaxValue, string.Empty); // Get all to find by ID
            return contacts.FirstOrDefault(c => c.Id == id);
        }

        public void Add(Contact contact)
        {
            var contacts = GetAll(int.MaxValue, int.MaxValue, string.Empty).ToList();
            contact.Id = contacts.Count > 0 ? contacts.Max(c => c.Id) + 1 : 1;
            contacts.Add(contact);
            SaveToFile(contacts);
        }

        public void Update(Contact contact)
        {
            var contacts = GetAll(int.MaxValue, int.MaxValue, string.Empty).ToList();
            var existingContact = contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Email = contact.Email;
                SaveToFile(contacts);
            }
        }

        public void Delete(int id)
        {
            var contacts = GetAll(int.MaxValue, int.MaxValue, string.Empty).ToList();
            var contact = contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                contacts.Remove(contact);
                SaveToFile(contacts);
            }
        }

        private void SaveToFile(List<Contact> contacts)
        {
            var json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}
