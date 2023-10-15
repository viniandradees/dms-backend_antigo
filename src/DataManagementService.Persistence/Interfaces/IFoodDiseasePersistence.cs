using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IFoodDiseasePersistence : IGeneralPersistence
    {  
        Task<FoodDisease> GetByIdAsync(int id);
        Task<FoodDisease> GetByRelatedIdAsync(int foodId, int diseaseId);
    }
}