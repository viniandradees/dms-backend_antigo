
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseSupplementService : IDiseaseSupplementService
    {
        private readonly IDiseaseSupplementPersistence _diseaseSupplementPersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly ISupplementPersistence _supplementPersistence;
        private readonly IMapper _mapper;
        public DiseaseSupplementService(IDiseaseSupplementPersistence diseaseSupplementPersistence, IDiseasePersistence diseasePersistence, ISupplementPersistence supplementPersistence, IMapper mapper)
        {
            _diseaseSupplementPersistence = diseaseSupplementPersistence;
            _diseasePersistence = diseasePersistence;
            _supplementPersistence = supplementPersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseSupplementDto> Add(DiseaseSupplementDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var supplement = await _supplementPersistence.GetByIdAsync(model.SupplementId);
                if (disease is null || supplement is null) throw new DmsException("AP-DS01", 400, "Related disease/supplement does not exist.");

                var diseaseSupplement = await _diseaseSupplementPersistence.GetByRelatedIdAsync(model.DiseaseId, model.SupplementId);
                if (diseaseSupplement is not null) throw new DmsException("AP-DS02", 409, "This treatment supplement already exists.");

                var toAddDiseaseSupplement = _mapper.Map<DiseaseSupplement>(model);
                _diseaseSupplementPersistence.Add<DiseaseSupplement>(toAddDiseaseSupplement);

                var save = await _diseaseSupplementPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DS03", 500, "Unexpected error saving this treatment supplement from the database.");

                var newDiseaseSupplement = await _diseaseSupplementPersistence.GetByRelatedIdAsync(model.DiseaseId, model.SupplementId);
                if (newDiseaseSupplement is null) throw new DmsException("AP-DS04", 500, "Unexpected error adding treatment supplement.");
                
                return _mapper.Map<DiseaseSupplementDto>(newDiseaseSupplement);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DS05", 500, $"Unexpected error adding treatment supplement: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseSupplement = await _diseaseSupplementPersistence.GetByIdAsync(id);
                if (diseaseSupplement is null) throw new DmsException("AP-DS06", 404, "This treatment supplement does not exist.");

                _diseaseSupplementPersistence.Delete<DiseaseSupplement>(diseaseSupplement);
                var save = await _diseaseSupplementPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DS07", 500, "Unexpected error deleting this treatment supplement from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DS08", 500, $"Unexpected error deleting treatment supplement: {ex.Message}");
            }
        }
    }
}