
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class LifestyleService : ILifestyleService
    {
        private readonly ILifestylePersistence _lifestylePersistence;
        private readonly IMapper _mapper;
        public LifestyleService(ILifestylePersistence lifestylePersistence, IMapper mapper)
        {
            _lifestylePersistence = lifestylePersistence;
            _mapper = mapper;
            
        }

        public async Task<LifestyleDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var lifestyles = await _lifestylePersistence.GetAllAsync(getFullData);
                if (lifestyles is null) return null;

                return _mapper.Map<LifestyleDto[]>(lifestyles);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-L01", 500, $"Unexpected error getting lifestyles: {ex.Message}");
            }
        }

        public async Task<LifestyleDto> GetByIdAsync(int id)
        {
            try
            {
                var lifestyles = await _lifestylePersistence.GetByIdAsync(id);
                if (lifestyles is null) return null;

                return _mapper.Map<LifestyleDto>(lifestyles);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-L02", 500, $"Unexpected error getting lifestyle: {ex.Message}");
            }
        }

        public async Task<LifestyleDto> GetByNameAsync(string name)
        {
            try
            {
                var lifestyles = await _lifestylePersistence.GetByNameAsync(name);
                if (lifestyles is null) return null;

                return _mapper.Map<LifestyleDto>(lifestyles);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-L03", 500, $"Unexpected error getting lifestyle: {ex.Message}");
            }
        }
        public async Task<LifestyleDto> Add(LifestyleDto model)
        {
            try
            {
                var lifestyle = _mapper.Map<Lifestyle>(model);
                _lifestylePersistence.Add<Lifestyle>(lifestyle);

                var checkName = await _lifestylePersistence.GetByNameAsync(lifestyle.Name);
                if (checkName is not null) throw new DmsException("AP-L04", 409, "Lifestyle name already exists.");
                
                var save = await _lifestylePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-L05", 500, "Unexpected error adding this lifestyle from the database.");

                var lifestyleDto = await _lifestylePersistence.GetByIdAsync(lifestyle.Id);
                if (lifestyleDto is null) throw new DmsException("AP-L06", 500, "Unexpected error adding lifestyle.");

                return _mapper.Map<LifestyleDto>(lifestyleDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-L07", 500, $"Unexpected error adding lifestyle: {ex.Message}");
            }
            
        }

        public async Task<LifestyleDto> Update(int id, LifestyleDto model)
        {
            try
            {
                var checkLifestyle = await _lifestylePersistence.GetByIdAsync(id);
                if (checkLifestyle is null) throw new DmsException("AP-L08", 404, "This lifestyle does not exist.");

                model.Id = id;

                if (checkLifestyle.Name != model.Name) {
                    var checkName = await _lifestylePersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-L09", 409, "Lifestyle name already exists.");
                }

                var lifestyle = _mapper.Map<Lifestyle>(model);
                _lifestylePersistence.Update<Lifestyle>(lifestyle);

                var save = await _lifestylePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-L10", 500, "Unexpected error updating this lifestyle from the database.");
                
                var result = await _lifestylePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-L11", 500, "Unexpected error updating lifestyle.");
                return _mapper.Map<LifestyleDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-L12", 500, $"Unexpected error updating lifestyle: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var lifestyle = await _lifestylePersistence.GetByIdAsync(id);
                if (lifestyle is null) throw new DmsException("AP-L13", 404, "This lifestyle does not exist.");

                _lifestylePersistence.Delete<Lifestyle>(lifestyle);
                return await _lifestylePersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-L14", 500, $"Unexpected error deleting lifestyle: {ex.Message}");
            }
        }
    }
}