using AutoMapper;
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
    public class MealDietaryOptionController : Controller
    {
        private readonly IMealDietaryOptionService _mealDietaryOptionService;
        private readonly IMapper _mapper;

        public MealDietaryOptionController(IMealDietaryOptionService mealDietaryOptionService, IMapper mapper)
        {
            _mealDietaryOptionService = mealDietaryOptionService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MealDietaryOptionDto model)
        {
            try
            {
                var mealDietaryOptionDto = _mapper.Map<MealDietaryOptionDto>(model);
                var mealDietaryOption = await _mealDietaryOptionService.Add(mealDietaryOptionDto);
                return Ok(mealDietaryOption);
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
                await _mealDietaryOptionService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}