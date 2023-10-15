using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDietaryOptionFoodAttributePersistence : IGeneralPersistence
    {  
        Task<DietaryOptionFoodAttribute> GetByIdAsync(int id);
        Task<DietaryOptionFoodAttribute> GetByRelatedIdAsync(int dietaryOptionId, int foodAttributeId);
    }
}