
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class MealFoodService : IMealFoodService
    {
        private readonly IMealFoodPersistence _mealFoodPersistence;
        private readonly IMealPersistence _mealPersistence;
        private readonly IFoodPersistence _foodPersistence;
        private readonly IMapper _mapper;
        public MealFoodService(IMealFoodPersistence mealFoodPersistence, IMealPersistence mealPersistence, IFoodPersistence foodPersistence, IMapper mapper)
        {
            _mealFoodPersistence = mealFoodPersistence;
            _mealPersistence = mealPersistence;
            _foodPersistence = foodPersistence;
            _mapper = mapper;
            
        }
        public async Task<MealFoodDto> Add(MealFoodDto model)
        {
            try
            {
                var meal = await _mealPersistence.GetByIdAsync(model.MealId);
                var food = await _foodPersistence.GetByIdAsync(model.FoodId);
                if (meal is null || food is null) throw new DmsException("AP-MF01", 400, "Related meal/ingredient does not exist.");

                var mealFood = await _mealFoodPersistence.GetByRelatedIdAsync(model.MealId, model.FoodId);
                if (mealFood is not null) throw new DmsException("AP-MF02", 409, "This meal ingredient already exists.");

                foreach (var mealDietaryOption in meal.MealDietaryOptions) // check permission to add this MealFood with related incompatibilities
                {
                    bool check = mealDietaryOption.DietaryOption.Incompatibilities.Any(a => food.Attributes.Any(b => b.FoodAttributeId == a.FoodAttributeId));
                    if (check) throw new DmsException("AP-MF03", 409, "This nutrient is incompatible with some dietary option in this meal.");
                }
                
                var toAddMealFood = _mapper.Map<MealFood>(model);
                _mealFoodPersistence.Add<MealFood>(toAddMealFood);

                var save = await _mealFoodPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-MF03", 500, "Unexpected error saving this meal ingredient from the database.");

                var newMealFood = await _mealFoodPersistence.GetByRelatedIdAsync(model.MealId, model.FoodId);
                if (newMealFood is null) throw new DmsException("AP-MF04", 500, "Unexpected error adding meal ingredient.");
                
                return _mapper.Map<MealFoodDto>(newMealFood);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-MF05", 500, $"Unexpected error adding meal ingredient: {ex.Message}");
            }
            
        }

        public async Task<MealFoodDto> Update(int id, MealFoodDto model)
        {
            try
            {
                var checkMealFood = await _mealFoodPersistence.GetByIdAsync(id);
                if (checkMealFood is null) throw new DmsException("AP-MF06", 404, "This meal ingredient does not exist.");

                model.Id = id;

                var mealFood = _mapper.Map<MealFood>(model);
                _mealFoodPersistence.Update(mealFood);

                var save = await _mealFoodPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-MF07", 500, "Unexpected error updating this meal ingredient from the database.");
                
                var result = await _mealFoodPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-MF08", 500, "Unexpected error updating meal ingredient.");
                return _mapper.Map<MealFoodDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-MF09", 500, $"Unexpected error updating meal ingredient: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var mealFood = await _mealFoodPersistence.GetByIdAsync(id);
                if (mealFood is null) throw new DmsException("AP-MF10", 404, "This meal ingredient does not exist.");

                _mealFoodPersistence.Delete<MealFood>(mealFood);
                var save = await _mealFoodPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-MF11", 500, "Unexpected error deleting this meal ingredient from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-MF12", 500, $"Unexpected error deleting meal ingredient: {ex.Message}");
            }
        }
    }
}