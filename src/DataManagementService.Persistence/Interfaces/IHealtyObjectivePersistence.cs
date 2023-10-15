using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IHealtyObjectivePersistence : IGeneralPersistence
    {
        Task<HealtyObjective[]> GetAllAsync(bool getFullData = false);
        Task<HealtyObjective> GetByIdAsync(int id);
        Task<HealtyObjective> GetByNameAsync(string name);
    }
}