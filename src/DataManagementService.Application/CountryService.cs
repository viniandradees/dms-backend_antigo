
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class CountryService : ICountryService
    {
        private readonly ICountryPersistence _countryPersistence;
        private readonly IMapper _mapper;
        public CountryService(ICountryPersistence countryPersistence, IMapper mapper)
        {
            _countryPersistence = countryPersistence;
            _mapper = mapper;
            
        }

        public async Task<CountryDto[]> GetAllAsync()
        {
            try
            {
                var countries = await _countryPersistence.GetAllAsync();
                if (countries is null) return null;

                return _mapper.Map<CountryDto[]>(countries);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-CO01", 500, $"Unexpected error getting countries: {ex.Message}");
            }
        }

        public async Task<CountryDto> GetByIdAsync(int id)
        {
            try
            {
                var countries = await _countryPersistence.GetByIdAsync(id);
                if (countries is null) return null;

                return _mapper.Map<CountryDto>(countries);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-CO02", 500, $"Unexpected error getting country: {ex.Message}");
            }
        }

        public async Task<CountryDto> GetByNameAsync(string name)
        {
            try
            {
                var countries = await _countryPersistence.GetByNameAsync(name);
                if (countries is null) return null;

                return _mapper.Map<CountryDto>(countries);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-CO03", 500, $"Unexpected error getting country: {ex.Message}");
            }
        }
        public async Task<CountryDto> Add(CountryDto model)
        {
            try
            {
                var country = _mapper.Map<Country>(model);
                _countryPersistence.Add<Country>(country);

                var checkName = await _countryPersistence.GetByNameAsync(country.Name);
                if (checkName is not null) throw new DmsException("AP-CO04", 409, "Country name already exists.");
                
                var save = await _countryPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-CO05", 500, "Unexpected error adding this country from the database.");

                var countryDto = await _countryPersistence.GetByIdAsync(country.Id);
                if (countryDto is null) throw new DmsException("AP-CO06", 500, "Unexpected error adding country.");

                return _mapper.Map<CountryDto>(countryDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-CO07", 500, $"Unexpected error adding country: {ex.Message}");
            }
            
        }

        public async Task<CountryDto> Update(int id, CountryDto model)
        {
            try
            {
                var checkCountry = await _countryPersistence.GetByIdAsync(id);
                if (checkCountry is null) throw new DmsException("AP-CO08", 404, "This country does not exist.");

                model.Id = id;

                if (checkCountry.Name != model.Name) {
                    var checkName = await _countryPersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-CO09", 409, "Country name already exists.");
                }

                var country = _mapper.Map<Country>(model);
                _countryPersistence.Update<Country>(country);

                var save = await _countryPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-CO10", 500, "Unexpected error updating this country from the database.");
                
                var result = await _countryPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-CO11", 500, "Unexpected error updating country.");
                return _mapper.Map<CountryDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-CO12", 500, $"Unexpected error updating country: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var country = await _countryPersistence.GetByIdAsync(id);
                if (country is null) throw new DmsException("AP-CO13", 404, "This country does not exist.");

                _countryPersistence.Delete<Country>(country);
                return await _countryPersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-CO14", 500, $"Unexpected error deleting country: {ex.Message}");
            }
        }
    }
}