
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class ExamLifestyleService : IExamLifestyleService
    {
        private readonly IExamLifestylePersistence _examLifestylePersistence;
        private readonly IExamPersistence _examPersistence;
        private readonly ILifestylePersistence _lifestylePersistence;
        private readonly IMapper _mapper;
        public ExamLifestyleService(IExamLifestylePersistence examLifestylePersistence, IExamPersistence examPersistence, ILifestylePersistence lifestylePersistence, IMapper mapper)
        {
            _examLifestylePersistence = examLifestylePersistence;
            _examPersistence = examPersistence;
            _lifestylePersistence = lifestylePersistence;
            _mapper = mapper;
            
        }
        public async Task<ExamLifestyleDto> Add(ExamLifestyleDto model)
        {
            try
            {
                var exam = await _examPersistence.GetByIdAsync(model.ExamId);
                var lifestyle = await _lifestylePersistence.GetByIdAsync(model.LifestyleId);
                if (exam is null || lifestyle is null) throw new DmsException("AP-EL01", 400, "Related exam/lifestyle does not exist.");

                var examLifestyle = await _examLifestylePersistence.GetByRelatedIdAsync(model.ExamId, model.LifestyleId);
                if (examLifestyle is not null) throw new DmsException("AP-EL02", 409, "This related lifestyle already exists.");

                var toAddExamLifestyle = _mapper.Map<ExamLifestyle>(model);
                _examLifestylePersistence.Add<ExamLifestyle>(toAddExamLifestyle);

                var save = await _examLifestylePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-EL03", 500, "Unexpected error saving this related lifestyle from the database.");

                var newExamLifestyle = await _examLifestylePersistence.GetByRelatedIdAsync(model.ExamId, model.LifestyleId);
                if (newExamLifestyle is null) throw new DmsException("AP-EL04", 500, "Unexpected error adding related lifestyle.");
                
                return _mapper.Map<ExamLifestyleDto>(newExamLifestyle);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-EL05", 500, $"Unexpected error adding related lifestyle: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var examLifestyle = await _examLifestylePersistence.GetByIdAsync(id);
                if (examLifestyle is null) throw new DmsException("AP-EL06", 404, "This related lifestyle does not exist.");

                _examLifestylePersistence.Delete<ExamLifestyle>(examLifestyle);
                var save = await _examLifestylePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-EL07", 500, "Unexpected error deleting this related lifestyle from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-EL08", 500, $"Unexpected error deleting related lifestyle: {ex.Message}");
            }
        }
    }
}