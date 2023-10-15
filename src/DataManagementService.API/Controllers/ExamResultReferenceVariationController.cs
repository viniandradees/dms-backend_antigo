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
    public class ExamResultReferenceVariationController : Controller
    {
        private readonly IExamResultReferenceVariationService _examResultReferenceVariationService;
        private readonly IMapper _mapper;

        public ExamResultReferenceVariationController(IExamResultReferenceVariationService examResultReferenceVariationService, IMapper mapper)
        {
            _examResultReferenceVariationService = examResultReferenceVariationService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExamResultReferenceVariationDto model)
        {
            try
            {
                var examResultReferenceVariationDto = _mapper.Map<ExamResultReferenceVariationDto>(model);
                var examResultReferenceVariation = await _examResultReferenceVariationService.Add(examResultReferenceVariationDto);
                return Ok(examResultReferenceVariation);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ExamResultReferenceVariationDto model) {
            try
            {
                var examResultReferenceVariation = await _examResultReferenceVariationService.Update(id, model);
                return examResultReferenceVariation is null ? NoContent() : Ok(examResultReferenceVariation);
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
                await _examResultReferenceVariationService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}