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
    public class ExamSupplementDosageController : Controller
    {
        private readonly IExamSupplementDosageService _examSupplementDosageService;
        private readonly IMapper _mapper;

        public ExamSupplementDosageController(IExamSupplementDosageService examSupplementDosageService, IMapper mapper)
        {
            _examSupplementDosageService = examSupplementDosageService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExamSupplementDosageDto model)
        {
            try
            {
                var examSupplementDosageDto = _mapper.Map<ExamSupplementDosageDto>(model);
                var examSupplementDosage = await _examSupplementDosageService.Add(examSupplementDosageDto);
                return Ok(examSupplementDosage);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ExamSupplementDosageDto model) {
            try
            {
                var examSupplementDosage = await _examSupplementDosageService.Update(id, model);
                return examSupplementDosage is null ? NoContent() : Ok(examSupplementDosage);
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
                await _examSupplementDosageService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}