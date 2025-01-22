using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        // Injecting the repository into the controller via the constructor
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployees();
            var employeeViewModels = employees.Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Department = e.Department,
                Email = e.Email,
                OrganizationId = e.OrganizationId
            }).ToList();

            return Ok(employeeViewModels);
        }

        // GET api/employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            var employeeViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                OrganizationId = employee.OrganizationId
            };

            return Ok(employeeViewModel);
        }

        // POST api/employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel == null)
            {
                return BadRequest(new { message = "Employee view model is required" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var employee = new Employee
            {
                Name = employeeViewModel.Name,
                Department = employeeViewModel.Department,
                Email = employeeViewModel.Email,
                OrganizationId = employeeViewModel.OrganizationId
            };

            await _employeeRepository.AddEmployee(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT api/employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }

            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            employee.Name = employeeViewModel.Name;
            employee.Department = employeeViewModel.Department;
            employee.Email = employeeViewModel.Email;
            employee.OrganizationId = employeeViewModel.OrganizationId;

            await _employeeRepository.UpdateEmployee(employee);

            return NoContent();
        }

        // DELETE api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            await _employeeRepository.DeleteEmployee(id);

            return NoContent();
        }
    }
}
