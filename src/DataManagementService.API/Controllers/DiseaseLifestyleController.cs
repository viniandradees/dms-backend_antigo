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
    public class DiseaseLifestyleController : Controller
    {
        private readonly IDiseaseLifestyleService _diseaseLifestyleService;
        private readonly IMapper _mapper;

        public DiseaseLifestyleController(IDiseaseLifestyleService diseaseLifestyleService, IMapper mapper)
        {
            _diseaseLifestyleService = diseaseLifestyleService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseLifestyleDto model)
        {
            try
            {
                var diseaseLifestyleDto = _mapper.Map<DiseaseLifestyleDto>(model);
                var diseaseLifestyle = await _diseaseLifestyleService.Add(diseaseLifestyleDto);
                return Ok(diseaseLifestyle);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseLifestyleDto model) {
            try
            {
                var diseaseLifestyle = await _diseaseLifestyleService.Update(id, model);
                return diseaseLifestyle is null ? NoContent() : Ok(diseaseLifestyle);
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
                await _diseaseLifestyleService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}