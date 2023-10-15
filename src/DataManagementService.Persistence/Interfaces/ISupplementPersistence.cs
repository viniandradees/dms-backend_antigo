using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface ISupplementPersistence : IGeneralPersistence
    {
        Task<Supplement[]> GetAllAsync(bool getFullData = false);
        Task<Supplement> GetByIdAsync(int id);
        Task<Supplement> GetByNameAsync(string name);
    }
}