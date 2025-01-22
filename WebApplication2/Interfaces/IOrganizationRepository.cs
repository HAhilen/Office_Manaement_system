using WebApplication2.Entities;

namespace WebApplication2.Repositories
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllOrganizations();
        Task<Organization> GetOrganizationById(int id);
        Task AddOrganization(Organization organization);
        Task UpdateOrganization(Organization organization);
        Task DeleteOrganization(int id);
    }
}

