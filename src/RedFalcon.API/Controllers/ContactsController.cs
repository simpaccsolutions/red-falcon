using Microsoft.AspNetCore.Mvc;
using RedFalcon.Application.DTOs;
using RedFalcon.Application.Interfaces.Services;
using RedFalcon.Application.ResourceParameters;

namespace RedFalcon.API.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactServices _contact;
        public ContactsController(IContactServices contact)
        {
            _contact = contact;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactsAsync(string? search, int page = 1, int pagesize = 10)
        {
            var resourceParameters = new ContactResourceParameters
            {
                Search = search,
                Page = page,
                PageSize = pagesize
            };
            var records = await _contact.GetAsync(resourceParameters);

            return Ok(new
            {
                data = records,
                total = records.TotalCount,
                page = resourceParameters.Page,
                pageSize = resourceParameters.PageSize,
                totalPages = records.TotalPages,
            });
        }

        [HttpGet("{id}", Name = nameof(ContactsController.GetContactByIdAsync))]
        public async Task<IActionResult> GetContactByIdAsync(int id)
        {
            var contact = await _contact.GetByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContactAsync([FromBody] CreateContactDTO contact)
        {
            var record = await _contact.CreateAsync(contact);

            if (record == null)
                return BadRequest("Failed");

            return CreatedAtRoute(nameof(ContactsController.GetContactByIdAsync), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactAsync([FromRoute] int id, [FromBody] UpdateContactDTO contact)
        {
            var isSuccess = await _contact.UpdateAsync(id, contact);

            if (!isSuccess)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactAsync([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var contact = await _contact.GetByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            var isSuccess = await _contact.DeleteAsync(id);

            if (!isSuccess)
                return NotFound();

            return NoContent();

        }


    }
}
