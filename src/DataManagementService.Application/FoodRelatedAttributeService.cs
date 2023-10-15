
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class FoodRelatedAttributeService : IFoodRelatedAttributeService
    {
        private readonly IFoodRelatedAttributePersistence _foodRelatedAttributePersistence;
        private readonly IFoodPersistence _foodPersistence;
        private readonly IFoodAttributePersistence _foodAttributePersistence;
        private readonly IMapper _mapper;
        public FoodRelatedAttributeService(IFoodRelatedAttributePersistence foodRelatedAttributePersistence, IFoodPersistence foodPersistence, IFoodAttributePersistence foodAttributePersistence, IMapper mapper)
        {
            _foodRelatedAttributePersistence = foodRelatedAttributePersistence;
            _foodPersistence = foodPersistence;
            _foodAttributePersistence = foodAttributePersistence;
            _mapper = mapper;
            
        }
        public async Task<FoodRelatedAttributeDto> Add(FoodRelatedAttributeDto model)
        {
            try
            {
                var food = await _foodPersistence.GetByIdAsync(model.FoodId);
                var foodAttribute = await _foodAttributePersistence.GetByIdAsync(model.FoodAttributeId);
                if (food is null || foodAttribute is null) throw new DmsException("AP-FRA01", 400, "Related food/food attribute does not exist.");

                var foodRelatedAttribute = await _foodRelatedAttributePersistence.GetByRelatedIdAsync(model.FoodId, model.FoodAttributeId);
                if (foodRelatedAttribute is not null) throw new DmsException("AP-FRA02", 409, "This food attribute already exists in this food.");

                var toAddFoodRelatedAttribute = _mapper.Map<FoodRelatedAttribute>(model);
                _foodRelatedAttributePersistence.Add<FoodRelatedAttribute>(toAddFoodRelatedAttribute);

                var save = await _foodRelatedAttributePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-FRA03", 500, "Unexpected error saving this food attribute from the database.");

                var newFoodRelatedAttribute = await _foodRelatedAttributePersistence.GetByRelatedIdAsync(model.FoodId, model.FoodAttributeId);
                if (newFoodRelatedAttribute is null) throw new DmsException("AP-FRA04", 500, "Unexpected error adding food attribute in this food.");
                
                return _mapper.Map<FoodRelatedAttributeDto>(newFoodRelatedAttribute);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FRA05", 500, $"Unexpected error adding food attribute in this food: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var foodRelatedAttribute = await _foodRelatedAttributePersistence.GetByIdAsync(id);
                if (foodRelatedAttribute is null) throw new DmsException("AP-FRA06", 404, "This food attribute does not exist.");

                _foodRelatedAttributePersistence.Delete<FoodRelatedAttribute>(foodRelatedAttribute);
                var save = await _foodRelatedAttributePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-FRA07", 500, "Unexpected error deleting this related food attribute from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FRA08", 500, $"Unexpected error deleting food attribute of this food: {ex.Message}");
            }
        }
    }
}