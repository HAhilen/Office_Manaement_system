using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Models;

namespace WebApplication2.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all organizations
        public async Task<IEnumerable<OrganizationViewModel>> GetAllOrganizations()
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

            return organizations;
        }

        // Get a single organization by ID
        public async Task<OrganizationViewModel> GetOrganizationById(int id)
        {
            var organization = await _context.Organizations
                .Where(o => o.Id == id)
                .Select(o => new OrganizationViewModel
                {
                    Id = o.Id,
                    OrganizationName = o.OrganizationName,
                    Address = o.Address,
                    PhoneNumber = o.PhoneNumber,
                    Email = o.Email,
                    Website = o.Website
                })
                .FirstOrDefaultAsync();

            return organization;
        }
        public async Task<IEnumerable<OrganizationViewModel>> GetOrganizationsWithEmployees()
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

            return organizations;
        }
 
        // Add a new organization
        public async Task AddOrganization(Organization organization)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
        }

        // Update an existing organization
        public async Task UpdateOrganization(Organization organization)
        {
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
        }

        // Delete an organization by ID
        public async Task DeleteOrganization(int id)
        {
            var organization = await _context.Organizations
                .FirstOrDefaultAsync(o => o.Id == id);

            if (organization != null)
            {
                _context.Organizations.Remove(organization);
                await _context.SaveChangesAsync();
            }
        }
    }
}
