
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class MealCountryService : IMealCountryService
    {
        private readonly IMealCountryPersistence _mealCountryPersistence;
        private readonly ICountryPersistence _countryPersistence;
        private readonly IMealPersistence _mealPersistence;
        private readonly IMapper _mapper;
        public MealCountryService(IMealCountryPersistence mealCountryPersistence, ICountryPersistence countryPersistence, IMealPersistence mealPersistence, IMapper mapper)
        {
            _mealCountryPersistence = mealCountryPersistence;
            _countryPersistence = countryPersistence;
            _mealPersistence = mealPersistence;
            _mapper = mapper;
            
        }
        public async Task<MealCountryDto> Add(MealCountryDto model)
        {
            try
            {
                var country = await _countryPersistence.GetByIdAsync(model.CountryId);
                var meal = await _mealPersistence.GetByIdAsync(model.MealId);
                if (country is null || meal is null) throw new DmsException("AP-DRD01", 400, "Related meal international cuisine/meal does not exist.");

                var mealCountry = await _mealCountryPersistence.GetByRelatedIdAsync(model.MealId, model.CountryId);
                if (mealCountry is not null) throw new DmsException("AP-DRD02", 409, "This meal international cuisine already exists.");

                var toAddMealCountry = _mapper.Map<MealCountry>(model);
                _mealCountryPersistence.Add<MealCountry>(toAddMealCountry);

                var save = await _mealCountryPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DRD03", 500, "Unexpected error saving this meal international cuisine from the database.");

                var newMealCountry = await _mealCountryPersistence.GetByRelatedIdAsync(model.MealId, model.CountryId);
                if (newMealCountry is null) throw new DmsException("AP-DRD04", 500, "Unexpected error adding meal international cuisine.");
                
                return _mapper.Map<MealCountryDto>(newMealCountry);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD05", 500, $"Unexpected error adding meal international cuisine: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var mealCountry = await _mealCountryPersistence.GetByIdAsync(id);
                if (mealCountry is null) throw new DmsException("AP-DRD06", 404, "This meal international cuisine does not exist.");

                _mealCountryPersistence.Delete<MealCountry>(mealCountry);
                var save = await _mealCountryPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DRD07", 500, "Unexpected error deleting this meal international cuisine from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DRD08", 500, $"Unexpected error deleting meal international cuisine: {ex.Message}");
            }
        }
    }
}