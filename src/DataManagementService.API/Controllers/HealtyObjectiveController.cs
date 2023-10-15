using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.API.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HealtyObjectiveController : Controller
    {
        private readonly IHealtyObjectiveService _healtyObjectiveService;
        public HealtyObjectiveController(IHealtyObjectiveService dataService)
        {
            _healtyObjectiveService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool getFullData = false) {
            try
            {
                var healtyObjectives = await _healtyObjectiveService.GetAllAsync(getFullData);
                return healtyObjectives is null ? NoContent() : Ok(healtyObjectives);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            try
            {
                var healtyObjective = await _healtyObjectiveService.GetByIdAsync(id);
                return healtyObjective is null ? NoContent() : Ok(healtyObjective);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name) {
            try
            {
                var healtyObjective = await _healtyObjectiveService.GetByNameAsync(name);
                return healtyObjective is null ? NoContent() : Ok(healtyObjective);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(HealtyObjectiveDto model) {
            try
            {
                var healtyObjective = await _healtyObjectiveService.Add(model);
                return Ok(healtyObjective);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, HealtyObjectiveDto model) {
            try
            {
                var healtyObjective = await _healtyObjectiveService.Update(id, model);
                return Ok(healtyObjective);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try
            {
                await _healtyObjectiveService.Delete(id);
                return Ok(new { message = "Deleted" });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }
    }
}