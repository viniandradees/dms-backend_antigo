
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class FoodAttributeService : IFoodAttributeService
    {
        private readonly IFoodAttributePersistence _foodAttributePersistence;
        private readonly IMapper _mapper;
        public FoodAttributeService(IFoodAttributePersistence foodAttributePersistence, IMapper mapper)
        {
            _foodAttributePersistence = foodAttributePersistence;
            _mapper = mapper;
            
        }

        public async Task<FoodAttributeDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var foodAttributes = await _foodAttributePersistence.GetAllAsync(getFullData);
                if (foodAttributes is null) return null;

                return _mapper.Map<FoodAttributeDto[]>(foodAttributes);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FA01", 500, $"Unexpected error getting food attributes: {ex.Message}");
            }
        }

        public async Task<FoodAttributeDto> GetByIdAsync(int id)
        {
            try
            {
                var foodAttributes = await _foodAttributePersistence.GetByIdAsync(id);
                if (foodAttributes is null) return null;

                return _mapper.Map<FoodAttributeDto>(foodAttributes);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FA02", 500, $"Unexpected error getting food attribute: {ex.Message}");
            }
        }

        public async Task<FoodAttributeDto> GetByNameAsync(string name)
        {
            try
            {
                var foodAttributes = await _foodAttributePersistence.GetByNameAsync(name);
                if (foodAttributes is null) return null;

                return _mapper.Map<FoodAttributeDto>(foodAttributes);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FA03", 500, $"Unexpected error getting food attribute: {ex.Message}");
            }
        }
        public async Task<FoodAttributeDto> Add(FoodAttributeDto model)
        {
            try
            {
                var foodAttribute = _mapper.Map<FoodAttribute>(model);
                _foodAttributePersistence.Add<FoodAttribute>(foodAttribute);

                var checkName = await _foodAttributePersistence.GetByNameAsync(foodAttribute.Name);
                if (checkName is not null) throw new DmsException("AP-FA04", 409, "Food attribute name already exists.");
                
                var save = await _foodAttributePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-FA05", 500, "Unexpected error adding this food attribute from the database.");

                var foodAttributeDto = await _foodAttributePersistence.GetByIdAsync(foodAttribute.Id);
                if (foodAttributeDto is null) throw new DmsException("AP-FA06", 500, "Unexpected error adding food attribute.");

                return _mapper.Map<FoodAttributeDto>(foodAttributeDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FA07", 500, $"Unexpected error adding food attribute: {ex.Message}");
            }
            
        }

        public async Task<FoodAttributeDto> Update(int id, FoodAttributeDto model)
        {
            try
            {
                var checkFoodAttribute = await _foodAttributePersistence.GetByIdAsync(id);
                if (checkFoodAttribute is null) throw new DmsException("AP-FA08", 404, "This food attribute does not exist.");

                model.Id = id;

                if (checkFoodAttribute.Name != model.Name) {
                    var checkName = await _foodAttributePersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-FA09", 409, "Food attribute name already exists.");
                }

                var foodAttribute = _mapper.Map<FoodAttribute>(model);
                _foodAttributePersistence.Update<FoodAttribute>(foodAttribute);

                var save = await _foodAttributePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-FA10", 500, "Unexpected error updating this food attribute from the database.");
                
                var result = await _foodAttributePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-FA11", 500, "Unexpected error updating food attribute.");
                return _mapper.Map<FoodAttributeDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FA12", 500, $"Unexpected error updating food attribute: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var foodAttribute = await _foodAttributePersistence.GetByIdAsync(id);
                if (foodAttribute is null) throw new DmsException("AP-FA13", 404, "This food attribute does not exist.");

                _foodAttributePersistence.Delete<FoodAttribute>(foodAttribute);
                return await _foodAttributePersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FA14", 500, $"Unexpected error deleting food attribute: {ex.Message}");
            }
        }
    }
}