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
    public class DiseaseFoodDosageController : Controller
    {
        private readonly IDiseaseFoodDosageService _diseaseFoodDosageService;
        private readonly IMapper _mapper;

        public DiseaseFoodDosageController(IDiseaseFoodDosageService diseaseFoodDosageService, IMapper mapper)
        {
            _diseaseFoodDosageService = diseaseFoodDosageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseFoodDosageDto model)
        {
            try
            {
                var diseaseFoodDosageDto = _mapper.Map<DiseaseFoodDosageDto>(model);
                var diseaseFoodDosage = await _diseaseFoodDosageService.Add(diseaseFoodDosageDto);
                return Ok(diseaseFoodDosage);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseFoodDosageDto model) {
            try
            {
                var diseaseFoodDosage = await _diseaseFoodDosageService.Update(id, model);
                return diseaseFoodDosage is null ? NoContent() : Ok(diseaseFoodDosage);
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
                await _diseaseFoodDosageService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}