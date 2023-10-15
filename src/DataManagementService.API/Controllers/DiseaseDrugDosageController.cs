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
    public class DiseaseDrugDosageController : Controller
    {
        private readonly IDiseaseDrugDosageService _diseaseDrugDosageService;
        private readonly IMapper _mapper;

        public DiseaseDrugDosageController(IDiseaseDrugDosageService diseaseDrugDosageService, IMapper mapper)
        {
            _diseaseDrugDosageService = diseaseDrugDosageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseDrugDosageDto model)
        {
            try
            {
                var diseaseDrugDosageDto = _mapper.Map<DiseaseDrugDosageDto>(model);
                var diseaseDrugDosage = await _diseaseDrugDosageService.Add(diseaseDrugDosageDto);
                return Ok(diseaseDrugDosage);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseDrugDosageDto model) {
            try
            {
                var diseaseDrugDosage = await _diseaseDrugDosageService.Update(id, model);
                return diseaseDrugDosage is null ? NoContent() : Ok(diseaseDrugDosage);
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
                await _diseaseDrugDosageService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}