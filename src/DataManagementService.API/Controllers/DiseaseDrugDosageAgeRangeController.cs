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
    public class DiseaseDrugDosageAgeRangeController : Controller
    {
        private readonly IDiseaseDrugDosageAgeRangeService _diseaseDrugDosageAgeRangeService;
        private readonly IMapper _mapper;

        public DiseaseDrugDosageAgeRangeController(IDiseaseDrugDosageAgeRangeService diseaseDrugDosageAgeRangeService, IMapper mapper)
        {
            _diseaseDrugDosageAgeRangeService = diseaseDrugDosageAgeRangeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseDrugDosageAgeRangeDto model)
        {
            try
            {
                if (model.MinimumAge > model.MaximumAge) throw new DmsException("CTR-DDDAR01", 422, "Minimum age should be less than or equal to the maximum age.");
                if (model.MinimumAge < 0) throw new DmsException("CTR-DDDAR02", 422, "Age needs to be equal to or greater than 0.");
                if (model.DosageMin > model.DosageMax) throw new DmsException("CTR-DDDAR03", 422, "Minimum dosage should be less than or equal to the maximum dosage.");
                if (model.DosageMin <= 0) throw new DmsException("CTR-DDDAR04", 422, "Minimum dosage needs to be greater than 0.");

                var diseaseDrugDosageAgeRangeDto = _mapper.Map<DiseaseDrugDosageAgeRangeDto>(model);
                var diseaseDrugDosageAgeRange = await _diseaseDrugDosageAgeRangeService.Add(diseaseDrugDosageAgeRangeDto);
                return Ok(diseaseDrugDosageAgeRange);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseDrugDosageAgeRangeDto model) {
            try
            {
                if (model.MinimumAge > model.MaximumAge) throw new DmsException("CTR-DDDAR05", 422, "Minimum age should be less than or equal to the maximum age.");
                if (model.MinimumAge < 0) throw new DmsException("CTR-DDDAR066", 422, "Age needs to be equal to or greater than 0.");
                if (model.DosageMin > model.DosageMax) throw new DmsException("CTR-DDDAR07", 422, "Minimum dosage should be less than or equal to the maximum dosage.");
                if (model.DosageMin <= 0) throw new DmsException("CTR-DDDAR08", 422, "Minimum dosage needs to be greater than 0.");
                
                var diseaseDrugDosageAgeRange = await _diseaseDrugDosageAgeRangeService.Update(id, model);
                return diseaseDrugDosageAgeRange is null ? NoContent() : Ok(diseaseDrugDosageAgeRange);
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
                await _diseaseDrugDosageAgeRangeService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}