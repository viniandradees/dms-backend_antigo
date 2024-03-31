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
    public class MealPeriodController : Controller
    {
        private readonly IMealPeriodService _mealPeriodService;
        private readonly IMapper _mapper;

        public MealPeriodController(IMealPeriodService mealPeriodService, IMapper mapper)
        {
            _mealPeriodService = mealPeriodService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MealPeriodDto model)
        {
            try
            {
                var mealPeriodDto = _mapper.Map<MealPeriodDto>(model);
                var mealPeriod = await _mealPeriodService.Add(mealPeriodDto);
                return Ok(mealPeriod);
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
                await _mealPeriodService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}