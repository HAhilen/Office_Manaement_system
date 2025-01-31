using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Models;



namespace WebApplication2.Interfaces  
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
            return await _context.Set<Organization>()
                .Select(o => new OrganizationViewModel
                {
                    Id = o.Id,
                    OrganizationName = o.OrganizationName,
                    Country = o.Country,
                    EstablishedYear = o.EstablishedYear,
                })
                .ToListAsync();
        }

        // Get a single organization by ID
        public async Task<OrganizationViewModel> GetOrganizationById(int id)
        {
            var organization = await _context.Set<Organization>().FirstOrDefaultAsync(x => x.Id == id);
            if (organization == null)
            {
                throw new Exception($"Organization with id:{id} is not found");
            }
            return new OrganizationViewModel
            {
                Id = organization.Id,
                OrganizationName = organization.OrganizationName,
                Country = organization.Country,
                EstablishedYear = organization.EstablishedYear,

            };
        }

        // Add a new organization
        public async Task<int> AddOrganization(OrganizationViewModel model)
        {
            var organization = new Organization
            {
                OrganizationName = model.OrganizationName,
                EstablishedYear = model.EstablishedYear,
                Country = model.Country,


            };

            await _context.Set<Organization>().AddAsync(organization);
            await _context.SaveChangesAsync();

           return organization.Id;
        }

        // Update an existing organization
        public async Task<bool> UpdateOrganization(OrganizationViewModel organizationViewModel)
        {
            var existingorganization = await _context.Set<Organization>().FirstOrDefaultAsync(o => o.Id == organizationViewModel.Id);

            if (existingorganization == null)
            {
                return false;
            }

            existingorganization.OrganizationName = organizationViewModel.OrganizationName;
            existingorganization.EstablishedYear = organizationViewModel.EstablishedYear;
            existingorganization.Country = organizationViewModel.Country ?? existingorganization.Country;
          
            _context.Set<Organization>().Update(existingorganization);
            await _context.SaveChangesAsync();

            return true;
        }

        // Delete an organization by ID
        public async Task<bool> DeleteOrganization(int id)
        {
            var organization = await _context.Set<Organization>().FirstOrDefaultAsync(o => o.Id == id);

            if (organization == null)
            {
                return false;
            }

            _context.Set<Organization>().Remove(organization);
            await _context.SaveChangesAsync();

            return true;
        }
        
    }
}
