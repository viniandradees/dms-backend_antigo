
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class CountryService : ICountryService
    {
        private readonly IGeneralPersistence _generalPersistence;
        private readonly ICountryPersistence _countryPersistence;
        private readonly IMapper _mapper;
        public CountryService(IGeneralPersistence generalPersistence, ICountryPersistence countryPersistence, IMapper mapper)
        {
            _generalPersistence = generalPersistence;
            _countryPersistence = countryPersistence;
            _mapper = mapper;
            
        }
        public async Task<CountryDto> Add(CountryDto model)
        {
            try
            {
                var country = _mapper.Map<Country>(model);
                _generalPersistence.Add<Country>(country);

                if (await _generalPersistence.SaveChangesAsync()) {
                    var countryDto = await _countryPersistence.GetByIdAsync(country.Id);
                    return _mapper.Map<CountryDto>(countryDto);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<CountryDto> Update(int id, CountryDto model)
        {
            try
            {
                var check_country = await _countryPersistence.GetByIdAsync(id);
                if (check_country is null) return null;

                var country = _mapper.Map<Country>(model);
                _generalPersistence.Update<Country>(country);

                if (await _generalPersistence.SaveChangesAsync()) {
                    var result = await _countryPersistence.GetByIdAsync(id);
                    return _mapper.Map<CountryDto>(result);
                }
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var country = await _countryPersistence.GetByIdAsync(id);
                if (country is null) throw new Exception("Country not exists.");

                _generalPersistence.Delete<Country>(country);
                return await _generalPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CountryDto[]> GetAllAsync()
        {
            try
            {
                var countrys = await _countryPersistence.GetAllAsync();
                if (countrys is null) return null;

                return _mapper.Map<CountryDto[]>(countrys);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CountryDto> GetByIdAsync(int id)
        {
            try
            {
                var countrys = await _countryPersistence.GetByIdAsync(id);
                if (countrys is null) return null;

                return _mapper.Map<CountryDto>(countrys);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CountryDto> GetByNameAsync(string name)
        {
            try
            {
                var countrys = await _countryPersistence.GetByNameAsync(name);
                if (countrys is null) return null;

                return _mapper.Map<CountryDto>(countrys);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}