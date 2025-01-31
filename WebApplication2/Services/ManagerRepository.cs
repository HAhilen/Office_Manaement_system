using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Entities;
using WebApplication2.Interfaces;
using WebApplication2.Models;

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
                    ManagerName = m.ManagerName,
                    Email = m.Email,
                    PhoneNumber = m.PhoneNumber,
                    DepartmentId = m.DepartmentId,
                    HireDate = m.HireDate,


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
                ManagerName = manager.ManagerName,
                Email = manager.Email,
                PhoneNumber = manager.PhoneNumber,
                HireDate = manager.HireDate,
                DepartmentId = manager.DepartmentId,

            };
        }

        public async Task<ManagerViewModel> AddManager(ManagerViewModel managerViewModel)
        {
            var manager = new Manager
            {
                ManagerName = managerViewModel.ManagerName,
                Email = managerViewModel.Email,
                PhoneNumber = managerViewModel.PhoneNumber,
                HireDate = managerViewModel.HireDate,
                DepartmentId = managerViewModel.DepartmentId,

            };

            await context.Set<Manager>().AddAsync(manager);
            await context.SaveChangesAsync();

            return new ManagerViewModel
            {
                Id = manager.Id,
                ManagerName = manager.ManagerName,
                Email = manager.Email,
                PhoneNumber = manager.PhoneNumber,
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
            existingManager.ManagerName = managerViewModel.ManagerName ?? managerViewModel.ManagerName;
            existingManager.Email = managerViewModel.Email ?? managerViewModel.Email;
            existingManager.PhoneNumber = managerViewModel.PhoneNumber ?? managerViewModel.PhoneNumber;
            existingManager.HireDate = managerViewModel.HireDate;
            existingManager.DepartmentId = managerViewModel.DepartmentId;

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
