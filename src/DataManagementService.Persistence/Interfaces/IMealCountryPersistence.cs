using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IMealCountryPersistence : IGeneralPersistence
    {  
        Task<MealCountry> GetByIdAsync(int id);
        Task<MealCountry> GetByRelatedIdAsync(int mealId, int countryId);
    }
}