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
    public class SupplementDiseaseController : Controller
    {
        private readonly ISupplementDiseaseService _supplementDiseaseService;
        private readonly IMapper _mapper;

        public SupplementDiseaseController(ISupplementDiseaseService supplementDiseaseService, IMapper mapper)
        {
            _supplementDiseaseService = supplementDiseaseService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SupplementDiseaseDto model)
        {
            try
            {
                var supplementDiseaseDto = _mapper.Map<SupplementDiseaseDto>(model);
                var supplementDisease = await _supplementDiseaseService.Add(supplementDiseaseDto);
                return Ok(supplementDisease);
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
                await _supplementDiseaseService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}