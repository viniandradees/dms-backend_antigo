
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class FoodSupplementService : IFoodSupplementService
    {
        private readonly IFoodSupplementPersistence _foodSupplementPersistence;
        private readonly IFoodPersistence _foodPersistence;
        private readonly ISupplementPersistence _supplementPersistence;
        private readonly IMapper _mapper;
        public FoodSupplementService(IFoodSupplementPersistence foodSupplementPersistence, IFoodPersistence foodPersistence, ISupplementPersistence supplementPersistence, IMapper mapper)
        {
            _foodSupplementPersistence = foodSupplementPersistence;
            _foodPersistence = foodPersistence;
            _supplementPersistence = supplementPersistence;
            _mapper = mapper;
            
        }
        public async Task<FoodSupplementDto> Add(FoodSupplementDto model)
        {
            try
            {
                var food = await _foodPersistence.GetByIdAsync(model.FoodId);
                var supplement = await _supplementPersistence.GetByIdAsync(model.SupplementId);
                if (food is null || supplement is null) throw new DmsException("AP-FS01", 400, "Related food/nutrient does not exist.");

                var foodSupplement = await _foodSupplementPersistence.GetByRelatedIdAsync(model.FoodId, model.SupplementId);
                if (foodSupplement is not null) throw new DmsException("AP-FS02", 409, "This food nutrient already exists.");
                
                var toAddFoodSupplement = _mapper.Map<FoodSupplement>(model);
                _foodSupplementPersistence.Add<FoodSupplement>(toAddFoodSupplement);

                var save = await _foodSupplementPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-FS03", 500, "Unexpected error saving this food nutrient from the database.");

                var newFoodSupplement = await _foodSupplementPersistence.GetByRelatedIdAsync(model.FoodId, model.SupplementId);
                if (newFoodSupplement is null) throw new DmsException("AP-FS04", 500, "Unexpected error adding food nutrient.");
                
                return _mapper.Map<FoodSupplementDto>(newFoodSupplement);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FS05", 500, $"Unexpected error adding food nutrient: {ex.Message}");
            }
            
        }

        public async Task<FoodSupplementDto> Update(int id, FoodSupplementDto model)
        {
            try
            {
                var checkFoodSupplement = await _foodSupplementPersistence.GetByIdAsync(id);
                if (checkFoodSupplement is null) throw new DmsException("AP-FS06", 404, "This food nutrient does not exist.");

                model.Id = id;

                var foodSupplement = _mapper.Map<FoodSupplement>(model);
                _foodSupplementPersistence.Update(foodSupplement);

                var save = await _foodSupplementPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-FS07", 500, "Unexpected error updating this food nutrient from the database.");
                
                var result = await _foodSupplementPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-FS08", 500, "Unexpected error updating food nutrient.");
                return _mapper.Map<FoodSupplementDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FS09", 500, $"Unexpected error updating food nutrient: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var foodSupplement = await _foodSupplementPersistence.GetByIdAsync(id);
                if (foodSupplement is null) throw new DmsException("AP-FS10", 404, "This food nutrient does not exist.");

                _foodSupplementPersistence.Delete<FoodSupplement>(foodSupplement);
                var save = await _foodSupplementPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-FS11", 500, "Unexpected error deleting this food nutrient from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FS12", 500, $"Unexpected error deleting food nutrient: {ex.Message}");
            }
        }
    }
}