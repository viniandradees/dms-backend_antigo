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
    public class MealCountryController : Controller
    {
        private readonly IMealCountryService _mealCountryService;
        private readonly IMapper _mapper;

        public MealCountryController(IMealCountryService mealCountryService, IMapper mapper)
        {
            _mealCountryService = mealCountryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(MealCountryDto model)
        {
            try
            {
                var mealCountryDto = _mapper.Map<MealCountryDto>(model);
                var mealCountry = await _mealCountryService.Add(mealCountryDto);
                return Ok(mealCountry);
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
                await _mealCountryService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}