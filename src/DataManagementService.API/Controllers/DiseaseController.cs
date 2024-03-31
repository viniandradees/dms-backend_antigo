using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DiseaseController : Controller
    {
        private readonly IDiseaseService _diseaseService;
        public DiseaseController(IDiseaseService dataService)
        {
            _diseaseService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool getFullData = false) {
            try
            {
                var diseases = await _diseaseService.GetAllAsync(getFullData);
                return diseases is null ? NoContent() : Ok(diseases);
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
                var disease = await _diseaseService.GetByIdAsync(id);
                return disease is null ? NoContent() : Ok(disease);
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
                var disease = await _diseaseService.GetByNameAsync(name);
                return disease is null ? NoContent() : Ok(disease);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseDto model) {
            try
            {
                var disease = await _diseaseService.Add(model);
                return Ok(disease);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseDto model) {
            try
            {
                var disease = await _diseaseService.Update(id, model);
                return Ok(disease);
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
                await _diseaseService.Delete(id);
                return Ok(new { message = "Deleted" });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }
    }
}