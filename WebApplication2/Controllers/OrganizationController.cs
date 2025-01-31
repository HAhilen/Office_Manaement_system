using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Interfaces;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Route("api/organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        // Get all organizations
        
        [HttpGet]
        public async Task<IActionResult> GetOrganizations()
        {
            var organizations = await _organizationRepository.GetAllOrganizations();
            return Ok(organizations);
        }

        // Get an organization by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrganization(int id)
        {
            var organization = await _organizationRepository.GetOrganizationById(id);
            return organization == null ? NotFound() : Ok(organization);
        }

        // Add a new organization
        [HttpPost]
        public async Task<IActionResult> AddOrganization([FromBody] OrganizationViewModel organizationViewModel)
        {
            if (organizationViewModel == null)
                return BadRequest();

            await _organizationRepository.AddOrganization(organizationViewModel);
            return CreatedAtAction(nameof(GetOrganization), new { id = organizationViewModel.Id }, organizationViewModel);
        }

        // Update an organization
        [HttpPost("update")]
        public async Task<IActionResult> UpdateOrganization( OrganizationViewModel organizationViewModel)
        {
            if (organizationViewModel == null)
                return BadRequest();

            var updated = await _organizationRepository.UpdateOrganization(organizationViewModel);
            return updated ? NoContent() : NotFound();
        }

        // Delete an organization
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            var deleted = await _organizationRepository.DeleteOrganization(id);
            return deleted ? NoContent() : NotFound();
        }
        
    }
}
