
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseFoodDosageAgeRangeService : IDiseaseFoodDosageAgeRangeService
    {
        private readonly IDiseaseFoodDosageAgeRangePersistence _diseaseFoodDosageAgeRangePersistence;
        private readonly IDiseaseFoodDosagePersistence _diseaseFoodDosagePersistence;
        private readonly IMapper _mapper;
        public DiseaseFoodDosageAgeRangeService(IDiseaseFoodDosageAgeRangePersistence diseaseFoodDosageAgeRangePersistence, IDiseaseFoodDosagePersistence diseaseFoodDosagePersistence, IMapper mapper)
        {
            _diseaseFoodDosageAgeRangePersistence = diseaseFoodDosageAgeRangePersistence;
            _diseaseFoodDosagePersistence = diseaseFoodDosagePersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseFoodDosageAgeRangeDto> Add(DiseaseFoodDosageAgeRangeDto model)
        {
            try
            {
                var diseaseFood = await _diseaseFoodDosagePersistence.GetByIdAsync(model.DiseaseFoodDosageId);
                if (diseaseFood is null) throw new DmsException("AP-DFDAR01", 400, "This dosage does not exist.");

                var checkIfExists = await _diseaseFoodDosageAgeRangePersistence.GetByRelatedDataAsync(model.DiseaseFoodDosageId, model.AgeTimeUnit, model.MinimumAge, model.MaximumAge);
                if (checkIfExists is not null) throw new DmsException("AP-DFDAR02", 409, "This dosage age range already exists.");

                var toAddDiseaseFoodDosageAgeRange = _mapper.Map<DiseaseFoodDosageAgeRange>(model);
                _diseaseFoodDosageAgeRangePersistence.Add(toAddDiseaseFoodDosageAgeRange);

                var save = await _diseaseFoodDosageAgeRangePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DFDAR03", 500, "Unexpected error saving this dosage age range from the database.");

                var newDiseaseFoodDosageAgeRange = await _diseaseFoodDosageAgeRangePersistence.GetByRelatedDataAsync(model.DiseaseFoodDosageId, model.AgeTimeUnit, model.MinimumAge, model.MaximumAge);
                if (newDiseaseFoodDosageAgeRange is null) throw new DmsException("AP-DFDAR04", 500, "Unexpected error adding dosage age range.");
                return _mapper.Map<DiseaseFoodDosageAgeRangeDto>(newDiseaseFoodDosageAgeRange);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DFDAR05", 500, $"Unexpected error adding dosage age range: {ex.Message}");
            }
            
        }

        public async Task<DiseaseFoodDosageAgeRangeDto> Update(int id, DiseaseFoodDosageAgeRangeDto model)
        {
            try
            {
                var checkDiseaseFoodDosageAgeRange = await _diseaseFoodDosageAgeRangePersistence.GetByIdAsync(id);
                if (checkDiseaseFoodDosageAgeRange is null) throw new DmsException("AP-DFDAR06", 404, "This dosage age range does not exist.");

                model.Id = id;

                var diseaseFoodDosageAgeRange = _mapper.Map<DiseaseFoodDosageAgeRange>(model);
                _diseaseFoodDosageAgeRangePersistence.Update(diseaseFoodDosageAgeRange);

                var save = await _diseaseFoodDosageAgeRangePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DFDAR07", 500, "Unexpected error updating this dosage age range from the database.");
                
                var result = await _diseaseFoodDosageAgeRangePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DFDAR08", 500, "Unexpected error updating dosage age range.");
                return _mapper.Map<DiseaseFoodDosageAgeRangeDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DFDAR09", 500, $"Unexpected error updating dosage age range: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseFoodDosageAgeRange = await _diseaseFoodDosageAgeRangePersistence.GetByIdAsync(id);
                if (diseaseFoodDosageAgeRange is null) throw new DmsException("AP-DFDAR10", 404, "This dosage age range does not exist.");

                _diseaseFoodDosageAgeRangePersistence.Delete<DiseaseFoodDosageAgeRange>(diseaseFoodDosageAgeRange);
                var save = await _diseaseFoodDosageAgeRangePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DFDAR11", 500, "Unexpected error deleting this dosage age range from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DFDAR12", 500, $"Unexpected error deleting dosage age range: {ex.Message}");
            }
        }
    }
}