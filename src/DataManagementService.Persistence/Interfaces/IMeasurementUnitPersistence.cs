using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IMeasurementUnitPersistence : IGeneralPersistence
    {
        Task<MeasurementUnit[]> GetAllAsync(bool getFullData = false);
        Task<MeasurementUnit> GetByIdAsync(int id);
        Task<MeasurementUnit> GetByNameAsync(string name);
    }
}