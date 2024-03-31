using AutoMapper;
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
    public class MealFoodController : Controller
    {
        private readonly IMealFoodService _mealFoodService;
        private readonly IMapper _mapper;

        public MealFoodController(IMealFoodService mealFoodService, IMapper mapper)
        {
            _mealFoodService = mealFoodService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MealFoodDto model)
        {
            try
            {
                var mealFoodDto = _mapper.Map<MealFoodDto>(model);
                var mealFood = await _mealFoodService.Add(mealFoodDto);
                return Ok(mealFood);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MealFoodDto model) {
            try
            {
                var mealFood = await _mealFoodService.Update(id, model);
                return mealFood is null ? NoContent() : Ok(mealFood);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mealFoodService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}