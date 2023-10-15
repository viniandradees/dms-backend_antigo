
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class ExamResultReferenceCountryService : IExamResultReferenceCountryService
    {
        private readonly IExamResultReferenceCountryPersistence _examResultReferenceCountryPersistence;
        private readonly IExamResultReferencePersistence _examResultReferencePersistence;
        private readonly IMapper _mapper;
        public ExamResultReferenceCountryService(IExamResultReferenceCountryPersistence examResultReferenceCountryPersistence, IExamResultReferencePersistence examPersistence, IMapper mapper)
        {
            _examResultReferenceCountryPersistence = examResultReferenceCountryPersistence;
            _examResultReferencePersistence = examPersistence;
            _mapper = mapper;
            
        }
        public async Task<ExamResultReferenceCountryDto> Add(ExamResultReferenceCountryDto model)
        {
            try
            {
                var examResultReference = await _examResultReferencePersistence.GetByIdAsync(model.ExamResultReferenceId);
                if (examResultReference is null) throw new DmsException("AP-ERRC01", 400, "Exam result reference does not exist.");

                var checkIfExists = await _examResultReferenceCountryPersistence.GetByRelatedSettingsAsync(model.ExamResultReferenceId, model.CountryId);
                if (checkIfExists is not null) throw new DmsException("AP-ERRC02", 409, "This exam result reference country already exists.");

                var toAddExamResultReferenceCountry = _mapper.Map<ExamResultReferenceCountry>(model);
                _examResultReferenceCountryPersistence.Add<ExamResultReferenceCountry>(toAddExamResultReferenceCountry);

                var save = await _examResultReferenceCountryPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-ERRC03", 500, "Unexpected error saving this exam result reference country from the database.");

                var newExamResultReferenceCountry = await _examResultReferenceCountryPersistence.GetByRelatedSettingsAsync(model.ExamResultReferenceId, model.CountryId);
                if (newExamResultReferenceCountry is null) throw new DmsException("AP-ERRC04", 500, "Unexpected error adding exam result reference country.");
                
                return _mapper.Map<ExamResultReferenceCountryDto>(newExamResultReferenceCountry);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ERRC05", 500, $"Unexpected error adding exam result reference country: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var examResultReferenceCountry = await _examResultReferenceCountryPersistence.GetByIdAsync(id);
                if (examResultReferenceCountry is null) throw new DmsException("AP-ERRC06", 404, "This exam result reference country does not exist.");

                _examResultReferenceCountryPersistence.Delete<ExamResultReferenceCountry>(examResultReferenceCountry);
                var save = await _examResultReferenceCountryPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-ERRC07", 500, "Unexpected error deleting this exam result reference country from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ERRC08", 500, $"Unexpected error deleting exam result reference country: {ex.Message}");
            }
        }
    }
}