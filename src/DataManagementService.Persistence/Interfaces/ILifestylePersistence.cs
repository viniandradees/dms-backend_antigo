using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface ILifestylePersistence : IGeneralPersistence
    {
        Task<Lifestyle[]> GetAllAsync(bool getFullData = false);
        Task<Lifestyle> GetByIdAsync(int id);
        Task<Lifestyle> GetByNameAsync(string name);
    }
}