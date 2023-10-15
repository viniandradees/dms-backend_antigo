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
    public class DietaryOptionFoodAttributeController : Controller
    {
        private readonly IDietaryOptionFoodAttributeService _dietaryOptionFoodAttributeService;
        private readonly IMapper _mapper;

        public DietaryOptionFoodAttributeController(IDietaryOptionFoodAttributeService dietaryOptionFoodAttributeService, IMapper mapper)
        {
            _dietaryOptionFoodAttributeService = dietaryOptionFoodAttributeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DietaryOptionFoodAttributeDto model)
        {
            try
            {
                var dietaryOptionFoodAttributeDto = _mapper.Map<DietaryOptionFoodAttributeDto>(model);
                var dietaryOptionFoodAttribute = await _dietaryOptionFoodAttributeService.Add(dietaryOptionFoodAttributeDto);
                return Ok(dietaryOptionFoodAttribute);
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
                await _dietaryOptionFoodAttributeService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}