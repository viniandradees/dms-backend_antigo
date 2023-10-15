
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class MeasurementUnitService : IMeasurementUnitService
    {
        private readonly IMeasurementUnitPersistence _measurementUnitPersistence;
        private readonly IMapper _mapper;
        public MeasurementUnitService(IMeasurementUnitPersistence measurementUnitPersistence, IMapper mapper)
        {
            _measurementUnitPersistence = measurementUnitPersistence;
            _mapper = mapper;
            
        }

        public async Task<MeasurementUnitDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var measurementUnits = await _measurementUnitPersistence.GetAllAsync(getFullData);
                if (measurementUnits is null) return null;

                return _mapper.Map<MeasurementUnitDto[]>(measurementUnits);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-MU01", 500, $"Unexpected error getting measurementUnits: {ex.Message}");
            }
        }

        public async Task<MeasurementUnitDto> GetByIdAsync(int id)
        {
            try
            {
                var measurementUnits = await _measurementUnitPersistence.GetByIdAsync(id);
                if (measurementUnits is null) return null;

                return _mapper.Map<MeasurementUnitDto>(measurementUnits);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-MU02", 500, $"Unexpected error getting measurementUnit: {ex.Message}");
            }
        }

        public async Task<MeasurementUnitDto> GetByNameAsync(string name)
        {
            try
            {
                var measurementUnits = await _measurementUnitPersistence.GetByNameAsync(name);
                if (measurementUnits is null) return null;

                return _mapper.Map<MeasurementUnitDto>(measurementUnits);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-MU03", 500, $"Unexpected error getting measurement unit: {ex.Message}");
            }
        }
        public async Task<MeasurementUnitDto> Add(MeasurementUnitDto model)
        {
            try
            {
                var measurementUnit = _mapper.Map<MeasurementUnit>(model);
                _measurementUnitPersistence.Add<MeasurementUnit>(measurementUnit);

                var checkName = await _measurementUnitPersistence.GetByNameAsync(measurementUnit.Name);
                if (checkName is not null) throw new DmsException("AP-MU04", 409, "Measurement unit name already exists.");
                
                var save = await _measurementUnitPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-MU05", 500, "Unexpected error adding this measurement unit from the database.");

                var measurementUnitDto = await _measurementUnitPersistence.GetByIdAsync(measurementUnit.Id);
                if (measurementUnitDto is null) throw new DmsException("AP-MU06", 500, "Unexpected error adding measurement unit.");

                return _mapper.Map<MeasurementUnitDto>(measurementUnitDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-MU07", 500, $"Unexpected error adding measurement unit: {ex.Message}");
            }
            
        }

        public async Task<MeasurementUnitDto> Update(int id, MeasurementUnitDto model)
        {
            try
            {
                var checkMeasurementUnit = await _measurementUnitPersistence.GetByIdAsync(id);
                if (checkMeasurementUnit is null) throw new DmsException("AP-MU08", 404, "This measurement unit does not exist.");

                model.Id = id;

                if (checkMeasurementUnit.Name != model.Name) {
                    var checkName = await _measurementUnitPersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-MU09", 409, "Measurement unit name already exists.");
                }

                var measurementUnit = _mapper.Map<MeasurementUnit>(model);
                _measurementUnitPersistence.Update<MeasurementUnit>(measurementUnit);

                var save = await _measurementUnitPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-MU10", 500, "Unexpected error updating this measurement unit from the database.");
                
                var result = await _measurementUnitPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-MU11", 500, "Unexpected error updating measurement unit.");
                return _mapper.Map<MeasurementUnitDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-MU12", 500, $"Unexpected error updating measurement unit: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var measurementUnit = await _measurementUnitPersistence.GetByIdAsync(id);
                if (measurementUnit is null) throw new DmsException("AP-MU13", 404, "This measurement unit does not exist.");

                _measurementUnitPersistence.Delete<MeasurementUnit>(measurementUnit);
                return await _measurementUnitPersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-MU14", 500, $"Unexpected error deleting measurement unit: {ex.Message}");
            }
        }
    }
}