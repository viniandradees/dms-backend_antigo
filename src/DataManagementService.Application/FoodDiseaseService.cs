
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class FoodDiseaseService : IFoodDiseaseService
    {
        private readonly IFoodDiseasePersistence _foodDiseasePersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly IFoodPersistence _foodPersistence;
        private readonly IMapper _mapper;
        public FoodDiseaseService(IFoodDiseasePersistence foodDiseasePersistence, IDiseasePersistence diseasePersistence, IFoodPersistence foodPersistence, IMapper mapper)
        {
            _foodDiseasePersistence = foodDiseasePersistence;
            _diseasePersistence = diseasePersistence;
            _foodPersistence = foodPersistence;
            _mapper = mapper;
            
        }
        public async Task<FoodDiseaseDto> Add(FoodDiseaseDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var food = await _foodPersistence.GetByIdAsync(model.FoodId);
                if (disease is null || food is null) throw new DmsException("AP-DRD01", 400, "Related food side effect/food does not exist.");

                var foodDisease = await _foodDiseasePersistence.GetByRelatedIdAsync(model.FoodId, model.DiseaseId);
                if (foodDisease is not null) throw new DmsException("AP-DRD02", 409, "This food side effect already exists.");

                var toAddFoodDisease = _mapper.Map<FoodDisease>(model);
                _foodDiseasePersistence.Add<FoodDisease>(toAddFoodDisease);

                var save = await _foodDiseasePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DRD03", 500, "Unexpected error saving this food side effect from the database.");

                var newFoodDisease = await _foodDiseasePersistence.GetByRelatedIdAsync(model.FoodId, model.DiseaseId);
                if (newFoodDisease is null) throw new DmsException("AP-DRD04", 500, "Unexpected error adding food side effect.");
                
                return _mapper.Map<FoodDiseaseDto>(newFoodDisease);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD05", 500, $"Unexpected error adding food side effect: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var foodDisease = await _foodDiseasePersistence.GetByIdAsync(id);
                if (foodDisease is null) throw new DmsException("AP-DRD06", 404, "This food side effect does not exist.");

                _foodDiseasePersistence.Delete<FoodDisease>(foodDisease);
                var save = await _foodDiseasePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DRD07", 500, "Unexpected error deleting this food side effect from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD08", 500, $"Unexpected error deleting food side effect: {ex.Message}");
            }
        }
    }
}