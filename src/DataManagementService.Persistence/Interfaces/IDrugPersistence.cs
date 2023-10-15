using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDrugPersistence : IGeneralPersistence
    {
        Task<Drug[]> GetAllAsync(bool getFullData = false);
        Task<Drug> GetByIdAsync(int id);
        Task<Drug> GetByNameAsync(string name);
    }
}