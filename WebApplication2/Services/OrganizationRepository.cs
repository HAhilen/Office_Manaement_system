using AutoMapper.Execution;
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
            return await _context.Set<Organization>()
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
            var organization = await _context.Set<Organization>().FirstOrDefaultAsync(x => x.Id == id);
            if(organization == null)
            {
                throw new Exception($"Organization with id:{id} is not found");
            }
            return new OrganizationViewModel
            {
                Id = organization.Id,
                OrganizationName = organization.OrganizationName,
                Address = organization.Address,
                PhoneNumber = organization.PhoneNumber,
                Email = organization.Email,
                Website = organization.Website
            };         
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

            await _context.Set<Organization>().AddAsync(organization);
            await _context.SaveChangesAsync();

            organizationViewModel.Id = organization.Id; // Update the ID in the ViewModel
        }

        // Update an existing organization
        public async Task<bool> UpdateOrganization(OrganizationViewModel organizationViewModel)
        {
            var existingorganization = await _context.Set<Organization>().FirstOrDefaultAsync(o => o.Id == organizationViewModel.Id);

            if (existingorganization == null)
            {
                return false;
            }

            existingorganization.OrganizationName = organizationViewModel.OrganizationName??existingorganization.OrganizationName;
            existingorganization.Address = organizationViewModel.Address??existingorganization.Address;
            existingorganization.PhoneNumber = organizationViewModel.PhoneNumber??existingorganization.PhoneNumber;
            existingorganization.Email = organizationViewModel.Email??existingorganization.Email;
            existingorganization.Website = organizationViewModel.Website??existingorganization.Website;

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
