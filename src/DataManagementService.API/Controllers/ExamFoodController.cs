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
    public class ExamFoodController : Controller
    {
        private readonly IExamFoodService _examFoodService;
        private readonly IMapper _mapper;

        public ExamFoodController(IExamFoodService examFoodService, IMapper mapper)
        {
            _examFoodService = examFoodService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExamFoodDto model)
        {
            try
            {
                var examFoodDto = _mapper.Map<ExamFoodDto>(model);
                var examFood = await _examFoodService.Add(examFoodDto);
                return Ok(examFood);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ExamFoodDto model) {
            try
            {
                var examFood = await _examFoodService.Update(id, model);
                return examFood is null ? NoContent() : Ok(examFood);
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
                await _examFoodService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}