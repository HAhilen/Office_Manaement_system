using WebApplication2.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication2.Repositories
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllOrganizationsAsync();
        Task<Organization?> GetOrganizationByIdAsync(int id);
    }
}
