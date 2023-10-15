using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDiseasePersistence : IGeneralPersistence
    {
        Task<Disease[]> GetAllAsync(bool getFullData = false);
        Task<Disease> GetByIdAsync(int id);
        Task<Disease> GetByNameAsync(string name);
    }
}