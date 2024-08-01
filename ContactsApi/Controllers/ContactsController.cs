using Microsoft.AspNetCore.Mvc;
using ContactsApi.Models;
using ContactsApi.Services;
using System.Collections.Generic;
using System;

namespace ContactsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _service;

        public ContactsController(ContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetContacts(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? searchTerm = null) // Make searchTerm nullable
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            var contacts = _service.GetContacts(pageNumber, pageSize, searchTerm);
            var totalCount = _service.GetTotalCount(searchTerm);
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Contacts = contacts,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public ActionResult<Contact> GetContact(int id)
        {
            var contact = _service.GetContactById(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPost]
        public ActionResult<Contact> PostContact(Contact contact)
        {
            _service.AddContact(contact);
            return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _service.UpdateContact(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            _service.DeleteContact(id);
            return NoContent();
        }
    }
}
