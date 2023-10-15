
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class MealPeriodService : IMealPeriodService
    {
        private readonly IMealPeriodPersistence _mealPeriodPersistence;
        private readonly IMealPersistence _mealPersistence;
        private readonly IMapper _mapper;
        public MealPeriodService(IMealPeriodPersistence mealPeriodPersistence, IMealPersistence mealPersistence, IMapper mapper)
        {
            _mealPeriodPersistence = mealPeriodPersistence;
            _mealPersistence = mealPersistence;
            _mapper = mapper;
            
        }
        public async Task<MealPeriodDto> Add(MealPeriodDto model)
        {
            try
            {
                var mealTimeId = model.MealTime;
                var meal = await _mealPersistence.GetByIdAsync(model.MealId);
                if (!Enum.IsDefined(typeof(MealTime), mealTimeId) || meal is null) throw new DmsException("AP-DRD01", 400, "Related meal period/meal does not exist.");

                var mealPeriod = await _mealPeriodPersistence.GetByRelatedIdAsync(model.MealId, model.MealTime);
                if (mealPeriod is not null) throw new DmsException("AP-DRD02", 409, "This meal period already exists.");

                var toAddMealPeriod = _mapper.Map<MealPeriod>(model);
                _mealPeriodPersistence.Add<MealPeriod>(toAddMealPeriod);

                var save = await _mealPeriodPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DRD03", 500, "Unexpected error saving this meal period from the database.");

                var newMealPeriod = await _mealPeriodPersistence.GetByRelatedIdAsync(model.MealId, model.MealTime);
                if (newMealPeriod is null) throw new DmsException("AP-DRD04", 500, "Unexpected error adding meal period.");
                
                return _mapper.Map<MealPeriodDto>(newMealPeriod);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD05", 500, $"Unexpected error adding meal period: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var mealPeriod = await _mealPeriodPersistence.GetByIdAsync(id);
                if (mealPeriod is null) throw new DmsException("AP-DRD06", 404, "This meal period does not exist.");

                _mealPeriodPersistence.Delete<MealPeriod>(mealPeriod);
                var save = await _mealPeriodPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DRD07", 500, "Unexpected error deleting this meal period from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD08", 500, $"Unexpected error deleting meal period: {ex.Message}");
            }
        }
    }
}