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
    public class DiseaseDiseaseController : Controller
    {
        private readonly IDiseaseDiseaseService _diseaseDiseaseService;
        private readonly IMapper _mapper;

        public DiseaseDiseaseController(IDiseaseDiseaseService diseaseDiseaseService, IMapper mapper)
        {
            _diseaseDiseaseService = diseaseDiseaseService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseDiseaseDto model)
        {
            try
            {
                var diseaseDiseaseDto = _mapper.Map<DiseaseDiseaseDto>(model);
                var diseaseDisease = await _diseaseDiseaseService.Add(diseaseDiseaseDto);
                return Ok(diseaseDisease);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DiseaseDiseaseDto model) {
            try
            {
                var diseaseDisease = await _diseaseDiseaseService.Update(id, model);
                return diseaseDisease is null ? NoContent() : Ok(diseaseDisease);
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
                await _diseaseDiseaseService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}