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
    public class DiseaseFoodDosageAgeRangeController : Controller
    {
        private readonly IDiseaseFoodDosageAgeRangeService _diseaseFoodDosageAgeRangeService;
        private readonly IMapper _mapper;

        public DiseaseFoodDosageAgeRangeController(IDiseaseFoodDosageAgeRangeService diseaseFoodDosageAgeRangeService, IMapper mapper)
        {
            _diseaseFoodDosageAgeRangeService = diseaseFoodDosageAgeRangeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseFoodDosageAgeRangeDto model)
        {
            try
            {
                if (model.MinimumAge > model.MaximumAge) throw new DmsException("CTR-DFDAR01", 422, "Minimum age should be less than or equal to the maximum age.");
                if (model.MinimumAge < 0) throw new DmsException("CTR-DFDAR02", 422, "Age needs to be equal to or greater than 0.");
                if (model.DosageMin > model.DosageMax) throw new DmsException("CTR-DFDAR03", 422, "Minimum dosage should be less than or equal to the maximum dosage.");
                if (model.DosageMin <= 0) throw new DmsException("CTR-DFDAR04", 422, "Minimum dosage needs to be greater than 0.");

                var diseaseFoodDosageAgeRangeDto = _mapper.Map<DiseaseFoodDosageAgeRangeDto>(model);
                var diseaseFoodDosageAgeRange = await _diseaseFoodDosageAgeRangeService.Add(diseaseFoodDosageAgeRangeDto);
                return Ok(diseaseFoodDosageAgeRange);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseFoodDosageAgeRangeDto model) {
            try
            {
                if (model.MinimumAge > model.MaximumAge) throw new DmsException("CTR-DFDAR05", 422, "Minimum age should be less than or equal to the maximum age.");
                if (model.MinimumAge < 0) throw new DmsException("CTR-DFDAR066", 422, "Age needs to be equal to or greater than 0.");
                if (model.DosageMin > model.DosageMax) throw new DmsException("CTR-DFDAR07", 422, "Minimum dosage should be less than or equal to the maximum dosage.");
                if (model.DosageMin <= 0) throw new DmsException("CTR-DFDAR08", 422, "Minimum dosage needs to be greater than 0.");
                
                var diseaseFoodDosageAgeRange = await _diseaseFoodDosageAgeRangeService.Update(id, model);
                return diseaseFoodDosageAgeRange is null ? NoContent() : Ok(diseaseFoodDosageAgeRange);
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
                await _diseaseFoodDosageAgeRangeService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}