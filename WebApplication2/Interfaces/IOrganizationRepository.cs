using WebApplication2.Entities;
using WebApplication2.Models;

namespace  WebApplication2.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<OrganizationViewModel>> GetAllOrganizations();
        Task<OrganizationViewModel> GetOrganizationById(int id);
        Task<int>AddOrganization(OrganizationViewModel organizationViewModel);
        Task<bool> UpdateOrganization(OrganizationViewModel organizationViewModel);
        Task<bool> DeleteOrganization(int id);
        Task<OrganizationManagerModel> GetOrganizationWithManager(int organizationId);
    }
}
