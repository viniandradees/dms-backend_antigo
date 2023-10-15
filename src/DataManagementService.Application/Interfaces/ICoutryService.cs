using DataManagementService.Application.Dtos;

namespace DataManagementService.Application.Interfaces
{
    public interface ICountryService
    {
        Task<CountryDto> Add(CountryDto model);
        Task<CountryDto> Update(int id, CountryDto model);
        Task<bool> Delete(int id);

        Task<CountryDto[]> GetAllAsync();
        Task<CountryDto> GetByIdAsync(int id);
        Task<CountryDto> GetByNameAsync(string name);
    }
}