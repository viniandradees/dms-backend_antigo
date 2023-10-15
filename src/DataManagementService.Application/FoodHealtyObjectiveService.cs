
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class FoodHealtyObjectiveService : IFoodHealtyObjectiveService
    {
        private readonly IFoodHealtyObjectivePersistence _foodHealtyObjectivePersistence;
        private readonly IHealtyObjectivePersistence _healtyObjectivePersistence;
        private readonly IFoodPersistence _foodPersistence;
        private readonly IMapper _mapper;
        public FoodHealtyObjectiveService(IFoodHealtyObjectivePersistence foodHealtyObjectivePersistence, IHealtyObjectivePersistence healtyObjectivePersistence, IFoodPersistence foodPersistence, IMapper mapper)
        {
            _foodHealtyObjectivePersistence = foodHealtyObjectivePersistence;
            _healtyObjectivePersistence = healtyObjectivePersistence;
            _foodPersistence = foodPersistence;
            _mapper = mapper;
            
        }
        public async Task<FoodHealtyObjectiveDto> Add(FoodHealtyObjectiveDto model)
        {
            try
            {
                var healtyObjective = await _healtyObjectivePersistence.GetByIdAsync(model.HealtyObjectiveId);
                var food = await _foodPersistence.GetByIdAsync(model.FoodId);
                if (healtyObjective is null || food is null) throw new DmsException("AP-FHO01", 400, "Related related food healty objective/food does not exist.");

                var foodHealtyObjective = await _foodHealtyObjectivePersistence.GetByRelatedIdAsync(model.FoodId, model.HealtyObjectiveId);
                if (foodHealtyObjective is not null) throw new DmsException("AP-FHO02", 409, "This related food healty objective already exists.");

                var toAddFoodHealtyObjective = _mapper.Map<FoodHealtyObjective>(model);
                _foodHealtyObjectivePersistence.Add<FoodHealtyObjective>(toAddFoodHealtyObjective);

                var save = await _foodHealtyObjectivePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-FHO03", 500, "Unexpected error saving this related food healty objective from the database.");

                var newFoodHealtyObjective = await _foodHealtyObjectivePersistence.GetByRelatedIdAsync(model.FoodId, model.HealtyObjectiveId);
                if (newFoodHealtyObjective is null) throw new DmsException("AP-FHO04", 500, "Unexpected error adding related food healty objective.");
                
                return _mapper.Map<FoodHealtyObjectiveDto>(newFoodHealtyObjective);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FHO05", 500, $"Unexpected error adding related food healty objective: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var foodHealtyObjective = await _foodHealtyObjectivePersistence.GetByIdAsync(id);
                if (foodHealtyObjective is null) throw new DmsException("AP-FHO06", 404, "This related food healty objective does not exist.");

                _foodHealtyObjectivePersistence.Delete<FoodHealtyObjective>(foodHealtyObjective);
                var save = await _foodHealtyObjectivePersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-FHO07", 500, "Unexpected error deleting this related food healty objective from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-FHO08", 500, $"Unexpected error deleting related food healty objective: {ex.Message}");
            }
        }
    }
}