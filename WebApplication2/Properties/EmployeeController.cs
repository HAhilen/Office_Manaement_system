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
            // Fetch the employees from the repository
            var employees = await _employeeRepository.GetAllEmployees();

            if (employees == null || !employees.Any())
            {
                return NotFound("No employees found.");
            }
            return Ok(employees);
         
        }

        // GET api/employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);

           /* if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            var employeeViewModel = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Department = employee.Department,
                Email = employee.Email,
                OrganizationId = employee.OrganizationId,
                
            };
  */
            return Ok(employee);
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

            // Create a new employee entity
            var employee = new Employee
            {
                Name = employeeViewModel.Name,
                Department = employeeViewModel.Department,
                Email = employeeViewModel.Email,
                OrganizationId = employeeViewModel.OrganizationId,
            };

            // Save the new employee to the repository
            await _employeeRepository.AddEmployee(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT: api/employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel == null)
            {
                return BadRequest("Employee view model cannot be null.");
            }
            if (id != employeeViewModel.Id)
            {
                return BadRequest("Employee ID mismatch.");
            }
            var existingEmployee = await _employeeRepository.GetEmployeeById(id);
            if (existingEmployee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }
            var employeeEntity = new Employee
            {
                Id = employeeViewModel.Id,
                Name = employeeViewModel.Name,
                Department = employeeViewModel.Department,
                Email = employeeViewModel.Email,
                OrganizationId = employeeViewModel.OrganizationId,
            };
            await _employeeRepository.UpdateEmployee(employeeEntity);

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

            // Delete the employee from the repository
            await _employeeRepository.DeleteEmployee(id);

            return NoContent();
        }
    }
}
