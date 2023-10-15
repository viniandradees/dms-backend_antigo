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
    public class FoodRelatedAttributeController : Controller
    {
        private readonly IFoodRelatedAttributeService _foodRelatedAttributeService;
        private readonly IMapper _mapper;

        public FoodRelatedAttributeController(IFoodRelatedAttributeService foodRelatedAttributeService, IMapper mapper)
        {
            _foodRelatedAttributeService = foodRelatedAttributeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(FoodRelatedAttributeDto model)
        {
            try
            {
                var foodRelatedAttributeDto = _mapper.Map<FoodRelatedAttributeDto>(model);
                var foodRelatedAttribute = await _foodRelatedAttributeService.Add(foodRelatedAttributeDto);
                return Ok(foodRelatedAttribute);
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
                await _foodRelatedAttributeService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}