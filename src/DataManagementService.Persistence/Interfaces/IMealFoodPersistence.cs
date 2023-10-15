using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IMealFoodPersistence : IGeneralPersistence
    {  
        Task<MealFood> GetByIdAsync(int id);
        Task<MealFood> GetByRelatedIdAsync(int mealId, int foodId);
    }
}