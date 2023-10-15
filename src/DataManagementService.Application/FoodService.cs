
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class FoodService : IFoodService
    {
        private readonly IFoodPersistence _foodPersistence;
        private readonly IMapper _mapper;
        public FoodService(IFoodPersistence foodPersistence, IMapper mapper)
        {
            _foodPersistence = foodPersistence;
            _mapper = mapper;
            
        }

        public async Task<FoodDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var foods = await _foodPersistence.GetAllAsync(getFullData);
                if (foods is null) return null;

                return _mapper.Map<FoodDto[]>(foods);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-F01", 500, $"Unexpected error getting foods: {ex.Message}");
            }
        }

        public async Task<FoodDto> GetByIdAsync(int id)
        {
            try
            {
                var foods = await _foodPersistence.GetByIdAsync(id);
                if (foods is null) return null;

                return _mapper.Map<FoodDto>(foods);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-F02", 500, $"Unexpected error getting food: {ex.Message}");
            }
        }

        public async Task<FoodDto> GetByNameAsync(string name)
        {
            try
            {
                var foods = await _foodPersistence.GetByNameAsync(name);
                if (foods is null) return null;

                return _mapper.Map<FoodDto>(foods);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-F03", 500, $"Unexpected error getting food: {ex.Message}");
            }
        }
        public async Task<FoodDto> Add(FoodDto model)
        {
            try
            {
                var food = _mapper.Map<Food>(model);
                _foodPersistence.Add<Food>(food);

                var checkName = await _foodPersistence.GetByNameAsync(food.Name);
                if (checkName is not null) throw new DmsException("AP-F04", 409, "Food name already exists.");
                
                var save = await _foodPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-F05", 500, "Unexpected error adding this food from the database.");

                var foodDto = await _foodPersistence.GetByIdAsync(food.Id);
                if (foodDto is null) throw new DmsException("AP-F06", 500, "Unexpected error adding food.");

                return _mapper.Map<FoodDto>(foodDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-F07", 500, $"Unexpected error adding food: {ex.Message}");
            }
            
        }

        public async Task<FoodDto> Update(int id, FoodDto model)
        {
            try
            {
                var checkFood = await _foodPersistence.GetByIdAsync(id);
                if (checkFood is null) throw new DmsException("AP-F08", 404, "This food does not exist.");

                model.Id = id;

                if (checkFood.Name != model.Name) {
                    var checkName = await _foodPersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-F09", 409, "Food name already exists.");
                }

                var food = _mapper.Map<Food>(model);
                _foodPersistence.Update<Food>(food);

                var save = await _foodPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-F10", 500, "Unexpected error updating this food from the database.");
                
                var result = await _foodPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-F11", 500, "Unexpected error updating food.");
                return _mapper.Map<FoodDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-F12", 500, $"Unexpected error updating food: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var food = await _foodPersistence.GetByIdAsync(id);
                if (food is null) throw new DmsException("AP-F13", 404, "This food does not exist.");

                _foodPersistence.Delete<Food>(food);
                return await _foodPersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-F14", 500, $"Unexpected error deleting food: {ex.Message}");
            }
        }
    }
}