using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IFoodPersistence : IGeneralPersistence
    {
        Task<Food[]> GetAllAsync(bool getFullData = false);
        Task<Food> GetByIdAsync(int id);
        Task<Food> GetByNameAsync(string name);
    }
}