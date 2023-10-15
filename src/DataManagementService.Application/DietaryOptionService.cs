
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DietaryOptionService : IDietaryOptionService
    {
        private readonly IDietaryOptionPersistence _dietaryOptionPersistence;
        private readonly IMapper _mapper;
        public DietaryOptionService(IDietaryOptionPersistence dietaryOptionPersistence, IMapper mapper)
        {
            _dietaryOptionPersistence = dietaryOptionPersistence;
            _mapper = mapper;
            
        }

        public async Task<DietaryOptionDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var dietaryOptions = await _dietaryOptionPersistence.GetAllAsync(getFullData);
                if (dietaryOptions is null) return null;

                return _mapper.Map<DietaryOptionDto[]>(dietaryOptions);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI01", 500, $"Unexpected error getting dietary options: {ex.Message}");
            }
        }

        public async Task<DietaryOptionDto> GetByIdAsync(int id)
        {
            try
            {
                var dietaryOptions = await _dietaryOptionPersistence.GetByIdAsync(id);
                if (dietaryOptions is null) return null;

                return _mapper.Map<DietaryOptionDto>(dietaryOptions);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI02", 500, $"Unexpected error getting dietary option: {ex.Message}");
            }
        }

        public async Task<DietaryOptionDto> GetByNameAsync(string name)
        {
            try
            {
                var dietaryOptions = await _dietaryOptionPersistence.GetByNameAsync(name);
                if (dietaryOptions is null) return null;

                return _mapper.Map<DietaryOptionDto>(dietaryOptions);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI03", 500, $"Unexpected error getting dietary option: {ex.Message}");
            }
        }
        public async Task<DietaryOptionDto> Add(DietaryOptionDto model)
        {
            try
            {
                var dietaryOption = _mapper.Map<DietaryOption>(model);
                _dietaryOptionPersistence.Add<DietaryOption>(dietaryOption);

                var checkName = await _dietaryOptionPersistence.GetByNameAsync(dietaryOption.Name);
                if (checkName is not null) throw new DmsException("AP-DI04", 409, "Dietary option name already exists.");
                
                var save = await _dietaryOptionPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DI05", 500, "Unexpected error adding this dietary option from the database.");

                var dietaryOptionDto = await _dietaryOptionPersistence.GetByIdAsync(dietaryOption.Id);
                if (dietaryOptionDto is null) throw new DmsException("AP-DI06", 500, "Unexpected error adding dietary option.");

                return _mapper.Map<DietaryOptionDto>(dietaryOptionDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI07", 500, $"Unexpected error adding dietary option: {ex.Message}");
            }
            
        }

        public async Task<DietaryOptionDto> Update(int id, DietaryOptionDto model)
        {
            try
            {
                var checkDietaryOption = await _dietaryOptionPersistence.GetByIdAsync(id);
                if (checkDietaryOption is null) throw new DmsException("AP-DI08", 404, "This dietary option does not exist.");

                model.Id = id;

                if (checkDietaryOption.Name != model.Name) {
                    var checkName = await _dietaryOptionPersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-DI09", 409, "Dietary option name already exists.");
                }

                var dietaryOption = _mapper.Map<DietaryOption>(model);
                _dietaryOptionPersistence.Update<DietaryOption>(dietaryOption);

                var save = await _dietaryOptionPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DI10", 500, "Unexpected error updating this dietary option from the database.");
                
                var result = await _dietaryOptionPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DI11", 500, "Unexpected error updating dietary option.");
                return _mapper.Map<DietaryOptionDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI12", 500, $"Unexpected error updating dietary option: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var dietaryOption = await _dietaryOptionPersistence.GetByIdAsync(id);
                if (dietaryOption is null) throw new DmsException("AP-DI13", 404, "This dietary option does not exist.");

                _dietaryOptionPersistence.Delete<DietaryOption>(dietaryOption);
                return await _dietaryOptionPersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DI14", 500, $"Unexpected error deleting dietary option: {ex.Message}");
            }
        }
    }
}