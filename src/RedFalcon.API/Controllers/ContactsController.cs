using Microsoft.AspNetCore.Mvc;
using RedFalcon.Application.DTOs;
using RedFalcon.Application.Interfaces.Services;

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
        public async Task<IActionResult> GetAsync()
        {

            return Ok(new { contacts = await _contact.GetAsync() });
        }

        [HttpGet("{id}", Name = nameof(ContactsController.GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var contact = await _contact.GetByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateContactDTO contact)
        {
            var record = await _contact.CreateAsync(contact);

            if (record == null)
                return BadRequest("Failed");

            return CreatedAtRoute(nameof(ContactsController.GetByIdAsync), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateContactDTO contact)
        {
            var isSuccess = await _contact.UpdateAsync(id, contact);

            if (!isSuccess)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
