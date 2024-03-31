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
    public class DietaryOptionController : Controller
    {
        private readonly IDietaryOptionService _dietaryOptionService;
        public DietaryOptionController(IDietaryOptionService dataService)
        {
            _dietaryOptionService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool getFullData = false) {
            try
            {
                var dietaryOptions = await _dietaryOptionService.GetAllAsync(getFullData);
                return dietaryOptions is null ? NoContent() : Ok(dietaryOptions);
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
                var dietaryOption = await _dietaryOptionService.GetByIdAsync(id);
                return dietaryOption is null ? NoContent() : Ok(dietaryOption);
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
                var dietaryOption = await _dietaryOptionService.GetByNameAsync(name);
                return dietaryOption is null ? NoContent() : Ok(dietaryOption);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(DietaryOptionDto model) {
            try
            {
                var dietaryOption = await _dietaryOptionService.Add(model);
                return Ok(dietaryOption);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DietaryOptionDto model) {
            try
            {
                var dietaryOption = await _dietaryOptionService.Update(id, model);
                return Ok(dietaryOption);
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
                await _dietaryOptionService.Delete(id);
                return Ok(new { message = "Deleted" });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }
    }
}