using ContactManager.DataAccess;
using ContactManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> _contactRepository;

        public ContactService(IRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task AddContactAsync(Contact contact)
        {
            await _contactRepository.AddAsync(contact);
            await _contactRepository.SaveAsync();
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            await _contactRepository.UpdateAsync(contact);
            await _contactRepository.SaveAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            await _contactRepository.DeleteAsync(id);
            await _contactRepository.SaveAsync();
        }
    }
}

