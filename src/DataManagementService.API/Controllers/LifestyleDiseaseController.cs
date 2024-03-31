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
    public class LifestyleDiseaseController : Controller
    {
        private readonly ILifestyleDiseaseService _lifestyleDiseaseService;
        private readonly IMapper _mapper;

        public LifestyleDiseaseController(ILifestyleDiseaseService lifestyleDiseaseService, IMapper mapper)
        {
            _lifestyleDiseaseService = lifestyleDiseaseService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LifestyleDiseaseDto model)
        {
            try
            {
                var lifestyleDiseaseDto = _mapper.Map<LifestyleDiseaseDto>(model);
                var lifestyleDisease = await _lifestyleDiseaseService.Add(lifestyleDiseaseDto);
                return Ok(lifestyleDisease);
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
                await _lifestyleDiseaseService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}