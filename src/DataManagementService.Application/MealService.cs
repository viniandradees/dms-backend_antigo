
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class MealService : IMealService
    {
        private readonly IMealPersistence _mealPersistence;
        private readonly IMapper _mapper;
        public MealService(IMealPersistence mealPersistence, IMapper mapper)
        {
            _mealPersistence = mealPersistence;
            _mapper = mapper;
            
        }

        public async Task<MealDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var meals = await _mealPersistence.GetAllAsync(getFullData);
                if (meals is null) return null;

                return _mapper.Map<MealDto[]>(meals);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI01", 500, $"Unexpected error getting meals: {ex.Message}");
            }
        }

        public async Task<MealDto> GetByIdAsync(int id)
        {
            try
            {
                var meals = await _mealPersistence.GetByIdAsync(id);
                if (meals is null) return null;

                return _mapper.Map<MealDto>(meals);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI02", 500, $"Unexpected error getting meal: {ex.Message}");
            }
        }

        public async Task<MealDto> GetByNameAsync(string name)
        {
            try
            {
                var meals = await _mealPersistence.GetByNameAsync(name);
                if (meals is null) return null;

                return _mapper.Map<MealDto>(meals);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI03", 500, $"Unexpected error getting meal: {ex.Message}");
            }
        }
        public async Task<MealDto> Add(MealDto model)
        {
            try
            {
                var meal = _mapper.Map<Meal>(model);
                _mealPersistence.Add<Meal>(meal);

                var checkName = await _mealPersistence.GetByNameAsync(meal.Name);
                if (checkName is not null) throw new DmsException("AP-DI04", 409, "Meal name already exists.");
                
                var save = await _mealPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DI05", 500, "Unexpected error adding this meal from the database.");

                var mealDto = await _mealPersistence.GetByIdAsync(meal.Id);
                if (mealDto is null) throw new DmsException("AP-DI06", 500, "Unexpected error adding meal.");

                return _mapper.Map<MealDto>(mealDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI07", 500, $"Unexpected error adding meal: {ex.Message}");
            }
            
        }

        public async Task<MealDto> Update(int id, MealDto model)
        {
            try
            {
                var checkMeal = await _mealPersistence.GetByIdAsync(id);
                if (checkMeal is null) throw new DmsException("AP-DI08", 404, "This meal does not exist.");

                model.Id = id;

                if (checkMeal.Name != model.Name) {
                    var checkName = await _mealPersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-DI09", 409, "Meal name already exists.");
                }

                var meal = _mapper.Map<Meal>(model);
                _mealPersistence.Update<Meal>(meal);

                var save = await _mealPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DI10", 500, "Unexpected error updating this meal from the database.");
                
                var result = await _mealPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DI11", 500, "Unexpected error updating meal.");
                return _mapper.Map<MealDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI12", 500, $"Unexpected error updating meal: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var meal = await _mealPersistence.GetByIdAsync(id);
                if (meal is null) throw new DmsException("AP-DI13", 404, "This meal does not exist.");

                _mealPersistence.Delete<Meal>(meal);
                return await _mealPersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI14", 500, $"Unexpected error deleting meal: {ex.Message}");
            }
        }
    }
}