using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IFoodSupplementPersistence : IGeneralPersistence
    {  
        Task<FoodSupplement> GetByIdAsync(int id);
        Task<FoodSupplement> GetByRelatedIdAsync(int foodId, int supplementId);
    }
}