using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IFoodAttributePersistence : IGeneralPersistence
    {
        Task<FoodAttribute[]> GetAllAsync(bool getFullData = false);
        Task<FoodAttribute> GetByIdAsync(int id);
        Task<FoodAttribute> GetByNameAsync(string name);
    }
}