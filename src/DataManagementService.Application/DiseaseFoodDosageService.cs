
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseFoodDosageService : IDiseaseFoodDosageService
    {
        private readonly IDiseaseFoodDosagePersistence _diseaseFoodDosagePersistence;
        private readonly IDiseaseFoodPersistence _diseaseFoodPersistence;
        private readonly IMapper _mapper;
        public DiseaseFoodDosageService(IDiseaseFoodDosagePersistence diseaseFoodDosagePersistence, IDiseaseFoodPersistence diseaseFoodPersistence, IMapper mapper)
        {
            _diseaseFoodDosagePersistence = diseaseFoodDosagePersistence;
            _diseaseFoodPersistence = diseaseFoodPersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseFoodDosageDto> Add(DiseaseFoodDosageDto model)
        {
            try
            {
                var diseaseFood = await _diseaseFoodPersistence.GetByIdAsync(model.DiseaseFoodId);
                if (diseaseFood is null) throw new DmsException("AP-DFD01", 400, "This treatment food does not exist.");

                var checkIfExists = await _diseaseFoodDosagePersistence.GetByRelatedIdAsync(model.DiseaseFoodId, model.MeasurementUnitId);
                if (checkIfExists is not null) throw new DmsException("AP-DFD02", 409, "This dosage already exists.");

                var toAddDiseaseFoodDosage = _mapper.Map<DiseaseFoodDosage>(model);
                _diseaseFoodDosagePersistence.Add(toAddDiseaseFoodDosage);

                var save = await _diseaseFoodDosagePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DFD03", 500, "Unexpected error saving this food dosage from the database.");

                var newDiseaseFoodDosage = await _diseaseFoodDosagePersistence.GetByRelatedIdAsync(model.DiseaseFoodId, model.MeasurementUnitId);
                if (newDiseaseFoodDosage is null) throw new DmsException("AP-DFD04", 500, "Unexpected error adding food dosage.");
                return _mapper.Map<DiseaseFoodDosageDto>(newDiseaseFoodDosage);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DFD05", 500, $"Unexpected error adding food dosage: {ex.Message}");
            }
            
        }

        public async Task<DiseaseFoodDosageDto> Update(int id, DiseaseFoodDosageDto model)
        {
            try
            {
                var checkDiseaseFoodDosage = await _diseaseFoodDosagePersistence.GetByIdAsync(id);
                if (checkDiseaseFoodDosage is null) throw new DmsException("AP-DFD06", 404, "This food dosage does not exist.");

                model.Id = id;

                var diseaseFoodDosage = _mapper.Map<DiseaseFoodDosage>(model);
                _diseaseFoodDosagePersistence.Update(diseaseFoodDosage);

                var save = await _diseaseFoodDosagePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DFD07", 500, "Unexpected error updating this dosage from the database.");
                
                var result = await _diseaseFoodDosagePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DFD08", 500, "Unexpected error updating dosage.");
                return _mapper.Map<DiseaseFoodDosageDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DFD09", 500, $"Unexpected error updating dosage: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseFoodDosage = await _diseaseFoodDosagePersistence.GetByIdAsync(id);
                if (diseaseFoodDosage is null) throw new DmsException("AP-DFD10", 404, "This food dosage does not exist.");

                _diseaseFoodDosagePersistence.Delete<DiseaseFoodDosage>(diseaseFoodDosage);
                var save = await _diseaseFoodDosagePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DFD11", 500, "Unexpected error deleting this food dosage from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DFD12", 500, $"Unexpected error deleting food dosage: {ex.Message}");
            }
        }
    }
}