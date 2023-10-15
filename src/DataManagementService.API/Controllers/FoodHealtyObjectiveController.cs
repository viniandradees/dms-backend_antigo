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
    public class FoodHealtyObjectiveController : Controller
    {
        private readonly IFoodHealtyObjectiveService _foodHealtyObjectiveService;
        private readonly IMapper _mapper;

        public FoodHealtyObjectiveController(IFoodHealtyObjectiveService foodHealtyObjectiveService, IMapper mapper)
        {
            _foodHealtyObjectiveService = foodHealtyObjectiveService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(FoodHealtyObjectiveDto model)
        {
            try
            {
                var foodHealtyObjectiveDto = _mapper.Map<FoodHealtyObjectiveDto>(model);
                var foodHealtyObjective = await _foodHealtyObjectiveService.Add(foodHealtyObjectiveDto);
                return Ok(foodHealtyObjective);
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
                await _foodHealtyObjectiveService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}