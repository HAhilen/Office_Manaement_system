using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrganizationController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationViewModel>>> GetOrganizations()
        {
            var organizations = await _context.Organizations
                .Select(o => new OrganizationViewModel
                {
                    Id = o.Id,
                    OrganizationName = o.OrganizationName,
                    Address = o.Address,
                    PhoneNumber = o.PhoneNumber,
                    Email = o.Email,
                    Website = o.Website
                })
                .ToListAsync();

            return Ok(organizations);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationViewModel>> GetOrganization(int id)
        {
            var organization = await _context.Organizations
                .FirstOrDefaultAsync(o => o.Id == id);

            if (organization == null)
            {
                return NotFound(new { message = $"Organization with the  ID {id} not found" });
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

       
        [HttpPost]
        public async Task<ActionResult<OrganizationViewModel>> CreateOrganization(OrganizationViewModel organizationViewModel)
        {
            var organization = new Organization
            {
                OrganizationName = organizationViewModel.OrganizationName,
                Address = organizationViewModel.Address,
                PhoneNumber = organizationViewModel.PhoneNumber,
                Email = organizationViewModel.Email,
                Website = organizationViewModel.Website
            };

            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();

            organizationViewModel.Id = organization.Id;

            return CreatedAtAction(nameof(GetOrganization), new { id = organizationViewModel.Id }, organizationViewModel);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganization(int id, OrganizationViewModel organizationViewModel)
        {
            if (id != organizationViewModel.Id) return BadRequest();

            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null) return NotFound(new { message =$"Organization with the  ID {id} not found" });

            organization.OrganizationName = organizationViewModel.OrganizationName;
            organization.Address = organizationViewModel.Address;
            organization.PhoneNumber = organizationViewModel.PhoneNumber;
            organization.Email = organizationViewModel.Email;
            organization.Website = organizationViewModel.Website;

            _context.Entry(organization).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete an organization
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null) return NotFound(new{ message = $"Organization  with ID {id} not found." });

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
