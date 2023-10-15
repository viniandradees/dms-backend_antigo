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
    public class ExamSupplementController : Controller
    {
        private readonly IExamSupplementService _examSupplementService;
        private readonly IMapper _mapper;

        public ExamSupplementController(IExamSupplementService examSupplementService, IMapper mapper)
        {
            _examSupplementService = examSupplementService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExamSupplementDto model)
        {
            try
            {
                var examSupplementDto = _mapper.Map<ExamSupplementDto>(model);
                var examSupplement = await _examSupplementService.Add(examSupplementDto);
                return Ok(examSupplement);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ExamSupplementDto model) {
            try
            {
                var examSupplement = await _examSupplementService.Update(id, model);
                return examSupplement is null ? NoContent() : Ok(examSupplement);
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
                await _examSupplementService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}