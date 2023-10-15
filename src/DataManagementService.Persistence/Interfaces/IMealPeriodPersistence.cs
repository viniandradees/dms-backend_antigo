using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Domain.Enum;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IMealPeriodPersistence : IGeneralPersistence
    {  
        Task<MealPeriod> GetByIdAsync(int id);
        Task<MealPeriod> GetByRelatedIdAsync(int mealId, MealTime mealTime);
    }
}