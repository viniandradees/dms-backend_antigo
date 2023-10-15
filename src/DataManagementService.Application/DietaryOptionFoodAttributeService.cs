
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DietaryOptionFoodAttributeService : IDietaryOptionFoodAttributeService
    {
        private readonly IDietaryOptionFoodAttributePersistence _dietaryOptionFoodAttributePersistence;
        private readonly IFoodAttributePersistence _foodAttributePersistence;
        private readonly IDietaryOptionPersistence _dietaryOptionPersistence;
        private readonly IMapper _mapper;
        public DietaryOptionFoodAttributeService(IDietaryOptionFoodAttributePersistence dietaryOptionFoodAttributePersistence, IFoodAttributePersistence foodAttributePersistence, IDietaryOptionPersistence dietaryOptionPersistence, IMapper mapper)
        {
            _dietaryOptionFoodAttributePersistence = dietaryOptionFoodAttributePersistence;
            _foodAttributePersistence = foodAttributePersistence;
            _dietaryOptionPersistence = dietaryOptionPersistence;
            _mapper = mapper;
            
        }
        public async Task<DietaryOptionFoodAttributeDto> Add(DietaryOptionFoodAttributeDto model)
        {
            try
            {
                var foodAttribute = await _foodAttributePersistence.GetByIdAsync(model.FoodAttributeId);
                var dietaryOption = await _dietaryOptionPersistence.GetByIdAsync(model.DietaryOptionId);
                if (foodAttribute is null || dietaryOption is null) throw new DmsException("AP-DOFA01", 400, "Related dietary option/food attribute does not exist.");

                var dietaryOptionFoodAttribute = await _dietaryOptionFoodAttributePersistence.GetByRelatedIdAsync(model.DietaryOptionId, model.FoodAttributeId);
                if (dietaryOptionFoodAttribute is not null) throw new DmsException("AP-DOFA02", 409, "This dietary option incompatibility already exists.");

                var toAddDietaryOptionFoodAttribute = _mapper.Map<DietaryOptionFoodAttribute>(model);
                _dietaryOptionFoodAttributePersistence.Add<DietaryOptionFoodAttribute>(toAddDietaryOptionFoodAttribute);

                var save = await _dietaryOptionFoodAttributePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DOFA03", 500, "Unexpected error saving this dietary option incompatibility from the database.");

                var newDietaryOptionFoodAttribute = await _dietaryOptionFoodAttributePersistence.GetByRelatedIdAsync(model.DietaryOptionId, model.FoodAttributeId);
                if (newDietaryOptionFoodAttribute is null) throw new DmsException("AP-DOFA04", 500, "Unexpected error adding dietary option incompatibility.");
                
                return _mapper.Map<DietaryOptionFoodAttributeDto>(newDietaryOptionFoodAttribute);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DOFA05", 500, $"Unexpected error adding dietary option incompatibility: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var dietaryOptionFoodAttribute = await _dietaryOptionFoodAttributePersistence.GetByIdAsync(id);
                if (dietaryOptionFoodAttribute is null) throw new DmsException("AP-DOFA06", 404, "This dietary option incompatibility does not exist.");

                _dietaryOptionFoodAttributePersistence.Delete<DietaryOptionFoodAttribute>(dietaryOptionFoodAttribute);
                var save = await _dietaryOptionFoodAttributePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DOFA07", 500, "Unexpected error deleting this dietary option incompatibility from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DOFA08", 500, $"Unexpected error deleting dietary option incompatibility: {ex.Message}");
            }
        }
    }
}