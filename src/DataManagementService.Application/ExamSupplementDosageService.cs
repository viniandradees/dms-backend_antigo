
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class ExamSupplementDosageService : IExamSupplementDosageService
    {
        private readonly IExamSupplementDosagePersistence _examSupplementDosagePersistence;
        private readonly IExamSupplementPersistence _examSupplementPersistence;
        private readonly IMapper _mapper;
        public ExamSupplementDosageService(IExamSupplementDosagePersistence examSupplementDosagePersistence, IExamSupplementPersistence examSupplementPersistence, IMapper mapper)
        {
            _examSupplementDosagePersistence = examSupplementDosagePersistence;
            _examSupplementPersistence = examSupplementPersistence;
            _mapper = mapper;
            
        }
        public async Task<ExamSupplementDosageDto> Add(ExamSupplementDosageDto model)
        {
            try
            {
                var examSupplement = await _examSupplementPersistence.GetByIdAsync(model.ExamSupplementId);
                if (examSupplement is null) throw new DmsException("AP-DSD01", 400, "This treatment supplement does not exist.");

                var checkIfExists = await _examSupplementDosagePersistence.GetByRelatedIdAsync(model.ExamSupplementId, model.MeasurementUnitId);
                if (checkIfExists is not null) throw new DmsException("AP-DSD02", 409, "This dosage already exists.");

                var toAddExamSupplementDosage = _mapper.Map<ExamSupplementDosage>(model);
                _examSupplementDosagePersistence.Add(toAddExamSupplementDosage);

                var save = await _examSupplementDosagePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DSD03", 500, "Unexpected error saving this supplement dosage from the database.");

                var newExamSupplementDosage = await _examSupplementDosagePersistence.GetByRelatedIdAsync(model.ExamSupplementId, model.MeasurementUnitId);
                if (newExamSupplementDosage is null) throw new DmsException("AP-DSD04", 500, "Unexpected error adding supplement dosage.");
                return _mapper.Map<ExamSupplementDosageDto>(newExamSupplementDosage);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DSD05", 500, $"Unexpected error adding supplement dosage: {ex.Message}");
            }
            
        }

        public async Task<ExamSupplementDosageDto> Update(int id, ExamSupplementDosageDto model)
        {
            try
            {
                var checkExamSupplementDosage = await _examSupplementDosagePersistence.GetByIdAsync(id);
                if (checkExamSupplementDosage is null) throw new DmsException("AP-DSD06", 404, "This supplement dosage does not exist.");

                model.Id = id;

                var examSupplementDosage = _mapper.Map<ExamSupplementDosage>(model);
                _examSupplementDosagePersistence.Update(examSupplementDosage);

                var save = await _examSupplementDosagePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DSD07", 500, "Unexpected error updating this dosage from the database.");
                
                var result = await _examSupplementDosagePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DSD08", 500, "Unexpected error updating dosage.");
                return _mapper.Map<ExamSupplementDosageDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DSD09", 500, $"Unexpected error updating dosage: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var examSupplementDosage = await _examSupplementDosagePersistence.GetByIdAsync(id);
                if (examSupplementDosage is null) throw new DmsException("AP-DSD10", 404, "This supplement dosage does not exist.");

                _examSupplementDosagePersistence.Delete<ExamSupplementDosage>(examSupplementDosage);
                var save = await _examSupplementDosagePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DSD11", 500, "Unexpected error deleting this supplement dosage from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DSD12", 500, $"Unexpected error deleting supplement dosage: {ex.Message}");
            }
        }
    }
}