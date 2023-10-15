using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface ICountryPersistence : IGeneralPersistence
    {
        Task<Country[]> GetAllAsync();
        Task<Country> GetByIdAsync(int id);
        Task<Country> GetByNameAsync(string name);
    }
}