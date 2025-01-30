using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Interfaces;
using Department = WebApplication2.Migrations.Department;

namespace WebApplication2.Services
{
    
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext context;

        public ManagerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
      
        public async Task<IEnumerable<ManagerViewModel>> GetAllManagers()
        {
            var managers = await context.Set<Manager>()
                .Select(m => new ManagerViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Email = m.Email,
                    PhoneNumber = m.PhoneNumber,
                    Address = m.Address,
                    DepartmentNames = m.Departments.Select(d => d.DepartmentName).ToList(),
                    

                })
                .ToListAsync();
            return managers;
        }

        public async Task<ManagerViewModel> GetManagerById(int id)
        {
            var manager = await context.Set<Manager>().FirstOrDefaultAsync(e => e.Id == id);
            if (manager == null)
            {
                throw new Exception($"Employee with id :{id} is not found");
            }
            return new ManagerViewModel
            {
                Id = manager.Id,
                Name = manager.Name,
                Email = manager.Email,
                Address = manager.Address,
                PhoneNumber = manager.PhoneNumber,
               
            };
        }

        public async Task<ManagerViewModel> AddManager(ManagerViewModel managerViewModel)
        {
            var manager = new Manager
            {
                Name = managerViewModel.Name,
                Email = managerViewModel.Email,
                 PhoneNumber = managerViewModel.PhoneNumber,
                Address = managerViewModel.Address
            };

            await context.Set<Manager>().AddAsync(manager);
            await context.SaveChangesAsync();

            return new ManagerViewModel
            {
                Id = manager.Id,
                Name = manager.Name,
                Email = manager.Email,
                 PhoneNumber = manager.PhoneNumber,
                 Address = manager.Address
            };
        }

        // Update operation for Manager
        public async Task<bool> UpdateManager(ManagerViewModel managerViewModel)
        {
            var existingManager = await context.Set<Manager>()
                .FirstOrDefaultAsync(e => e.Id == managerViewModel.Id);

            if (existingManager == null)
            {
                return false;
            }
            existingManager.Name = managerViewModel.Name ?? managerViewModel.Name;
            existingManager.Email = managerViewModel.Email ?? managerViewModel.Email;
             existingManager.Address = managerViewModel.Address ?? existingManager.Address;
             existingManager.PhoneNumber = managerViewModel.PhoneNumber ?? managerViewModel.PhoneNumber;
            await context.SaveChangesAsync();
            return true;
        }
        //Delete opetation for Managers

        public async Task<bool> DeleteManager(int id)
        {
            var manager = await context.Set<Manager>().FindAsync(id);
            if (manager == null) return false;

            context.Set<Manager>().Remove(manager);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
