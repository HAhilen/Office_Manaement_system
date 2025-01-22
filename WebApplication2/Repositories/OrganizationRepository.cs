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
        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync()
        {
            return await _context.Organizations
                .Include(o => o.Employees) 
                .ToListAsync();
        }
        public async Task<Organization> GetOrganizationByIdAsync(int id)
        {
            return await _context.Organizations
                .Include(o => o.Employees)  
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}