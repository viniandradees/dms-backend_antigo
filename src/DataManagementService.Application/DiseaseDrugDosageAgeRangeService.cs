
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseDrugDosageAgeRangeService : IDiseaseDrugDosageAgeRangeService
    {
        private readonly IDiseaseDrugDosageAgeRangePersistence _diseaseDrugDosageAgeRangePersistence;
        private readonly IDiseaseDrugDosagePersistence _diseaseDrugDosagePersistence;
        private readonly IMapper _mapper;
        public DiseaseDrugDosageAgeRangeService(IDiseaseDrugDosageAgeRangePersistence diseaseDrugDosageAgeRangePersistence, IDiseaseDrugDosagePersistence diseaseDrugDosagePersistence, IMapper mapper)
        {
            _diseaseDrugDosageAgeRangePersistence = diseaseDrugDosageAgeRangePersistence;
            _diseaseDrugDosagePersistence = diseaseDrugDosagePersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseDrugDosageAgeRangeDto> Add(DiseaseDrugDosageAgeRangeDto model)
        {
            try
            {
                var diseaseDrug = await _diseaseDrugDosagePersistence.GetByIdAsync(model.DiseaseDrugDosageId);
                if (diseaseDrug is null) throw new DmsException("AP-DDDAR01", 400, "This dosage does not exist.");

                var checkIfExists = await _diseaseDrugDosageAgeRangePersistence.GetByRelatedDataAsync(model.DiseaseDrugDosageId, model.AgeTimeUnit, model.MinimumAge, model.MaximumAge);
                if (checkIfExists is not null) throw new DmsException("AP-DDDAR02", 409, "This dosage age range already exists.");

                var toAddDiseaseDrugDosageAgeRange = _mapper.Map<DiseaseDrugDosageAgeRange>(model);
                _diseaseDrugDosageAgeRangePersistence.Add(toAddDiseaseDrugDosageAgeRange);

                var save = await _diseaseDrugDosageAgeRangePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DDDAR03", 500, "Unexpected error saving this dosage age range from the database.");

                var newDiseaseDrugDosageAgeRange = await _diseaseDrugDosageAgeRangePersistence.GetByRelatedDataAsync(model.DiseaseDrugDosageId, model.AgeTimeUnit, model.MinimumAge, model.MaximumAge);
                if (newDiseaseDrugDosageAgeRange is null) throw new DmsException("AP-DDDAR04", 500, "Unexpected error adding dosage age range.");
                return _mapper.Map<DiseaseDrugDosageAgeRangeDto>(newDiseaseDrugDosageAgeRange);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DDDAR05", 500, $"Unexpected error adding dosage age range: {ex.Message}");
            }
            
        }

        public async Task<DiseaseDrugDosageAgeRangeDto> Update(int id, DiseaseDrugDosageAgeRangeDto model)
        {
            try
            {
                var checkDiseaseDrugDosageAgeRange = await _diseaseDrugDosageAgeRangePersistence.GetByIdAsync(id);
                if (checkDiseaseDrugDosageAgeRange is null) throw new DmsException("AP-DDDAR06", 404, "This dosage age range does not exist.");

                model.Id = id;

                var diseaseDrugDosageAgeRange = _mapper.Map<DiseaseDrugDosageAgeRange>(model);
                _diseaseDrugDosageAgeRangePersistence.Update(diseaseDrugDosageAgeRange);

                var save = await _diseaseDrugDosageAgeRangePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DDDAR07", 500, "Unexpected error updating this dosage age range from the database.");
                
                var result = await _diseaseDrugDosageAgeRangePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DDDAR08", 500, "Unexpected error updating dosage age range.");
                return _mapper.Map<DiseaseDrugDosageAgeRangeDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DDDAR09", 500, $"Unexpected error updating dosage age range: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseDrugDosageAgeRange = await _diseaseDrugDosageAgeRangePersistence.GetByIdAsync(id);
                if (diseaseDrugDosageAgeRange is null) throw new DmsException("AP-DDDAR10", 404, "This dosage age range does not exist.");

                _diseaseDrugDosageAgeRangePersistence.Delete<DiseaseDrugDosageAgeRange>(diseaseDrugDosageAgeRange);
                var save = await _diseaseDrugDosageAgeRangePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DDDAR11", 500, "Unexpected error deleting this dosage age range from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DDDAR12", 500, $"Unexpected error deleting dosage age range: {ex.Message}");
            }
        }
    }
}