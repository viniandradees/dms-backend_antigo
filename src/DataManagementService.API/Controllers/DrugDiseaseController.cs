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
    public class DrugDiseaseController : Controller
    {
        private readonly IDrugDiseaseService _drugDiseaseService;
        private readonly IMapper _mapper;

        public DrugDiseaseController(IDrugDiseaseService drugDiseaseService, IMapper mapper)
        {
            _drugDiseaseService = drugDiseaseService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(DrugDiseaseDto model)
        {
            try
            {
                var drugDiseaseDto = _mapper.Map<DrugDiseaseDto>(model);
                var drugDisease = await _drugDiseaseService.Add(drugDiseaseDto);
                return Ok(drugDisease);
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
                await _drugDiseaseService.Delete(id);
                return Ok(new { message = "Deleted." });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new { code = ex.Code, message = ex.Message });
            }
        }
    }
}