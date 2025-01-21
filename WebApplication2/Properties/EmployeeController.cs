using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Data;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Department = e.Department,
                    Email = e.Email,
                    OrganizationId=e.OrganizationId
                })
                .ToListAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            if (employee == null)
            {
                return NotFound();
            }
            var employeeViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                OrganizationId=employee.OrganizationId
            };

            return Ok(employeeViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeViewModel>> CreateEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = new Employee
            {
                Name = employeeViewModel.Name,
                Department = employeeViewModel.Department,
                Email = employeeViewModel.Email,
                OrganizationId=employeeViewModel.OrganizationId
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            employeeViewModel.Id = employee.Id;

            return CreatedAtAction(nameof(GetEmployee), new { id = employeeViewModel.Id }, employeeViewModel);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id) return BadRequest();

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            employee.Name = employeeViewModel.Name;
            employee.Department = employeeViewModel.Department;
            employee.Email = employeeViewModel.Email;
            employee.OrganizationId = employeeViewModel.OrganizationId;

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
