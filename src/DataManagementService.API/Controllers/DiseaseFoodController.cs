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
    public class DiseaseFoodController : Controller
    {
        private readonly IDiseaseFoodService _diseaseFoodService;
        private readonly IMapper _mapper;

        public DiseaseFoodController(IDiseaseFoodService diseaseFoodService, IMapper mapper)
        {
            _diseaseFoodService = diseaseFoodService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseFoodDto model)
        {
            try
            {
                var diseaseFoodDto = _mapper.Map<DiseaseFoodDto>(model);
                var diseaseFood = await _diseaseFoodService.Add(diseaseFoodDto);
                return Ok(diseaseFood);
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
                await _diseaseFoodService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}