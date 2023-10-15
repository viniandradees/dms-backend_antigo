
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class ExamSupplementDosageAgeRangeService : IExamSupplementDosageAgeRangeService
    {
        private readonly IExamSupplementDosageAgeRangePersistence _examSupplementDosageAgeRangePersistence;
        private readonly IExamSupplementDosagePersistence _examSupplementDosagePersistence;
        private readonly IMapper _mapper;
        public ExamSupplementDosageAgeRangeService(IExamSupplementDosageAgeRangePersistence examSupplementDosageAgeRangePersistence, IExamSupplementDosagePersistence examSupplementDosagePersistence, IMapper mapper)
        {
            _examSupplementDosageAgeRangePersistence = examSupplementDosageAgeRangePersistence;
            _examSupplementDosagePersistence = examSupplementDosagePersistence;
            _mapper = mapper;
            
        }
        public async Task<ExamSupplementDosageAgeRangeDto> Add(ExamSupplementDosageAgeRangeDto model)
        {
            try
            {
                var examSupplement = await _examSupplementDosagePersistence.GetByIdAsync(model.ExamSupplementDosageId);
                if (examSupplement is null) throw new DmsException("AP-EDAR01", 400, "This dosage does not exist.");

                var checkIfExists = await _examSupplementDosageAgeRangePersistence.GetByRelatedDataAsync(model.ExamSupplementDosageId, model.AgeTimeUnit, model.MinimumAge, model.MaximumAge);
                if (checkIfExists is not null) throw new DmsException("AP-EDAR02", 409, "This dosage age range already exists.");

                var toAddExamSupplementDosageAgeRange = _mapper.Map<ExamSupplementDosageAgeRange>(model);
                _examSupplementDosageAgeRangePersistence.Add(toAddExamSupplementDosageAgeRange);

                var save = await _examSupplementDosageAgeRangePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-EDAR03", 500, "Unexpected error saving this dosage age range from the database.");

                var newExamSupplementDosageAgeRange = await _examSupplementDosageAgeRangePersistence.GetByRelatedDataAsync(model.ExamSupplementDosageId, model.AgeTimeUnit, model.MinimumAge, model.MaximumAge);
                if (newExamSupplementDosageAgeRange is null) throw new DmsException("AP-EDAR04", 500, "Unexpected error adding dosage age range.");
                return _mapper.Map<ExamSupplementDosageAgeRangeDto>(newExamSupplementDosageAgeRange);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-EDAR05", 500, $"Unexpected error adding dosage age range: {ex.Message}");
            }
            
        }

        public async Task<ExamSupplementDosageAgeRangeDto> Update(int id, ExamSupplementDosageAgeRangeDto model)
        {
            try
            {
                var checkExamSupplementDosageAgeRange = await _examSupplementDosageAgeRangePersistence.GetByIdAsync(id);
                if (checkExamSupplementDosageAgeRange is null) throw new DmsException("AP-EDAR06", 404, "This dosage age range does not exist.");

                model.Id = id;

                var examSupplementDosageAgeRange = _mapper.Map<ExamSupplementDosageAgeRange>(model);
                _examSupplementDosageAgeRangePersistence.Update(examSupplementDosageAgeRange);

                var save = await _examSupplementDosageAgeRangePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-EDAR07", 500, "Unexpected error updating this dosage age range from the database.");
                
                var result = await _examSupplementDosageAgeRangePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-EDAR08", 500, "Unexpected error updating dosage age range.");
                return _mapper.Map<ExamSupplementDosageAgeRangeDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-EDAR09", 500, $"Unexpected error updating dosage age range: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var examSupplementDosageAgeRange = await _examSupplementDosageAgeRangePersistence.GetByIdAsync(id);
                if (examSupplementDosageAgeRange is null) throw new DmsException("AP-EDAR10", 404, "This dosage age range does not exist.");

                _examSupplementDosageAgeRangePersistence.Delete<ExamSupplementDosageAgeRange>(examSupplementDosageAgeRange);
                var save = await _examSupplementDosageAgeRangePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-EDAR11", 500, "Unexpected error deleting this dosage age range from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-EDAR12", 500, $"Unexpected error deleting dosage age range: {ex.Message}");
            }
        }
    }
}