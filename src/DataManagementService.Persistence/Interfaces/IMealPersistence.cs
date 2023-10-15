using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IMealPersistence : IGeneralPersistence
    {
        Task<Meal[]> GetAllAsync(bool getFullData = false);
        Task<Meal> GetByIdAsync(int id);
        Task<Meal> GetByNameAsync(string name);
    }
}