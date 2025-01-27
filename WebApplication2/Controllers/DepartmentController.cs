using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Interfaces;

namespace WebApplication2.Controllers
{
    [Route("api/Department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentViewModel>>> GetDepartments() =>
            Ok(await _departmentRepository.GetAllDepartments());

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentViewModel>> GetDepartment(int id)
        {
            var employee = await _departmentRepository.GetDepartmentById(id);
            return employee == null ? NotFound() : Ok(employee);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee( DepartmentViewModel departmentViewModel)
        {
            var result = await _departmentRepository.AddDepartment(departmentViewModel);
            return result == null ? BadRequest() : CreatedAtAction(nameof(GetDepartment), new { id = result.Id }, result);
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateDepartment(DepartmentViewModel departmentViewModel)
        {
            var isUpdated = await _departmentRepository.UpdateDepartment(departmentViewModel);
            return isUpdated ? NoContent() : NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var isDeleted = await _departmentRepository.DeleteDepartment(id);
            return isDeleted ? NoContent() : NotFound();
        }
    }
}