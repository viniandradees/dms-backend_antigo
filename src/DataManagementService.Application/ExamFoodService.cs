
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class ExamFoodService : IExamFoodService
    {
        private readonly IExamFoodPersistence _examFoodPersistence;
        private readonly IExamPersistence _examPersistence;
        private readonly IFoodPersistence _foodPersistence;
        private readonly IMapper _mapper;
        public ExamFoodService(IExamFoodPersistence examFoodPersistence, IExamPersistence examPersistence, IFoodPersistence foodPersistence, IMapper mapper)
        {
            _examFoodPersistence = examFoodPersistence;
            _examPersistence = examPersistence;
            _foodPersistence = foodPersistence;
            _mapper = mapper;
            
        }
        public async Task<ExamFoodDto> Add(ExamFoodDto model)
        {
            try
            {
                var exam = await _examPersistence.GetByIdAsync(model.ExamId);
                var food = await _foodPersistence.GetByIdAsync(model.FoodId);
                if (exam is null || food is null) throw new DmsException("AP-EF01", 400, "Related exam/food does not exist.");

                var examFood = await _examFoodPersistence.GetByRelatedIdAsync(model.ExamId, model.FoodId);
                if (examFood is not null) throw new DmsException("AP-EF02", 409, "This related food already exists.");

                var toAddExamFood = _mapper.Map<ExamFood>(model);
                _examFoodPersistence.Add<ExamFood>(toAddExamFood);

                var save = await _examFoodPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-EF03", 500, "Unexpected error saving this related food from the database.");

                var newExamFood = await _examFoodPersistence.GetByRelatedIdAsync(model.ExamId, model.FoodId);
                if (newExamFood is null) throw new DmsException("AP-EF04", 500, "Unexpected error adding related food.");
                
                return _mapper.Map<ExamFoodDto>(newExamFood);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-EF05", 500, $"Unexpected error adding related food: {ex.Message}");
            } 
        }

        public async Task<ExamFoodDto> Update(int id, ExamFoodDto model)
        {
            try
            {
                var checkExamFood = await _examFoodPersistence.GetByIdAsync(id);
                if (checkExamFood is null) throw new DmsException("AP-EF06", 404, "This related food does not exist.");

                model.Id = id;

                var examFood = _mapper.Map<ExamFood>(model);
                _examFoodPersistence.Update(examFood);

                var save = await _examFoodPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-EF07", 500, "Unexpected error updating this related food from the database.");
                
                var result = await _examFoodPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-EF08", 500, "Unexpected error updating related food.");
                return _mapper.Map<ExamFoodDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-EF09", 500, $"Unexpected error updating related food: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var examFood = await _examFoodPersistence.GetByIdAsync(id);
                if (examFood is null) throw new DmsException("AP-EF06", 404, "This related food does not exist.");

                _examFoodPersistence.Delete<ExamFood>(examFood);
                var save = await _examFoodPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-EF07", 500, "Unexpected error deleting this related food from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-EF08", 500, $"Unexpected error deleting related food: {ex.Message}");
            }
        }
    }
}