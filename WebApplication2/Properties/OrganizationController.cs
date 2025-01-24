using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Models;



namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        // GET: api/Organization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationViewModel>>> GetOrganizations()
        {
            var organizations = await _organizationRepository.GetAllOrganizations();
            var organizationViewModels = new List<OrganizationViewModel>();

            foreach (var organization in organizations)
            {
                organizationViewModels.Add(new OrganizationViewModel
                {
                    Id = organization.Id,
                    OrganizationName = organization.OrganizationName,
                    Address = organization.Address,
                    PhoneNumber = organization.PhoneNumber,
                    Email = organization.Email,
                    Website = organization.Website
                });
            }

            return Ok(organizationViewModels);
        }

        // GET: api/Organization/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationViewModel>> GetOrganization(int id)
        {
            var organization = await _organizationRepository.GetOrganizationById(id);

            if (organization == null)
            {
                return NotFound($"Organization with ID {id} not found.");
            }

            var organizationViewModel = new OrganizationViewModel
            {
                Id = organization.Id,
                OrganizationName = organization.OrganizationName,
                Address = organization.Address,
                PhoneNumber = organization.PhoneNumber,
                Email = organization.Email,
                Website = organization.Website
            };

            return Ok(organizationViewModel);
        }

        // POST: api/Organization
        [HttpPost]
        public async Task<ActionResult<OrganizationViewModel>> CreateOrganization(OrganizationViewModel organizationViewModel)
        {
            if (organizationViewModel == null)
            {
                return BadRequest("Organization view model is required.");
            }

            var organization = new Organization
            {
                OrganizationName = organizationViewModel.OrganizationName,
                Address = organizationViewModel.Address,
                PhoneNumber = organizationViewModel.PhoneNumber,
                Email = organizationViewModel.Email,
                Website = organizationViewModel.Website
            };

            await _organizationRepository.AddOrganization(organization);
            organizationViewModel.Id = organization.Id;

            return CreatedAtAction(nameof(GetOrganization), new { id = organizationViewModel.Id }, organizationViewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganization(int id, [FromBody] OrganizationViewModel organizationViewModel)
        {
            if (organizationViewModel == null)
            {
                return BadRequest("Organization view model cannot be null.");
            }

            if (id != organizationViewModel.Id)
            {
                return BadRequest("Organization ID mismatch.");
            }

            var existingOrganization = await _organizationRepository.GetOrganizationById(id);
            if (existingOrganization == null)
            {
                return NotFound($"Organization with ID {id} not found.");
            }

           
            var organizationEntity = new Organization
            {
                Id = organizationViewModel.Id,
                OrganizationName = organizationViewModel.OrganizationName,
                Address = organizationViewModel.Address,
                PhoneNumber = organizationViewModel.PhoneNumber,
                Email = organizationViewModel.Email,
                Website = organizationViewModel.Website
            };

            await _organizationRepository.UpdateOrganization(organizationEntity);

            return NoContent(); 
        }
 

        // DELETE: api/Organization/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            var organization = await _organizationRepository.GetOrganizationById(id);
            if (organization == null)
            {
                return NotFound($"Organization with ID {id} not found.");
            }

            await _organizationRepository.DeleteOrganization(id);
            return NoContent();
        }
    }
}
