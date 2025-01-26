using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeViewModel>>> GetEmployees() =>
            Ok(await _employeeRepository.GetAllEmployees());

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeViewModel>> GetEmployee(int id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeViewModel employeeViewModel)
        {
            var result = await _employeeRepository.AddEmployee(employeeViewModel);
            return result == null ? BadRequest() : CreatedAtAction(nameof(GetEmployee), new { id = result.Id }, result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            var isUpdated = await _employeeRepository.UpdateEmployee(employeeViewModel);
            return isUpdated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var isDeleted = await _employeeRepository.DeleteEmployee(id);
            return isDeleted ? NoContent() : NotFound();
        }
    }
}