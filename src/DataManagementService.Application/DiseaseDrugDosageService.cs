
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseDrugDosageService : IDiseaseDrugDosageService
    {
        private readonly IDiseaseDrugDosagePersistence _diseaseDrugDosagePersistence;
        private readonly IDiseaseDrugPersistence _diseaseDrugPersistence;
        private readonly IMapper _mapper;
        public DiseaseDrugDosageService(IDiseaseDrugDosagePersistence diseaseDrugDosagePersistence, IDiseaseDrugPersistence diseaseDrugPersistence, IMapper mapper)
        {
            _diseaseDrugDosagePersistence = diseaseDrugDosagePersistence;
            _diseaseDrugPersistence = diseaseDrugPersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseDrugDosageDto> Add(DiseaseDrugDosageDto model)
        {
            try
            {
                var diseaseDrug = await _diseaseDrugPersistence.GetByIdAsync(model.DiseaseDrugId);
                if (diseaseDrug is null) throw new DmsException("AP-DDD01", 400, "This treatment drug does not exist.");

                var checkIfExists = await _diseaseDrugDosagePersistence.GetByRelatedIdAsync(model.DiseaseDrugId, model.MeasurementUnitId);
                if (checkIfExists is not null) throw new DmsException("AP-DDD02", 409, "This dosage already exists.");

                var toAddDiseaseDrugDosage = _mapper.Map<DiseaseDrugDosage>(model);
                _diseaseDrugDosagePersistence.Add(toAddDiseaseDrugDosage);

                var save = await _diseaseDrugDosagePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DDD03", 500, "Unexpected error saving this drug dosage from the database.");

                var newDiseaseDrugDosage = await _diseaseDrugDosagePersistence.GetByRelatedIdAsync(model.DiseaseDrugId, model.MeasurementUnitId);
                if (newDiseaseDrugDosage is null) throw new DmsException("AP-DDD04", 500, "Unexpected error adding drug dosage.");
                return _mapper.Map<DiseaseDrugDosageDto>(newDiseaseDrugDosage);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DDD05", 500, $"Unexpected error adding drug dosage: {ex.Message}");
            }
            
        }

        public async Task<DiseaseDrugDosageDto> Update(int id, DiseaseDrugDosageDto model)
        {
            try
            {
                var checkDiseaseDrugDosage = await _diseaseDrugDosagePersistence.GetByIdAsync(id);
                if (checkDiseaseDrugDosage is null) throw new DmsException("AP-DDD06", 404, "This drug dosage does not exist.");

                model.Id = id;

                var diseaseDrugDosage = _mapper.Map<DiseaseDrugDosage>(model);
                _diseaseDrugDosagePersistence.Update(diseaseDrugDosage);

                var save = await _diseaseDrugDosagePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DDD07", 500, "Unexpected error updating this dosage from the database.");
                
                var result = await _diseaseDrugDosagePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DDD08", 500, "Unexpected error updating dosage.");
                return _mapper.Map<DiseaseDrugDosageDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DDD09", 500, $"Unexpected error updating dosage: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseDrugDosage = await _diseaseDrugDosagePersistence.GetByIdAsync(id);
                if (diseaseDrugDosage is null) throw new DmsException("AP-DDD10", 404, "This drug dosage does not exist.");

                _diseaseDrugDosagePersistence.Delete<DiseaseDrugDosage>(diseaseDrugDosage);
                var save = await _diseaseDrugDosagePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DDD11", 500, "Unexpected error deleting this drug dosage from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DDD12", 500, $"Unexpected error deleting drug dosage: {ex.Message}");
            }
        }
    }
}