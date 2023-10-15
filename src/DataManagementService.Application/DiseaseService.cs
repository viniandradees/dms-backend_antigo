
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly IMapper _mapper;
        public DiseaseService(IDiseasePersistence diseasePersistence, IMapper mapper)
        {
            _diseasePersistence = diseasePersistence;
            _mapper = mapper;
            
        }

        public async Task<DiseaseDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var diseases = await _diseasePersistence.GetAllAsync(getFullData);
                if (diseases is null) return null;

                return _mapper.Map<DiseaseDto[]>(diseases);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI01", 500, $"Unexpected error getting diseases: {ex.Message}");
            }
        }

        public async Task<DiseaseDto> GetByIdAsync(int id)
        {
            try
            {
                var diseases = await _diseasePersistence.GetByIdAsync(id);
                if (diseases is null) return null;

                return _mapper.Map<DiseaseDto>(diseases);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI02", 500, $"Unexpected error getting disease: {ex.Message}");
            }
        }

        public async Task<DiseaseDto> GetByNameAsync(string name)
        {
            try
            {
                var diseases = await _diseasePersistence.GetByNameAsync(name);
                if (diseases is null) return null;

                return _mapper.Map<DiseaseDto>(diseases);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI03", 500, $"Unexpected error getting disease: {ex.Message}");
            }
        }
        public async Task<DiseaseDto> Add(DiseaseDto model)
        {
            try
            {
                var disease = _mapper.Map<Disease>(model);
                _diseasePersistence.Add<Disease>(disease);

                var checkName = await _diseasePersistence.GetByNameAsync(disease.Name);
                if (checkName is not null) throw new DmsException("AP-DI04", 409, "Disease name already exists.");
                
                var save = await _diseasePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DI05", 500, "Unexpected error adding this disease from the database.");

                var diseaseDto = await _diseasePersistence.GetByIdAsync(disease.Id);
                if (diseaseDto is null) throw new DmsException("AP-DI06", 500, "Unexpected error adding disease.");

                return _mapper.Map<DiseaseDto>(diseaseDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI07", 500, $"Unexpected error adding disease: {ex.Message}");
            }
            
        }

        public async Task<DiseaseDto> Update(int id, DiseaseDto model)
        {
            try
            {
                var checkDisease = await _diseasePersistence.GetByIdAsync(id);
                if (checkDisease is null) throw new DmsException("AP-DI08", 404, "This disease does not exist.");

                model.Id = id;

                if (checkDisease.Name != model.Name) {
                    var checkName = await _diseasePersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-DI09", 409, "Disease name already exists.");
                }

                var disease = _mapper.Map<Disease>(model);
                _diseasePersistence.Update<Disease>(disease);

                var save = await _diseasePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DI10", 500, "Unexpected error updating this disease from the database.");
                
                var result = await _diseasePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DI11", 500, "Unexpected error updating disease.");
                return _mapper.Map<DiseaseDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI12", 500, $"Unexpected error updating disease: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(id);
                if (disease is null) throw new DmsException("AP-DI13", 404, "This disease does not exist.");

                _diseasePersistence.Delete<Disease>(disease);
                return await _diseasePersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI14", 500, $"Unexpected error deleting disease: {ex.Message}");
            }
        }
    }
}