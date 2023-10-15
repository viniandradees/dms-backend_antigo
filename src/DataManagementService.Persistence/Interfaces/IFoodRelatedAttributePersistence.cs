using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IFoodRelatedAttributePersistence : IGeneralPersistence
    {  
        Task<FoodRelatedAttribute> GetByIdAsync(int id);
        Task<FoodRelatedAttribute> GetByRelatedIdAsync(int foodId, int foodAttributeId);
    }
}