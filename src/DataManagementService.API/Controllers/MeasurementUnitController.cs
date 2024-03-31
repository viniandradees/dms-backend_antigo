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
    public class MeasurementUnitController : Controller
    {
        private readonly IMeasurementUnitService _measurementUnitService;
        public MeasurementUnitController(IMeasurementUnitService dataService)
        {
            _measurementUnitService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool getFullData = false) {
            try
            {
                var measurementUnits = await _measurementUnitService.GetAllAsync(getFullData);
                return measurementUnits is null ? NoContent() : Ok(measurementUnits);
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
                var measurementUnit = await _measurementUnitService.GetByIdAsync(id);
                return measurementUnit is null ? NoContent() : Ok(measurementUnit);
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
                var measurementUnit = await _measurementUnitService.GetByNameAsync(name);
                return measurementUnit is null ? NoContent() : Ok(measurementUnit);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(MeasurementUnitDto model) {
            try
            {
                var measurementUnit = await _measurementUnitService.Add(model);
                return Ok(measurementUnit);
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MeasurementUnitDto model) {
            try
            {
                var measurementUnit = await _measurementUnitService.Update(id, model);
                return Ok(measurementUnit);
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
                await _measurementUnitService.Delete(id);
                return Ok(new { message = "Deleted" });
            }
            catch (DmsException ex)
            {
                return this.StatusCode(ex.StatusCode, new {code = ex.Code, message = ex.Message});
            }
        }
    }
}