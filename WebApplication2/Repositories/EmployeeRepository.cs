using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;

namespace WebApplication2.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithOrganizationAsync()
        {
            return await _context.Employees
                .Include(e => e.Organization)  
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeWithOrganizationByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Organization)  
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}