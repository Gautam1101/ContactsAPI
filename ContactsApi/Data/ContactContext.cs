using Microsoft.EntityFrameworkCore;
using ContactsApi.Models;

namespace ContactsApi.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }
    }
}
