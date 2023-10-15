
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseLifestyleService : IDiseaseLifestyleService
    {
        private readonly IDiseaseLifestylePersistence _diseaseLifestylePersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly ILifestylePersistence _lifestylePersistence;
        private readonly IMapper _mapper;
        public DiseaseLifestyleService(IDiseaseLifestylePersistence diseaseLifestylePersistence, IDiseasePersistence diseasePersistence, ILifestylePersistence lifestylePersistence, IMapper mapper)
        {
            _diseaseLifestylePersistence = diseaseLifestylePersistence;
            _diseasePersistence = diseasePersistence;
            _lifestylePersistence = lifestylePersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseLifestyleDto> Add(DiseaseLifestyleDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var lifestyle = await _lifestylePersistence.GetByIdAsync(model.LifestyleId);
                if (disease is null || lifestyle is null) throw new DmsException("AP-DL01", 400, "Related disease/lifestyle does not exist.");

                var diseaseLifestyle = await _diseaseLifestylePersistence.GetByRelatedIdAsync(model.DiseaseId, model.LifestyleId);
                if (diseaseLifestyle is not null) throw new DmsException("AP-DL02", 409, "This treatment lifestyle already exists.");

                var toAddDiseaseLifestyle = _mapper.Map<DiseaseLifestyle>(model);
                _diseaseLifestylePersistence.Add<DiseaseLifestyle>(toAddDiseaseLifestyle);

                var save = await _diseaseLifestylePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DL03", 500, "Unexpected error saving this treatment lifestyle from the database.");

                var newDiseaseLifestyle = await _diseaseLifestylePersistence.GetByRelatedIdAsync(model.DiseaseId, model.LifestyleId);
                if (newDiseaseLifestyle is null) throw new DmsException("AP-DL04", 500, "Unexpected error adding treatment lifestyle.");
                
                return _mapper.Map<DiseaseLifestyleDto>(newDiseaseLifestyle);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DL05", 500, $"Unexpected error adding treatment lifestyle: {ex.Message}");
            }
            
        }

        public async Task<DiseaseLifestyleDto> Update(int id, DiseaseLifestyleDto model)
        {
            try
            {
                var checkDiseaseLifestyle = await _diseaseLifestylePersistence.GetByIdAsync(id);
                if (checkDiseaseLifestyle is null) throw new DmsException("AP-DL06", 404, "This treatment lifestyle does not exist.");

                model.Id = id;

                var diseaseLifestyle = _mapper.Map<DiseaseLifestyle>(model);
                _diseaseLifestylePersistence.Update(diseaseLifestyle);

                var save = await _diseaseLifestylePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DL07", 500, "Unexpected error updating this treatment lifestyle from the database.");
                
                var result = await _diseaseLifestylePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DL08", 500, "Unexpected error updating treatment lifestyle.");
                return _mapper.Map<DiseaseLifestyleDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DL09", 500, $"Unexpected error updating treatment lifestyle: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseLifestyle = await _diseaseLifestylePersistence.GetByIdAsync(id);
                if (diseaseLifestyle is null) throw new DmsException("AP-DL10", 404, "This treatment lifestyle does not exist.");

                _diseaseLifestylePersistence.Delete<DiseaseLifestyle>(diseaseLifestyle);
                var save = await _diseaseLifestylePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DL11", 500, "Unexpected error deleting this treatment lifestyle from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DL12", 500, $"Unexpected error deleting treatment lifestyle: {ex.Message}");
            }
        }
    }
}