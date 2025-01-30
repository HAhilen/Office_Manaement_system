using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Interfaces;

namespace WebApplication2.Controllers
{
    //Route for manager api
    [Route("api/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;

        // Injecting IManagerRepository for managing managers
        public ManagerController(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        // GET: api/manager
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManagerViewModel>>> GetManagers()
        {
            var managers = await _managerRepository.GetAllManagers();
            return Ok(managers);
        }

        // GET: api/manager/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ManagerViewModel>> GetManager(int id)
        {
            var manager = await _managerRepository.GetManagerById(id);
            return manager == null ? NotFound() : Ok(manager);
        }

        // POST: api/manager
        [HttpPost]
        public async Task<IActionResult> CreateManager(ManagerViewModel managerViewModel)
        {
            var result = await _managerRepository.AddManager(managerViewModel);
            return result == null ? BadRequest() : CreatedAtAction(nameof(GetManager), new { id = result.Id }, result);
        }

        // PUT: api/manager/update
        [HttpPost("update")]
        public async Task<IActionResult> UpdateManager(ManagerViewModel managerViewModel)
        {
            var isUpdated = await _managerRepository.UpdateManager(managerViewModel);
            return isUpdated ? NoContent() : NotFound();
        }

        // DELETE: api/manager/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManager(int id)
        {
            var isDeleted = await _managerRepository.DeleteManager(id);
            return isDeleted ? NoContent() : NotFound();
        }
    }
}
