using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication2.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all organizations (no need to include employees anymore)
        public async Task<IEnumerable<Organization>> GetAllOrganizations()
        {
            return await _context.Organizations.ToListAsync();
        }

        // Get an organization by ID (no need to include employees)
        public async Task<Organization> GetOrganizationById(int id)
        {
            return await _context.Organizations
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        // Get organizations with employees (no need to include employees anymore)
        public async Task<IEnumerable<Organization>> GetOrganizationsWithEmployees()
        {
            return await _context.Organizations
                .ToListAsync();
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

        // Delete an organization
        public async Task DeleteOrganization(int id)
        {
            var organization = await GetOrganizationById(id);
            if (organization != null)
            {
                _context.Organizations.Remove(organization);
                await _context.SaveChangesAsync();
            }
        }
    }
}
