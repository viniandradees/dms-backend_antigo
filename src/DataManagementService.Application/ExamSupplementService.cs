
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class ExamSupplementService : IExamSupplementService
    {
        private readonly IExamSupplementPersistence _examSupplementPersistence;
        private readonly IExamPersistence _examPersistence;
        private readonly ISupplementPersistence _supplementPersistence;
        private readonly IMapper _mapper;
        public ExamSupplementService(IExamSupplementPersistence examSupplementPersistence, IExamPersistence examPersistence, ISupplementPersistence supplementPersistence, IMapper mapper)
        {
            _examSupplementPersistence = examSupplementPersistence;
            _examPersistence = examPersistence;
            _supplementPersistence = supplementPersistence;
            _mapper = mapper;
            
        }
        public async Task<ExamSupplementDto> Add(ExamSupplementDto model)
        {
            try
            {
                var exam = await _examPersistence.GetByIdAsync(model.ExamId);
                var supplement = await _supplementPersistence.GetByIdAsync(model.SupplementId);
                if (exam is null || supplement is null) throw new DmsException("AP-ES01", 400, "Related exam/supplement does not exist.");

                var examSupplement = await _examSupplementPersistence.GetByRelatedIdAsync(model.ExamId, model.SupplementId);
                if (examSupplement is not null) throw new DmsException("AP-ES02", 409, "This related supplement already exists.");

                var toAddExamSupplement = _mapper.Map<ExamSupplement>(model);
                _examSupplementPersistence.Add<ExamSupplement>(toAddExamSupplement);

                var save = await _examSupplementPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-ES03", 500, "Unexpected error saving this related supplement from the database.");

                var newExamSupplement = await _examSupplementPersistence.GetByRelatedIdAsync(model.ExamId, model.SupplementId);
                if (newExamSupplement is null) throw new DmsException("AP-ES04", 500, "Unexpected error adding related supplement.");
                
                return _mapper.Map<ExamSupplementDto>(newExamSupplement);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ES05", 500, $"Unexpected error adding related supplement: {ex.Message}");
            }
            
        }

        public async Task<ExamSupplementDto> Update(int id, ExamSupplementDto model)
        {
            try
            {
                var checkExamSupplement = await _examSupplementPersistence.GetByIdAsync(id);
                if (checkExamSupplement is null) throw new DmsException("AP-ES06", 404, "This related supplement does not exist.");

                model.Id = id;

                var examSupplement = _mapper.Map<ExamSupplement>(model);
                _examSupplementPersistence.Update(examSupplement);

                var save = await _examSupplementPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-ES07", 500, "Unexpected error updating this related supplement from the database.");
                
                var result = await _examSupplementPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-ES08", 500, "Unexpected error updating related supplement.");
                return _mapper.Map<ExamSupplementDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ES09", 500, $"Unexpected error updating related supplement: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var examSupplement = await _examSupplementPersistence.GetByIdAsync(id);
                if (examSupplement is null) throw new DmsException("AP-ES10", 404, "This related supplement does not exist.");

                _examSupplementPersistence.Delete<ExamSupplement>(examSupplement);
                var save = await _examSupplementPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-ES11", 500, "Unexpected error deleting this related supplement from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-ES12", 500, $"Unexpected error deleting related supplement: {ex.Message}");
            }
        }
    }
}