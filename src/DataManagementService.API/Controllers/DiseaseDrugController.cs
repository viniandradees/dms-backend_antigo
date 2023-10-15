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
    public class DiseaseDrugController : Controller
    {
        private readonly IDiseaseDrugService _diseaseDrugService;
        private readonly IMapper _mapper;

        public DiseaseDrugController(IDiseaseDrugService diseaseDrugService, IMapper mapper)
        {
            _diseaseDrugService = diseaseDrugService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DiseaseDrugDto model)
        {
            try
            {
                var diseaseDrugDto = _mapper.Map<DiseaseDrugDto>(model);
                var diseaseDrug = await _diseaseDrugService.Add(diseaseDrugDto);
                return Ok(diseaseDrug);
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
                await _diseaseDrugService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}