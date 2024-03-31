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
    public class ExamResultReferenceController : Controller
    {
        private readonly IExamResultReferenceService _examResultReferenceService;
        private readonly IMapper _mapper;

        public ExamResultReferenceController(IExamResultReferenceService examResultReferenceService, IMapper mapper)
        {
            _examResultReferenceService = examResultReferenceService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExamResultReferenceDto model)
        {
            try
            {
                var examResultReferenceDto = _mapper.Map<ExamResultReferenceDto>(model);
                var examResultReference = await _examResultReferenceService.Add(examResultReferenceDto);
                return Ok(examResultReference);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ExamResultReferenceDto model) {
            try
            {
                var examResultReference = await _examResultReferenceService.Update(id, model);
                return examResultReference is null ? NoContent() : Ok(examResultReference);
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
                await _examResultReferenceService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}