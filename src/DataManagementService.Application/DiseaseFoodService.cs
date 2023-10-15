
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseFoodService : IDiseaseFoodService
    {
        private readonly IDiseaseFoodPersistence _diseaseFoodPersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly IFoodPersistence _foodPersistence;
        private readonly IMapper _mapper;
        public DiseaseFoodService(IDiseaseFoodPersistence diseaseFoodPersistence, IDiseasePersistence diseasePersistence, IFoodPersistence foodPersistence, IMapper mapper)
        {
            _diseaseFoodPersistence = diseaseFoodPersistence;
            _diseasePersistence = diseasePersistence;
            _foodPersistence = foodPersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseFoodDto> Add(DiseaseFoodDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var food = await _foodPersistence.GetByIdAsync(model.FoodId);
                if (disease is null || food is null) throw new DmsException("AP-DF01", 400, "Related disease/food does not exist.");

                var diseaseFood = await _diseaseFoodPersistence.GetByRelatedIdAsync(model.DiseaseId, model.FoodId);
                if (diseaseFood is not null) throw new DmsException("AP-DF02", 409, "This treatment food already exists.");

                var toAddDiseaseFood = _mapper.Map<DiseaseFood>(model);
                _diseaseFoodPersistence.Add<DiseaseFood>(toAddDiseaseFood);

                var save = await _diseaseFoodPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DF03", 500, "Unexpected error saving this treatment food from the database.");

                var newDiseaseFood = await _diseaseFoodPersistence.GetByRelatedIdAsync(model.DiseaseId, model.FoodId);
                if (newDiseaseFood is null) throw new DmsException("AP-DF04", 500, "Unexpected error adding treatment food.");
                
                return _mapper.Map<DiseaseFoodDto>(newDiseaseFood);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DF05", 500, $"Unexpected error adding treatment food: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseFood = await _diseaseFoodPersistence.GetByIdAsync(id);
                if (diseaseFood is null) throw new DmsException("AP-DF06", 404, "This treatment food does not exist.");

                _diseaseFoodPersistence.Delete<DiseaseFood>(diseaseFood);
                var save = await _diseaseFoodPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DF07", 500, "Unexpected error deleting this treatment food from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DF08", 500, $"Unexpected error deleting treatment food: {ex.Message}");
            }
        }
    }
}