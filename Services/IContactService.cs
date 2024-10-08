using ContactManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(int id);
    }
}
