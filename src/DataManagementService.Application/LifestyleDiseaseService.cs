
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class LifestyleDiseaseService : ILifestyleDiseaseService
    {
        private readonly ILifestyleDiseasePersistence _lifestyleDiseasePersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly ILifestylePersistence _lifestylePersistence;
        private readonly IMapper _mapper;
        public LifestyleDiseaseService(ILifestyleDiseasePersistence lifestyleDiseasePersistence, IDiseasePersistence diseasePersistence, ILifestylePersistence lifestylePersistence, IMapper mapper)
        {
            _lifestyleDiseasePersistence = lifestyleDiseasePersistence;
            _diseasePersistence = diseasePersistence;
            _lifestylePersistence = lifestylePersistence;
            _mapper = mapper;
            
        }
        public async Task<LifestyleDiseaseDto> Add(LifestyleDiseaseDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var lifestyle = await _lifestylePersistence.GetByIdAsync(model.LifestyleId);
                if (disease is null || lifestyle is null) throw new DmsException("AP-DRD01", 400, "Related lifestyle side effect/lifestyle does not exist.");

                var lifestyleDisease = await _lifestyleDiseasePersistence.GetByRelatedIdAsync(model.LifestyleId, model.DiseaseId);
                if (lifestyleDisease is not null) throw new DmsException("AP-DRD02", 409, "This lifestyle side effect already exists.");

                var toAddLifestyleDisease = _mapper.Map<LifestyleDisease>(model);
                _lifestyleDiseasePersistence.Add<LifestyleDisease>(toAddLifestyleDisease);

                var save = await _lifestyleDiseasePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DRD03", 500, "Unexpected error saving this lifestyle side effect from the database.");

                var newLifestyleDisease = await _lifestyleDiseasePersistence.GetByRelatedIdAsync(model.LifestyleId, model.DiseaseId);
                if (newLifestyleDisease is null) throw new DmsException("AP-DRD04", 500, "Unexpected error adding lifestyle side effect.");
                
                return _mapper.Map<LifestyleDiseaseDto>(newLifestyleDisease);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD05", 500, $"Unexpected error adding lifestyle side effect: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var lifestyleDisease = await _lifestyleDiseasePersistence.GetByIdAsync(id);
                if (lifestyleDisease is null) throw new DmsException("AP-DRD06", 404, "This lifestyle side effect does not exist.");

                _lifestyleDiseasePersistence.Delete<LifestyleDisease>(lifestyleDisease);
                var save = await _lifestyleDiseasePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DRD07", 500, "Unexpected error deleting this lifestyle side effect from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD08", 500, $"Unexpected error deleting lifestyle side effect: {ex.Message}");
            }
        }
    }
}