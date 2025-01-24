using WebApplication2.Entities;
using WebApplication2.Models;
public interface IOrganizationRepository
{
    Task<IEnumerable<OrganizationViewModel>> GetAllOrganizations();
    Task<OrganizationViewModel> GetOrganizationById(int id);
    Task<IEnumerable<OrganizationViewModel>> GetOrganizationsWithEmployees();
    Task AddOrganization(Organization organization);
    Task UpdateOrganization(Organization organization);
    Task DeleteOrganization(int id);
}