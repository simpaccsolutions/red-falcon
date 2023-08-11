using Microsoft.AspNetCore.Mvc;
using RedFalcon.Application.DTOs;
using RedFalcon.Application.Interfaces.Services;
using RedFalcon.Application.ResourceParameters;

namespace RedFalcon.API.Controllers
{
    [Route("api/organizations")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationServices _organization;
        public OrganizationsController(IOrganizationServices organization)
        {
            _organization = organization;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizationsAsync(string? search, int page=1, int pagesize = 10)
        {
            var resourceParameters = new OrganizationResourceParameters
            {
                Search = search,
                Page = page,
                PageSize = pagesize
            };
            var records = await _organization.GetAsync(resourceParameters);

            return Ok(new
            {
                data = records,
                total = records.TotalCount,
                page = resourceParameters.Page,
                pageSize = resourceParameters.PageSize,
                totalPages = records.TotalPages,
            });
        }

        [HttpGet("{id}", Name = nameof(OrganizationsController.GetOrganizationByIdAsync))]
        public async Task<IActionResult> GetOrganizationByIdAsync(int id)
        {
            var organization = await _organization.GetByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            return Ok(organization);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganizationyAsync([FromBody] CreateOrganizationDTO organization)
        {
            var record = await _organization.CreateAsync(organization);

            if (record == null)
                return BadRequest("Failed");

            return CreatedAtRoute(nameof(OrganizationsController.GetOrganizationByIdAsync), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganizationAsync([FromRoute] int id, [FromBody] UpdateOrganizationDTO organization)
        {
            var isSuccess = await _organization.UpdateAsync(id, organization);

            if (!isSuccess)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganizationAsync([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var organization = await _organization.GetByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }

            var isSuccess = await _organization.DeleteAsync(id);

            if (!isSuccess)
                return NotFound();

            return NoContent();

        }


    }
}
