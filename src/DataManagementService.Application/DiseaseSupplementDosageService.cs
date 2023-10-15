
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseSupplementDosageService : IDiseaseSupplementDosageService
    {
        private readonly IDiseaseSupplementDosagePersistence _diseaseSupplementDosagePersistence;
        private readonly IDiseaseSupplementPersistence _diseaseSupplementPersistence;
        private readonly IMapper _mapper;
        public DiseaseSupplementDosageService(IDiseaseSupplementDosagePersistence diseaseSupplementDosagePersistence, IDiseaseSupplementPersistence diseaseSupplementPersistence, IMapper mapper)
        {
            _diseaseSupplementDosagePersistence = diseaseSupplementDosagePersistence;
            _diseaseSupplementPersistence = diseaseSupplementPersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseSupplementDosageDto> Add(DiseaseSupplementDosageDto model)
        {
            try
            {
                var diseaseSupplement = await _diseaseSupplementPersistence.GetByIdAsync(model.DiseaseSupplementId);
                if (diseaseSupplement is null) throw new DmsException("AP-DSD01", 400, "This treatment supplement does not exist.");

                var checkIfExists = await _diseaseSupplementDosagePersistence.GetByRelatedIdAsync(model.DiseaseSupplementId, model.MeasurementUnitId);
                if (checkIfExists is not null) throw new DmsException("AP-DSD02", 409, "This dosage already exists.");

                var toAddDiseaseSupplementDosage = _mapper.Map<DiseaseSupplementDosage>(model);
                _diseaseSupplementDosagePersistence.Add(toAddDiseaseSupplementDosage);

                var save = await _diseaseSupplementDosagePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DSD03", 500, "Unexpected error saving this supplement dosage from the database.");

                var newDiseaseSupplementDosage = await _diseaseSupplementDosagePersistence.GetByRelatedIdAsync(model.DiseaseSupplementId, model.MeasurementUnitId);
                if (newDiseaseSupplementDosage is null) throw new DmsException("AP-DSD04", 500, "Unexpected error adding supplement dosage.");
                return _mapper.Map<DiseaseSupplementDosageDto>(newDiseaseSupplementDosage);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DSD05", 500, $"Unexpected error adding supplement dosage: {ex.Message}");
            }
            
        }

        public async Task<DiseaseSupplementDosageDto> Update(int id, DiseaseSupplementDosageDto model)
        {
            try
            {
                var checkDiseaseSupplementDosage = await _diseaseSupplementDosagePersistence.GetByIdAsync(id);
                if (checkDiseaseSupplementDosage is null) throw new DmsException("AP-DSD06", 404, "This supplement dosage does not exist.");

                model.Id = id;

                var diseaseSupplementDosage = _mapper.Map<DiseaseSupplementDosage>(model);
                _diseaseSupplementDosagePersistence.Update(diseaseSupplementDosage);

                var save = await _diseaseSupplementDosagePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DSD07", 500, "Unexpected error updating this dosage from the database.");
                
                var result = await _diseaseSupplementDosagePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DSD08", 500, "Unexpected error updating dosage.");
                return _mapper.Map<DiseaseSupplementDosageDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DSD09", 500, $"Unexpected error updating dosage: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseSupplementDosage = await _diseaseSupplementDosagePersistence.GetByIdAsync(id);
                if (diseaseSupplementDosage is null) throw new DmsException("AP-DSD10", 404, "This supplement dosage does not exist.");

                _diseaseSupplementDosagePersistence.Delete<DiseaseSupplementDosage>(diseaseSupplementDosage);
                var save = await _diseaseSupplementDosagePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DSD11", 500, "Unexpected error deleting this supplement dosage from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DSD12", 500, $"Unexpected error deleting supplement dosage: {ex.Message}");
            }
        }
    }
}