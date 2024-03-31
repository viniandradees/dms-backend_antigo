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
    public class FoodSupplementController : Controller
    {
        private readonly IFoodSupplementService _foodSupplementService;
        private readonly IMapper _mapper;

        public FoodSupplementController(IFoodSupplementService foodSupplementService, IMapper mapper)
        {
            _foodSupplementService = foodSupplementService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(FoodSupplementDto model)
        {
            try
            {
                var foodSupplementDto = _mapper.Map<FoodSupplementDto>(model);
                var foodSupplement = await _foodSupplementService.Add(foodSupplementDto);
                return Ok(foodSupplement);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, FoodSupplementDto model) {
            try
            {
                var foodSupplement = await _foodSupplementService.Update(id, model);
                return foodSupplement is null ? NoContent() : Ok(foodSupplement);
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
                await _foodSupplementService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}