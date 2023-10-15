
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class ExamResultReferenceVariationService : IExamResultReferenceVariationService
    {
        private readonly IExamResultReferenceVariationPersistence _examResultReferenceVariationPersistence;
        private readonly IExamResultReferencePersistence _examResultReferencePersistence;
        private readonly IMapper _mapper;
        public ExamResultReferenceVariationService(IExamResultReferenceVariationPersistence examResultReferenceVariationPersistence, IExamResultReferencePersistence examPersistence, IMapper mapper)
        {
            _examResultReferenceVariationPersistence = examResultReferenceVariationPersistence;
            _examResultReferencePersistence = examPersistence;
            _mapper = mapper;
            
        }
        public async Task<ExamResultReferenceVariationDto> Add(ExamResultReferenceVariationDto model)
        {
            try
            {
                var examResultReference = await _examResultReferencePersistence.GetByIdAsync(model.ExamResultReferenceId);
                if (examResultReference is null) throw new DmsException("AP-ERRV01", 400, "Exam result reference does not exist.");

                var checkIfExists = await _examResultReferenceVariationPersistence.GetByRelatedSettingsAsync(model.ExamResultReferenceId, model.PatientMinimumAge, model.PatientMaximumAge, model.Gender, model.PregnancyRequired, model.MenopauseRequired);
                if (checkIfExists is not null) throw new DmsException("AP-ERRV02", 409, "This exam result reference variation already exists.");

                var toAddExamResultReferenceVariation = _mapper.Map<ExamResultReferenceVariation>(model);
                _examResultReferenceVariationPersistence.Add<ExamResultReferenceVariation>(toAddExamResultReferenceVariation);

                var save = await _examResultReferenceVariationPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-ERRV03", 500, "Unexpected error saving this exam result reference variation from the database.");

                var newExamResultReferenceVariation = await _examResultReferenceVariationPersistence.GetByRelatedSettingsAsync(model.ExamResultReferenceId, model.PatientMinimumAge, model.PatientMaximumAge, model.Gender, model.PregnancyRequired, model.MenopauseRequired);
                if (newExamResultReferenceVariation is null) throw new DmsException("AP-ERRV04", 500, "Unexpected error adding exam result reference variation.");
                
                return _mapper.Map<ExamResultReferenceVariationDto>(newExamResultReferenceVariation);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ERRV05", 500, $"Unexpected error adding exam result reference variation: {ex.Message}");
            }
            
        }

        public async Task<ExamResultReferenceVariationDto> Update(int id, ExamResultReferenceVariationDto model)
        {
            try
            {
                var checkExamResultReferenceVariation = await _examResultReferenceVariationPersistence.GetByIdAsync(id);
                if (checkExamResultReferenceVariation is null) throw new DmsException("AP-ERR06", 404, "This exam result reference variation does not exist.");

                var checkIfExists = await _examResultReferenceVariationPersistence.GetByRelatedSettingsAsync(model.ExamResultReferenceId, model.PatientMinimumAge, model.PatientMaximumAge, model.Gender, model.PregnancyRequired, model.MenopauseRequired);
                if (checkIfExists is not null && checkIfExists.Id != model.Id) throw new DmsException("AP-ERRV02", 409, "This exam result reference variation already exists.");

                model.Id = id;

                var examResultReferenceVariation = _mapper.Map<ExamResultReferenceVariation>(model);
                _examResultReferenceVariationPersistence.Update(examResultReferenceVariation);

                var save = await _examResultReferenceVariationPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-ERR07", 500, "Unexpected error updating this exam result reference variation from the database.");
                
                var result = await _examResultReferenceVariationPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-ERR08", 500, "Unexpected error updating exam result reference variation.");
                return _mapper.Map<ExamResultReferenceVariationDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ERR09", 500, $"Unexpected error updating exam result reference: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var examResultReferenceVariation = await _examResultReferenceVariationPersistence.GetByIdAsync(id);
                if (examResultReferenceVariation is null) throw new DmsException("AP-ERRV06", 404, "This exam result reference variation does not exist.");

                _examResultReferenceVariationPersistence.Delete<ExamResultReferenceVariation>(examResultReferenceVariation);
                var save = await _examResultReferenceVariationPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-ERRV07", 500, "Unexpected error deleting this exam result reference variation from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ERRV08", 500, $"Unexpected error deleting exam result reference variation: {ex.Message}");
            }
        }
    }
}