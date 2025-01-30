using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Models;

namespace WebApplication2.Services
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
            return await _context.Set<Department>()
                .Select(d => new DepartmentViewModel
                {
                    Id = d.Id,
                    DepartmentName = d.DepartmentName,
                    ManagerId = d.ManagerId
                    
                })
                .ToListAsync();
        }
      //  Get Department by id 
        public async Task<DepartmentViewModel> GetDepartmentById(int id)
        {
            var department = await _context.Set<Department>().FirstOrDefaultAsync(d => d.Id == id);
            if (department == null)
            {
                throw new Exception($"Department with id {id} not found");
            }
            return new DepartmentViewModel
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName,
                ManagerId = department.ManagerId
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

            await _context.Set<Department>().AddAsync(department);
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
            var existingDepartment = await _context.Set<Department>()
                .FirstOrDefaultAsync(d => d.Id == departmentViewModel.Id);

            if (existingDepartment == null)
            {
                return false;
            }
            existingDepartment.DepartmentName = departmentViewModel.DepartmentName ?? existingDepartment.DepartmentName;
            existingDepartment.ManagerId = departmentViewModel.ManagerId ?? existingDepartment.ManagerId;
            _context.Set<Department>().Update(existingDepartment);
            await _context.SaveChangesAsync();

            return true;
        }
        //Delete opetation for Departments

        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await _context.Set<Department>().FindAsync(id);
            if (department == null) return false;

            _context.Set<Department>().Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }
        //Return all employeee with corresponding department
        public async Task<DepartmentEmployeeModel> GetDepartmentEmployeeByDepId(int departmentId)
        {
            var depEmp = await _context.Set<Department>().Include(x=>x.Employees).ThenInclude(x=>x.Organization).FirstOrDefaultAsync(x => x.Id == departmentId);
            if (depEmp == null)
            {
                throw new Exception("Not Found");
            }
            return new DepartmentEmployeeModel
            {
                Department = new DepartmentViewModel
                {
                    Id = depEmp.Id,
                    DepartmentName = depEmp.DepartmentName,
                    ManagerId = depEmp.ManagerId
                },
                Employees = depEmp?.Employees?.Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                     DepartmentId=e.DepartmentId,
                      DepartmentName=e.Department?.DepartmentName,
                       OrganizationId=e.OrganizationId,
                        OrganizationName= e.Organization?.OrganizationName
                }).ToList()
            };
        }
    }
}
