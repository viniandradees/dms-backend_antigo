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
    public class ExamSupplementDosageAgeRangeController : Controller
    {
        private readonly IExamSupplementDosageAgeRangeService _examSupplementDosageAgeRangeService;
        private readonly IMapper _mapper;

        public ExamSupplementDosageAgeRangeController(IExamSupplementDosageAgeRangeService examSupplementDosageAgeRangeService, IMapper mapper)
        {
            _examSupplementDosageAgeRangeService = examSupplementDosageAgeRangeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExamSupplementDosageAgeRangeDto model)
        {
            try
            {
                if (model.MinimumAge > model.MaximumAge) throw new DmsException("CTR-DEDAR01", 422, "Minimum age should be less than or equal to the maximum age.");
                if (model.MinimumAge < 0) throw new DmsException("CTR-DEDAR02", 422, "Age needs to be equal to or greater than 0.");
                if (model.DosageMin > model.DosageMax) throw new DmsException("CTR-DEDAR03", 422, "Minimum dosage should be less than or equal to the maximum dosage.");
                if (model.DosageMin <= 0) throw new DmsException("CTR-DEDAR04", 422, "Minimum dosage needs to be greater than 0.");

                var examSupplementDosageAgeRangeDto = _mapper.Map<ExamSupplementDosageAgeRangeDto>(model);
                var examSupplementDosageAgeRange = await _examSupplementDosageAgeRangeService.Add(examSupplementDosageAgeRangeDto);
                return Ok(examSupplementDosageAgeRange);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ExamSupplementDosageAgeRangeDto model) {
            try
            {
                if (model.MinimumAge > model.MaximumAge) throw new DmsException("CTR-DEDAR05", 422, "Minimum age should be less than or equal to the maximum age.");
                if (model.MinimumAge < 0) throw new DmsException("CTR-DEDAR066", 422, "Age needs to be equal to or greater than 0.");
                if (model.DosageMin > model.DosageMax) throw new DmsException("CTR-DEDAR07", 422, "Minimum dosage should be less than or equal to the maximum dosage.");
                if (model.DosageMin <= 0) throw new DmsException("CTR-DEDAR08", 422, "Minimum dosage needs to be greater than 0.");
                
                var examSupplementDosageAgeRange = await _examSupplementDosageAgeRangeService.Update(id, model);
                return examSupplementDosageAgeRange is null ? NoContent() : Ok(examSupplementDosageAgeRange);
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
                await _examSupplementDosageAgeRangeService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}