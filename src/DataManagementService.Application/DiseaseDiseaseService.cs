
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseDiseaseService : IDiseaseDiseaseService
    {
        private readonly IDiseaseDiseasePersistence _diseaseDiseasePersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly IMapper _mapper;
        public DiseaseDiseaseService(IDiseaseDiseasePersistence diseaseDiseasePersistence, IDiseasePersistence diseasePersistence, IMapper mapper)
        {
            _diseaseDiseasePersistence = diseaseDiseasePersistence;
            _diseasePersistence = diseasePersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseDiseaseDto> Add(DiseaseDiseaseDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var symptom = await _diseasePersistence.GetByIdAsync(model.SymptomId);
                if (disease is null || symptom is null) throw new DmsException("AP-DDS01", 400, "Related disease/symptom does not exist.");

                var diseaseDisease = await _diseaseDiseasePersistence.GetByRelatedIdAsync(model.DiseaseId, model.SymptomId);
                if (diseaseDisease is not null) throw new DmsException("AP-DDS02", 409, "This diagnostic symptom already exists.");

                var toAddDiseaseDisease = _mapper.Map<DiseaseDisease>(model);
                _diseaseDiseasePersistence.Add<DiseaseDisease>(toAddDiseaseDisease);

                var save = await _diseaseDiseasePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DDS03", 500, "Unexpected error saving this diagnostic symptom from the database.");

                var newDiseaseDisease = await _diseaseDiseasePersistence.GetByRelatedIdAsync(model.DiseaseId, model.SymptomId);
                if (newDiseaseDisease is null) throw new DmsException("AP-DDS04", 500, "Unexpected error adding diagnostic symptom.");
                
                return _mapper.Map<DiseaseDiseaseDto>(newDiseaseDisease);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DDS05", 500, $"Unexpected error adding diagnostic symptom: {ex.Message}");
            }
            
        }

        public async Task<DiseaseDiseaseDto> Update(int id, DiseaseDiseaseDto model)
        {
            try
            {
                var checkDiseaseDisease = await _diseaseDiseasePersistence.GetByIdAsync(id);
                if (checkDiseaseDisease is null) throw new DmsException("AP-DDS06", 404, "This diagnostic symptom does not exist.");

                model.Id = id;

                var diseaseDisease = _mapper.Map<DiseaseDisease>(model);
                _diseaseDiseasePersistence.Update(diseaseDisease);

                var save = await _diseaseDiseasePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DDS07", 500, "Unexpected error updating this diagnostic symptom from the database.");
                
                var result = await _diseaseDiseasePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DDS08", 500, "Unexpected error updating diagnostic symptom.");
                return _mapper.Map<DiseaseDiseaseDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DDS09", 500, $"Unexpected error updating diagnostic symptom: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseDisease = await _diseaseDiseasePersistence.GetByIdAsync(id);
                if (diseaseDisease is null) throw new DmsException("AP-DDS10", 404, "This diagnostic symptom does not exist.");

                _diseaseDiseasePersistence.Delete<DiseaseDisease>(diseaseDisease);
                var save = await _diseaseDiseasePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DDS11", 500, "Unexpected error deleting this diagnostic symptom from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DDS12", 500, $"Unexpected error deleting diagnostic symptom: {ex.Message}");
            }
        }
    }
}