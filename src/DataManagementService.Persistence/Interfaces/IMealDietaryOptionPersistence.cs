using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IMealDietaryOptionPersistence : IGeneralPersistence
    {  
        Task<MealDietaryOption> GetByIdAsync(int id);
        Task<MealDietaryOption> GetByRelatedIdAsync(int mealId, int dietaryOptionId);
    }
}