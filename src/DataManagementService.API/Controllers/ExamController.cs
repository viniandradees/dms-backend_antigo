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
    public class ExamController : Controller
    {
        private readonly IExamService _examService;
        public ExamController(IExamService dataService)
        {
            _examService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool getFullData = false) {
            try
            {
                var exams = await _examService.GetAllAsync(getFullData);
                return exams is null ? NoContent() : Ok(exams);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) {
            try
            {
                var exam = await _examService.GetByIdAsync(id);
                return exam is null ? NoContent() : Ok(exam);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetByName(string name) {
            try
            {
                var exam = await _examService.GetByNameAsync(name);
                return exam is null ? NoContent() : Ok(exam);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExamDto model) {
            try
            {
                var exam = await _examService.Add(model);
                return Ok(exam);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ExamDto model) {
            try
            {
                var exam = await _examService.Update(id, model);
                return Ok(exam);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try
            {
                await _examService.Delete(id);
                return Ok(new { message = "Deleted" });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }
    }
}