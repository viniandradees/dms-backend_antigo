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
    public class DiseaseSupplementDosageAgeRangeController : Controller
    {
        private readonly IDiseaseSupplementDosageAgeRangeService _diseaseSupplementDosageAgeRangeService;
        private readonly IMapper _mapper;

        public DiseaseSupplementDosageAgeRangeController(IDiseaseSupplementDosageAgeRangeService diseaseSupplementDosageAgeRangeService, IMapper mapper)
        {
            _diseaseSupplementDosageAgeRangeService = diseaseSupplementDosageAgeRangeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseSupplementDosageAgeRangeDto model)
        {
            try
            {
                if (model.MinimumAge > model.MaximumAge) throw new DmsException("CTR-DSDAR01", 422, "Minimum age should be less than or equal to the maximum age.");
                if (model.MinimumAge < 0) throw new DmsException("CTR-DSDAR02", 422, "Age needs to be equal to or greater than 0.");
                if (model.DosageMin > model.DosageMax) throw new DmsException("CTR-DSDAR03", 422, "Minimum dosage should be less than or equal to the maximum dosage.");
                if (model.DosageMin <= 0) throw new DmsException("CTR-DSDAR04", 422, "Minimum dosage needs to be greater than 0.");

                var diseaseSupplementDosageAgeRangeDto = _mapper.Map<DiseaseSupplementDosageAgeRangeDto>(model);
                var diseaseSupplementDosageAgeRange = await _diseaseSupplementDosageAgeRangeService.Add(diseaseSupplementDosageAgeRangeDto);
                return Ok(diseaseSupplementDosageAgeRange);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseSupplementDosageAgeRangeDto model) {
            try
            {
                if (model.MinimumAge > model.MaximumAge) throw new DmsException("CTR-DSDAR05", 422, "Minimum age should be less than or equal to the maximum age.");
                if (model.MinimumAge < 0) throw new DmsException("CTR-DSDAR066", 422, "Age needs to be equal to or greater than 0.");
                if (model.DosageMin > model.DosageMax) throw new DmsException("CTR-DSDAR07", 422, "Minimum dosage should be less than or equal to the maximum dosage.");
                if (model.DosageMin <= 0) throw new DmsException("CTR-DSDAR08", 422, "Minimum dosage needs to be greater than 0.");
                
                var diseaseSupplementDosageAgeRange = await _diseaseSupplementDosageAgeRangeService.Update(id, model);
                return diseaseSupplementDosageAgeRange is null ? NoContent() : Ok(diseaseSupplementDosageAgeRange);
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
                await _diseaseSupplementDosageAgeRangeService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}