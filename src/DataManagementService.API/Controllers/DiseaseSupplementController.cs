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
    public class DiseaseSupplementController : Controller
    {
        private readonly IDiseaseSupplementService _diseaseSupplementService;
        private readonly IMapper _mapper;

        public DiseaseSupplementController(IDiseaseSupplementService diseaseSupplementService, IMapper mapper)
        {
            _diseaseSupplementService = diseaseSupplementService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseSupplementDto model)
        {
            try
            {
                var diseaseSupplementDto = _mapper.Map<DiseaseSupplementDto>(model);
                var diseaseSupplement = await _diseaseSupplementService.Add(diseaseSupplementDto);
                return Ok(diseaseSupplement);
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
                await _diseaseSupplementService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}