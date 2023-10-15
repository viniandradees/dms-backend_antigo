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
    public class DiseaseExamController : Controller
    {
        private readonly IDiseaseExamService _diseaseExamService;
        private readonly IMapper _mapper;

        public DiseaseExamController(IDiseaseExamService diseaseExamService, IMapper mapper)
        {
            _diseaseExamService = diseaseExamService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseExamDto model)
        {
            try
            {
                var diseaseExamDto = _mapper.Map<DiseaseExamDto>(model);
                var diseaseExam = await _diseaseExamService.Add(diseaseExamDto);
                return Ok(diseaseExam);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseExamDto model) {
            try
            {
                var diseaseExam = await _diseaseExamService.Update(id, model);
                return diseaseExam is null ? NoContent() : Ok(diseaseExam);
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
                await _diseaseExamService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}