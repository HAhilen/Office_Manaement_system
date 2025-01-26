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
            return await _context.Organizations
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
        }

        // Get a single organization by ID
        public async Task<OrganizationViewModel> GetOrganizationById(int id)
        {
            return await _context.Organizations
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
        }

        // Add a new organization
        public async Task AddOrganization(OrganizationViewModel organizationViewModel)
        {
            var organization = new Organization
            {
                OrganizationName = organizationViewModel.OrganizationName,
                Address = organizationViewModel.Address,
                PhoneNumber = organizationViewModel.PhoneNumber,
                Email = organizationViewModel.Email,
                Website = organizationViewModel.Website
            };

            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();

            organizationViewModel.Id = organization.Id; // Update the ID in the ViewModel
        }

        // Update an existing organization
        public async Task<bool> UpdateOrganization(OrganizationViewModel organizationViewModel)
        {
            var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.Id == organizationViewModel.Id);

            if (organization == null)
            {
                return false;
            }

            organization.OrganizationName = organizationViewModel.OrganizationName;
            organization.Address = organizationViewModel.Address;
            organization.PhoneNumber = organizationViewModel.PhoneNumber;
            organization.Email = organizationViewModel.Email;
            organization.Website = organizationViewModel.Website;

            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();

            return true;
        }

        // Delete an organization by ID
        public async Task<bool> DeleteOrganization(int id)
        {
            var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.Id == id);

            if (organization == null)
            {
                return false;
            }

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
