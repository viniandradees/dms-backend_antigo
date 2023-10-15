
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseExamService : IDiseaseExamService
    {
        private readonly IDiseaseExamPersistence _diseaseExamPersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly IExamPersistence _examPersistence;
        private readonly IMapper _mapper;
        public DiseaseExamService(IDiseaseExamPersistence diseaseExamPersistence, IDiseasePersistence diseasePersistence, IExamPersistence examPersistence, IMapper mapper)
        {
            _diseaseExamPersistence = diseaseExamPersistence;
            _diseasePersistence = diseasePersistence;
            _examPersistence = examPersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseExamDto> Add(DiseaseExamDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var exam = await _examPersistence.GetByIdAsync(model.ExamId);
                if (disease is null || exam is null) throw new DmsException("AP-DF01", 400, "Related disease/exam does not exist.");

                var diseaseExam = await _diseaseExamPersistence.GetByRelatedIdAsync(model.DiseaseId, model.ExamId);
                if (diseaseExam is not null) throw new DmsException("AP-DF02", 409, "This diagnostic exam result already exists.");

                var toAddDiseaseExam = _mapper.Map<DiseaseExam>(model);
                _diseaseExamPersistence.Add<DiseaseExam>(toAddDiseaseExam);

                var save = await _diseaseExamPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DF03", 500, "Unexpected error saving this diagnostic exam result from the database.");

                var newDiseaseExam = await _diseaseExamPersistence.GetByRelatedIdAsync(model.DiseaseId, model.ExamId);
                if (newDiseaseExam is null) throw new DmsException("AP-DF04", 500, "Unexpected error adding diagnostic exam result.");
                
                return _mapper.Map<DiseaseExamDto>(newDiseaseExam);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DF05", 500, $"Unexpected error adding diagnostic exam result: {ex.Message}");
            }
        }

        public async Task<DiseaseExamDto> Update(int id, DiseaseExamDto model)
        {
            try
            {
                var checkDiseaseExam = await _diseaseExamPersistence.GetByIdAsync(id);
                if (checkDiseaseExam is null) throw new DmsException("AP-DF06", 404, "This diagnostic exam result does not exist.");

                model.Id = id;

                var diseaseExam = _mapper.Map<DiseaseExam>(model);
                _diseaseExamPersistence.Update(diseaseExam);

                var save = await _diseaseExamPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DF07", 500, "Unexpected error updating this diagnostic exam result from the database.");
                
                var result = await _diseaseExamPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DF08", 500, "Unexpected error updating diagnostic exam result.");
                return _mapper.Map<DiseaseExamDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DF09", 500, $"Unexpected error updating diagnostic exam result: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseExam = await _diseaseExamPersistence.GetByIdAsync(id);
                if (diseaseExam is null) throw new DmsException("AP-DF10", 404, "This diagnostic exam result does not exist.");

                _diseaseExamPersistence.Delete<DiseaseExam>(diseaseExam);
                var save = await _diseaseExamPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DF11", 500, "Unexpected error deleting this diagnostic exam result from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DF12", 500, $"Unexpected error deleting diagnostic exam result: {ex.Message}");
            }
        }
    }
}