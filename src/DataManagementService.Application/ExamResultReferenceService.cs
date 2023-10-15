
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class ExamResultReferenceService : IExamResultReferenceService
    {
        private readonly IExamResultReferencePersistence _examResultReferencePersistence;
        private readonly IMeasurementUnitPersistence _measurementUnitPersistence;
        private readonly IExamPersistence _examPersistence;
        private readonly IMapper _mapper;
        public ExamResultReferenceService(IExamResultReferencePersistence examResultReferencePersistence, IMeasurementUnitPersistence measurementUnitPersistence, IExamPersistence examPersistence, IMapper mapper)
        {
            _examResultReferencePersistence = examResultReferencePersistence;
            _measurementUnitPersistence = measurementUnitPersistence;
            _examPersistence = examPersistence;
            _mapper = mapper;
            
        }
        public async Task<ExamResultReferenceDto> Add(ExamResultReferenceDto model)
        {
            try
            {
                var measurementUnit = await _measurementUnitPersistence.GetByIdAsync(model.MeasurementUnitId);
                var exam = await _examPersistence.GetByIdAsync(model.ExamId);
                if (measurementUnit is null || exam is null) throw new DmsException("AP-ERR01", 400, "Related exam/measurement unit does not exist.");

                var checkIfExists = await _examResultReferencePersistence.GetByRelatedIdAsync(model.ExamId, model.MeasurementUnitId);
                if (checkIfExists is not null) throw new DmsException("AP-ERR02", 409, "This exam result reference already exists.");

                var toAddExamResultReference = _mapper.Map<ExamResultReference>(model);
                _examResultReferencePersistence.Add<ExamResultReference>(toAddExamResultReference);

                var save = await _examResultReferencePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-ERR03", 500, "Unexpected error saving this exam result reference from the database.");

                var newExamResultReference = await _examResultReferencePersistence.GetByRelatedIdAsync(model.ExamId, model.MeasurementUnitId);
                if (newExamResultReference is null) throw new DmsException("AP-ERR04", 500, "Unexpected error adding exam result reference.");
                
                return _mapper.Map<ExamResultReferenceDto>(newExamResultReference);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ERR05", 500, $"Unexpected error adding exam result reference: {ex.Message}");
            }
            
        }

        public async Task<ExamResultReferenceDto> Update(int id, ExamResultReferenceDto model)
        {
            try
            {
                var checkExamResultReference = await _examResultReferencePersistence.GetByIdAsync(id);
                if (checkExamResultReference is null) throw new DmsException("AP-ERR06", 404, "This exam result reference does not exist.");

                model.Id = id;

                var examResultReference = _mapper.Map<ExamResultReference>(model);
                _examResultReferencePersistence.Update(examResultReference);

                var save = await _examResultReferencePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-ERR07", 500, "Unexpected error updating this exam result reference from the database.");
                
                var result = await _examResultReferencePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-ERR08", 500, "Unexpected error updating exam result reference.");
                return _mapper.Map<ExamResultReferenceDto>(result);
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
                var examResultReference = await _examResultReferencePersistence.GetByIdAsync(id);
                if (examResultReference is null) throw new DmsException("AP-ERR10", 404, "This exam result reference does not exist.");

                _examResultReferencePersistence.Delete<ExamResultReference>(examResultReference);
                var save = await _examResultReferencePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-ERR11", 500, "Unexpected error deleting this exam result reference from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ERR12", 500, $"Unexpected error deleting exam result reference: {ex.Message}");
            }
        }
    }
}