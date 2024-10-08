using ContactManager.Models;
using ContactManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace ContactManager.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return View(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return RedirectToAction("Index");

            var contacts = new List<Contact>();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    var contact = new Contact
                    {
                        Name = values[0],
                        BirthDate = DateOnly.Parse(values[1]),
                        Married = bool.Parse(values[2]),
                        Phone = values[3],
                        Salary = decimal.Parse(values[4])
                    };

                    contacts.Add(contact);
                }
            }

            foreach (var contact in contacts)
            {
                await _contactService.AddContactAsync(contact);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactService.UpdateContactAsync(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null) return NotFound();
            return View(contact);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _contactService.DeleteContactAsync(id);
            return RedirectToAction("Index");
        }
    }
}
