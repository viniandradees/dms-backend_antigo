
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DrugDiseaseService : IDrugDiseaseService
    {
        private readonly IDrugDiseasePersistence _drugDiseasePersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly IDrugPersistence _drugPersistence;
        private readonly IMapper _mapper;
        public DrugDiseaseService(IDrugDiseasePersistence drugDiseasePersistence, IDiseasePersistence diseasePersistence, IDrugPersistence drugPersistence, IMapper mapper)
        {
            _drugDiseasePersistence = drugDiseasePersistence;
            _diseasePersistence = diseasePersistence;
            _drugPersistence = drugPersistence;
            _mapper = mapper;
            
        }
        public async Task<DrugDiseaseDto> Add(DrugDiseaseDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var drug = await _drugPersistence.GetByIdAsync(model.DrugId);
                if (disease is null || drug is null) throw new DmsException("AP-DRD01", 400, "Related drug side effect/drug does not exist.");

                var drugDisease = await _drugDiseasePersistence.GetByRelatedIdAsync(model.DrugId, model.DiseaseId);
                if (drugDisease is not null) throw new DmsException("AP-DRD02", 409, "This drug side effect already exists.");

                var toAddDrugDisease = _mapper.Map<DrugDisease>(model);
                _drugDiseasePersistence.Add<DrugDisease>(toAddDrugDisease);

                var save = await _drugDiseasePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DRD03", 500, "Unexpected error saving this drug side effect from the database.");

                var newDrugDisease = await _drugDiseasePersistence.GetByRelatedIdAsync(model.DrugId, model.DiseaseId);
                if (newDrugDisease is null) throw new DmsException("AP-DRD04", 500, "Unexpected error adding drug side effect.");
                
                return _mapper.Map<DrugDiseaseDto>(newDrugDisease);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD05", 500, $"Unexpected error adding drug side effect: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var drugDisease = await _drugDiseasePersistence.GetByIdAsync(id);
                if (drugDisease is null) throw new DmsException("AP-DRD06", 404, "This drug side effect does not exist.");

                _drugDiseasePersistence.Delete<DrugDisease>(drugDisease);
                var save = await _drugDiseasePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DRD07", 500, "Unexpected error deleting this drug side effect from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD08", 500, $"Unexpected error deleting drug side effect: {ex.Message}");
            }
        }
    }
}