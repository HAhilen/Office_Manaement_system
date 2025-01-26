using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Data;

namespace WebApplication2.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DepartmentViewModel>> GetAllDepartments()
        {
            return await _context.Departments
                .Select(d=> new DepartmentViewModel
                {
                    Id = d.Id,
                    DepartmentName = d.DepartmentName,
                    ManagerId    = d.ManagerId
                })
                .ToListAsync();
        }

        public async Task<DepartmentViewModel> GetDepartmentById(int id)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            return department == null
                ? null
                : new DepartmentViewModel
                {
                    Id = department.Id,
                    ManagerId=department.ManagerId,
                    DepartmentName = department.DepartmentName,
                };
        }

        
        public async Task<DepartmentViewModel> AddDepartment(DepartmentViewModel departmentViewModel)
        {
            var department = new Department
            {
                Id = departmentViewModel.Id,
                DepartmentName = departmentViewModel.DepartmentName,
                ManagerId = departmentViewModel.ManagerId
            };

            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();

            return new DepartmentViewModel
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName,
                ManagerId = department.ManagerId,
               
            };
        }

    // Update operation for department
        public async Task<bool> UpdateDepartment(DepartmentViewModel departmentViewModel)
        {
            var existingDepartment = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == departmentViewModel.Id);

            if (existingDepartment == null)
            {
                return false;
            }
            existingDepartment.DepartmentName = departmentViewModel.DepartmentName ?? existingDepartment.DepartmentName;
            existingDepartment.ManagerId = departmentViewModel.ManagerId ?? existingDepartment.ManagerId;
            _context.Departments.Update(existingDepartment);
            await _context.SaveChangesAsync();

            return true;
        }
    //Delete opetation for Departments
    
        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null) return false;

            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
