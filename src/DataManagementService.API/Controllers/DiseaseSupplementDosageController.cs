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
    public class DiseaseSupplementDosageController : Controller
    {
        private readonly IDiseaseSupplementDosageService _diseaseSupplementDosageService;
        private readonly IMapper _mapper;

        public DiseaseSupplementDosageController(IDiseaseSupplementDosageService diseaseSupplementDosageService, IMapper mapper)
        {
            _diseaseSupplementDosageService = diseaseSupplementDosageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseSupplementDosageDto model)
        {
            try
            {
                var diseaseSupplementDosageDto = _mapper.Map<DiseaseSupplementDosageDto>(model);
                var diseaseSupplementDosage = await _diseaseSupplementDosageService.Add(diseaseSupplementDosageDto);
                return Ok(diseaseSupplementDosage);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseSupplementDosageDto model) {
            try
            {
                var diseaseSupplementDosage = await _diseaseSupplementDosageService.Update(id, model);
                return diseaseSupplementDosage is null ? NoContent() : Ok(diseaseSupplementDosage);
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
                await _diseaseSupplementDosageService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}