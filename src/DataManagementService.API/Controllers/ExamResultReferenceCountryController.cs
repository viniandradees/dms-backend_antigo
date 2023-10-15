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
    public class ExamResultReferenceCountryController : Controller
    {
        private readonly IExamResultReferenceCountryService _examResultReferenceCountryService;
        private readonly IMapper _mapper;

        public ExamResultReferenceCountryController(IExamResultReferenceCountryService examResultReferenceCountryService, IMapper mapper)
        {
            _examResultReferenceCountryService = examResultReferenceCountryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExamResultReferenceCountryDto model)
        {
            try
            {
                var examResultReferenceCountryDto = _mapper.Map<ExamResultReferenceCountryDto>(model);
                var examResultReferenceCountry = await _examResultReferenceCountryService.Add(examResultReferenceCountryDto);
                return Ok(examResultReferenceCountry);
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
                await _examResultReferenceCountryService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}