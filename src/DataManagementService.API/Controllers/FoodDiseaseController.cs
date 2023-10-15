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
    public class FoodDiseaseController : Controller
    {
        private readonly IFoodDiseaseService _foodDiseaseService;
        private readonly IMapper _mapper;

        public FoodDiseaseController(IFoodDiseaseService foodDiseaseService, IMapper mapper)
        {
            _foodDiseaseService = foodDiseaseService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(FoodDiseaseDto model)
        {
            try
            {
                var foodDiseaseDto = _mapper.Map<FoodDiseaseDto>(model);
                var foodDisease = await _foodDiseaseService.Add(foodDiseaseDto);
                return Ok(foodDisease);
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
                await _foodDiseaseService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}