
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class SupplementDiseaseService : ISupplementDiseaseService
    {
        private readonly ISupplementDiseasePersistence _supplementDiseasePersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly ISupplementPersistence _supplementPersistence;
        private readonly IMapper _mapper;
        public SupplementDiseaseService(ISupplementDiseasePersistence supplementDiseasePersistence, IDiseasePersistence diseasePersistence, ISupplementPersistence supplementPersistence, IMapper mapper)
        {
            _supplementDiseasePersistence = supplementDiseasePersistence;
            _diseasePersistence = diseasePersistence;
            _supplementPersistence = supplementPersistence;
            _mapper = mapper;
            
        }
        public async Task<SupplementDiseaseDto> Add(SupplementDiseaseDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var supplement = await _supplementPersistence.GetByIdAsync(model.SupplementId);
                if (disease is null || supplement is null) throw new DmsException("AP-SD01", 400, "Related supplement side effect/supplement does not exist.");

                var supplementDisease = await _supplementDiseasePersistence.GetByRelatedIdAsync(model.SupplementId, model.DiseaseId);
                if (supplementDisease is not null) throw new DmsException("AP-SD02", 409, "This supplement side effect already exists.");

                var toAddSupplementDisease = _mapper.Map<SupplementDisease>(model);
                _supplementDiseasePersistence.Add<SupplementDisease>(toAddSupplementDisease);

                var save = await _supplementDiseasePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-SD03", 500, "Unexpected error saving this supplement side effect from the database.");

                var newSupplementDisease = await _supplementDiseasePersistence.GetByRelatedIdAsync(model.SupplementId, model.DiseaseId);
                if (newSupplementDisease is null) throw new DmsException("AP-SD04", 500, "Unexpected error adding supplement side effect.");
                
                return _mapper.Map<SupplementDiseaseDto>(newSupplementDisease);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-SD05", 500, $"Unexpected error adding supplement side effect: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var supplementDisease = await _supplementDiseasePersistence.GetByIdAsync(id);
                if (supplementDisease is null) throw new DmsException("AP-SD06", 404, "This supplement side effect does not exist.");

                _supplementDiseasePersistence.Delete<SupplementDisease>(supplementDisease);
                var save = await _supplementDiseasePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-SD07", 500, "Unexpected error deleting this supplement side effect from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-SD08", 500, $"Unexpected error deleting supplement side effect: {ex.Message}");
            }
        }
    }
}