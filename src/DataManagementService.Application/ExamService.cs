
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class ExamService : IExamService
    {
        private readonly IExamPersistence _examPersistence;
        private readonly IMapper _mapper;
        public ExamService(IExamPersistence examPersistence, IMapper mapper)
        {
            _examPersistence = examPersistence;
            _mapper = mapper;
            
        }

        public async Task<ExamDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var exams = await _examPersistence.GetAllAsync(getFullData);
                if (exams is null) return null;

                return _mapper.Map<ExamDto[]>(exams);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-E01", 500, $"Unexpected error getting exams: {ex.Message}");
            }
        }

        public async Task<ExamDto> GetByIdAsync(int id)
        {
            try
            {
                var exams = await _examPersistence.GetByIdAsync(id);
                if (exams is null) return null;

                return _mapper.Map<ExamDto>(exams);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-E02", 500, $"Unexpected error getting exam: {ex.Message}");
            }
        }

        public async Task<ExamDto> GetByNameAsync(string name)
        {
            try
            {
                var exams = await _examPersistence.GetByNameAsync(name);
                if (exams is null) return null;

                return _mapper.Map<ExamDto>(exams);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-E03", 500, $"Unexpected error getting exam: {ex.Message}");
            }
        }
        public async Task<ExamDto> Add(ExamDto model)
        {
            try
            {
                var exam = _mapper.Map<Exam>(model);
                _examPersistence.Add<Exam>(exam);

                var checkName = await _examPersistence.GetByNameAsync(exam.Name);
                if (checkName is not null) throw new DmsException("AP-E04", 409, "Exam name already exists.");
                
                var save = await _examPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-E05", 500, "Unexpected error adding this exam from the database.");

                var examDto = await _examPersistence.GetByIdAsync(exam.Id);
                if (examDto is null) throw new DmsException("AP-E06", 500, "Unexpected error adding exam.");

                return _mapper.Map<ExamDto>(examDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-E07", 500, $"Unexpected error adding exam: {ex.Message}");
            }
            
        }

        public async Task<ExamDto> Update(int id, ExamDto model)
        {
            try
            {
                var checkExam = await _examPersistence.GetByIdAsync(id);
                if (checkExam is null) throw new DmsException("AP-E08", 404, "This exam does not exist.");

                model.Id = id;

                if (checkExam.Name != model.Name) {
                    var checkName = await _examPersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-E09", 409, "Exam name already exists.");
                }

                var exam = _mapper.Map<Exam>(model);
                _examPersistence.Update<Exam>(exam);

                var save = await _examPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-E10", 500, "Unexpected error updating this exam from the database.");
                
                var result = await _examPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-E11", 500, "Unexpected error updating exam.");
                return _mapper.Map<ExamDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-E12", 500, $"Unexpected error updating exam: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var exam = await _examPersistence.GetByIdAsync(id);
                if (exam is null) throw new DmsException("AP-E13", 404, "This exam does not exist.");

                _examPersistence.Delete<Exam>(exam);
                return await _examPersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-E14", 500, $"Unexpected error deleting exam: {ex.Message}");
            }
        }
    }
}