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
    public class FoodAttributeController : Controller
    {
        private readonly IFoodAttributeService _foodAttributeService;
        public FoodAttributeController(IFoodAttributeService dataService)
        {
            _foodAttributeService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool getFullData = false) {
            try
            {
                var foodAttributes = await _foodAttributeService.GetAllAsync(getFullData);
                return foodAttributes is null ? NoContent() : Ok(foodAttributes);
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
                var foodAttribute = await _foodAttributeService.GetByIdAsync(id);
                return foodAttribute is null ? NoContent() : Ok(foodAttribute);
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
                var foodAttribute = await _foodAttributeService.GetByNameAsync(name);
                return foodAttribute is null ? NoContent() : Ok(foodAttribute);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(FoodAttributeDto model) {
            try
            {
                var foodAttribute = await _foodAttributeService.Add(model);
                return Ok(foodAttribute);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FoodAttributeDto model) {
            try
            {
                var foodAttribute = await _foodAttributeService.Update(id, model);
                return Ok(foodAttribute);
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
                await _foodAttributeService.Delete(id);
                return Ok(new { message = "Deleted" });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }
    }
}