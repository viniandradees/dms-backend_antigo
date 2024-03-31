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
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        public MealController(IMealService dataService)
        {
            _mealService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool getFullData = false) {
            try
            {
                var meals = await _mealService.GetAllAsync(getFullData);
                return meals is null ? NoContent() : Ok(meals);
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
                var meal = await _mealService.GetByIdAsync(id);
                return meal is null ? NoContent() : Ok(meal);
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
                var meal = await _mealService.GetByNameAsync(name);
                return meal is null ? NoContent() : Ok(meal);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(MealDto model) {
            try
            {
                var meal = await _mealService.Add(model);
                return Ok(meal);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MealDto model) {
            try
            {
                var meal = await _mealService.Update(id, model);
                return Ok(meal);
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
                await _mealService.Delete(id);
                return Ok(new { message = "Deleted" });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }
    }
}