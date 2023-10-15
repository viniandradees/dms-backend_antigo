using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IDietaryOptionPersistence : IGeneralPersistence
    {
        Task<DietaryOption[]> GetAllAsync(bool getFullData = false);
        Task<DietaryOption> GetByIdAsync(int id);
        Task<DietaryOption> GetByNameAsync(string name);
    }
}