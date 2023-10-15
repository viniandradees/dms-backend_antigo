
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseSupplementDosageAgeRangeService : IDiseaseSupplementDosageAgeRangeService
    {
        private readonly IDiseaseSupplementDosageAgeRangePersistence _diseaseSupplementDosageAgeRangePersistence;
        private readonly IDiseaseSupplementDosagePersistence _diseaseSupplementDosagePersistence;
        private readonly IMapper _mapper;
        public DiseaseSupplementDosageAgeRangeService(IDiseaseSupplementDosageAgeRangePersistence diseaseSupplementDosageAgeRangePersistence, IDiseaseSupplementDosagePersistence diseaseSupplementDosagePersistence, IMapper mapper)
        {
            _diseaseSupplementDosageAgeRangePersistence = diseaseSupplementDosageAgeRangePersistence;
            _diseaseSupplementDosagePersistence = diseaseSupplementDosagePersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseSupplementDosageAgeRangeDto> Add(DiseaseSupplementDosageAgeRangeDto model)
        {
            try
            {
                var diseaseSupplement = await _diseaseSupplementDosagePersistence.GetByIdAsync(model.DiseaseSupplementDosageId);
                if (diseaseSupplement is null) throw new DmsException("AP-DSDAR01", 400, "This dosage does not exist.");

                var checkIfExists = await _diseaseSupplementDosageAgeRangePersistence.GetByRelatedDataAsync(model.DiseaseSupplementDosageId, model.AgeTimeUnit, model.MinimumAge, model.MaximumAge);
                if (checkIfExists is not null) throw new DmsException("AP-DSDAR02", 409, "This dosage age range already exists.");

                var toAddDiseaseSupplementDosageAgeRange = _mapper.Map<DiseaseSupplementDosageAgeRange>(model);
                _diseaseSupplementDosageAgeRangePersistence.Add(toAddDiseaseSupplementDosageAgeRange);

                var save = await _diseaseSupplementDosageAgeRangePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DSDAR03", 500, "Unexpected error saving this dosage age range from the database.");

                var newDiseaseSupplementDosageAgeRange = await _diseaseSupplementDosageAgeRangePersistence.GetByRelatedDataAsync(model.DiseaseSupplementDosageId, model.AgeTimeUnit, model.MinimumAge, model.MaximumAge);
                if (newDiseaseSupplementDosageAgeRange is null) throw new DmsException("AP-DSDAR04", 500, "Unexpected error adding dosage age range.");
                return _mapper.Map<DiseaseSupplementDosageAgeRangeDto>(newDiseaseSupplementDosageAgeRange);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DSDAR05", 500, $"Unexpected error adding dosage age range: {ex.Message}");
            }
            
        }

        public async Task<DiseaseSupplementDosageAgeRangeDto> Update(int id, DiseaseSupplementDosageAgeRangeDto model)
        {
            try
            {
                var checkDiseaseSupplementDosageAgeRange = await _diseaseSupplementDosageAgeRangePersistence.GetByIdAsync(id);
                if (checkDiseaseSupplementDosageAgeRange is null) throw new DmsException("AP-DSDAR06", 404, "This dosage age range does not exist.");

                model.Id = id;

                var diseaseSupplementDosageAgeRange = _mapper.Map<DiseaseSupplementDosageAgeRange>(model);
                _diseaseSupplementDosageAgeRangePersistence.Update(diseaseSupplementDosageAgeRange);

                var save = await _diseaseSupplementDosageAgeRangePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DSDAR07", 500, "Unexpected error updating this dosage age range from the database.");
                
                var result = await _diseaseSupplementDosageAgeRangePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DSDAR08", 500, "Unexpected error updating dosage age range.");
                return _mapper.Map<DiseaseSupplementDosageAgeRangeDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DSDAR09", 500, $"Unexpected error updating dosage age range: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseSupplementDosageAgeRange = await _diseaseSupplementDosageAgeRangePersistence.GetByIdAsync(id);
                if (diseaseSupplementDosageAgeRange is null) throw new DmsException("AP-DSDAR10", 404, "This dosage age range does not exist.");

                _diseaseSupplementDosageAgeRangePersistence.Delete<DiseaseSupplementDosageAgeRange>(diseaseSupplementDosageAgeRange);
                var save = await _diseaseSupplementDosageAgeRangePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DSDAR11", 500, "Unexpected error deleting this dosage age range from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DSDAR12", 500, $"Unexpected error deleting dosage age range: {ex.Message}");
            }
        }
    }
}