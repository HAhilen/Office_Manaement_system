using WebApplication2.Entities;
using WebApplication2.Models;

public interface IOrganizationRepository
{
    Task<IEnumerable<OrganizationViewModel>> GetAllOrganizations();
    Task<OrganizationViewModel> GetOrganizationById(int id);
    Task AddOrganization(OrganizationViewModel organizationViewModel);
    Task<bool> UpdateOrganization(OrganizationViewModel organizationViewModel);
    Task<bool> DeleteOrganization(int id);
}