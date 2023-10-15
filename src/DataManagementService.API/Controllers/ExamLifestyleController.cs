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
    public class ExamLifestyleController : Controller
    {
        private readonly IExamLifestyleService _examLifestyleService;
        private readonly IMapper _mapper;

        public ExamLifestyleController(IExamLifestyleService examLifestyleService, IMapper mapper)
        {
            _examLifestyleService = examLifestyleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExamLifestyleDto model)
        {
            try
            {
                var examLifestyleDto = _mapper.Map<ExamLifestyleDto>(model);
                var examLifestyle = await _examLifestyleService.Add(examLifestyleDto);
                return Ok(examLifestyle);
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
                await _examLifestyleService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}