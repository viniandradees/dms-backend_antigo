
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class MealDietaryOptionService : IMealDietaryOptionService
    {
        private readonly IMealDietaryOptionPersistence _mealDietaryOptionPersistence;
        private readonly IDietaryOptionPersistence _dietaryOptionPersistence;
        private readonly IMealPersistence _mealPersistence;
        private readonly IMapper _mapper;
        public MealDietaryOptionService(IMealDietaryOptionPersistence mealDietaryOptionPersistence, IDietaryOptionPersistence dietaryOptionPersistence, IMealPersistence mealPersistence, IMapper mapper)
        {
            _mealDietaryOptionPersistence = mealDietaryOptionPersistence;
            _dietaryOptionPersistence = dietaryOptionPersistence;
            _mealPersistence = mealPersistence;
            _mapper = mapper;
            
        }
        public async Task<MealDietaryOptionDto> Add(MealDietaryOptionDto model)
        {
            try
            {
                var dietaryOption = await _dietaryOptionPersistence.GetByIdAsync(model.DietaryOptionId);
                var meal = await _mealPersistence.GetByIdAsync(model.MealId);
                if (dietaryOption is null || meal is null) throw new DmsException("AP-DRD01", 400, "Related meal dietary option/meal does not exist.");

                var mealDietaryOption = await _mealDietaryOptionPersistence.GetByRelatedIdAsync(model.MealId, model.DietaryOptionId);
                if (mealDietaryOption is not null) throw new DmsException("AP-DRD02", 409, "This meal dietary option already exists.");
                
                foreach (var ingredient in meal.Ingredients) // check permission to add this mealdietaryoption with related incompatibilities
                {
                    bool check = ingredient.Food.Attributes.Any(a => dietaryOption.Incompatibilities.Any(b => b.FoodAttributeId == a.FoodAttributeId));
                    if (check) throw new DmsException("AP-DRD03", 409, "This meal have incompatible nutrients for this dietary option.");
                }

                var toAddMealDietaryOption = _mapper.Map<MealDietaryOption>(model);
                _mealDietaryOptionPersistence.Add<MealDietaryOption>(toAddMealDietaryOption);

                var save = await _mealDietaryOptionPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DRD03", 500, "Unexpected error saving this meal dietary option from the database.");

                var newMealDietaryOption = await _mealDietaryOptionPersistence.GetByRelatedIdAsync(model.MealId, model.DietaryOptionId);
                if (newMealDietaryOption is null) throw new DmsException("AP-DRD04", 500, "Unexpected error adding meal dietary option.");
                
                return _mapper.Map<MealDietaryOptionDto>(newMealDietaryOption);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD05", 500, $"Unexpected error adding meal dietary option: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var mealDietaryOption = await _mealDietaryOptionPersistence.GetByIdAsync(id);
                if (mealDietaryOption is null) throw new DmsException("AP-DRD06", 404, "This meal dietary option does not exist.");

                _mealDietaryOptionPersistence.Delete<MealDietaryOption>(mealDietaryOption);
                var save = await _mealDietaryOptionPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DRD07", 500, "Unexpected error deleting this meal dietary option from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD08", 500, $"Unexpected error deleting meal dietary option: {ex.Message}");
            }
        }
    }
}