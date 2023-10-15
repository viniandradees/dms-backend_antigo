using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IFoodHealtyObjectivePersistence : IGeneralPersistence
    {  
        Task<FoodHealtyObjective> GetByIdAsync(int id);
        Task<FoodHealtyObjective> GetByRelatedIdAsync(int foodId, int HealtyObjectiveId);
    }
}