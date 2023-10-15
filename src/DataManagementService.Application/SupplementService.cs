
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class SupplementService : ISupplementService
    {
        private readonly ISupplementPersistence _supplementPersistence;
        private readonly IMapper _mapper;
        public SupplementService(ISupplementPersistence supplementPersistence, IMapper mapper)
        {
            _supplementPersistence = supplementPersistence;
            _mapper = mapper;
            
        }

        public async Task<SupplementDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var supplements = await _supplementPersistence.GetAllAsync(getFullData);
                if (supplements is null) return null;

                return _mapper.Map<SupplementDto[]>(supplements);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-S01", 500, $"Unexpected error getting supplements: {ex.Message}");
            }
        }

        public async Task<SupplementDto> GetByIdAsync(int id)
        {
            try
            {
                var supplements = await _supplementPersistence.GetByIdAsync(id);
                if (supplements is null) return null;

                return _mapper.Map<SupplementDto>(supplements);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-S02", 500, $"Unexpected error getting supplement: {ex.Message}");
            }
        }

        public async Task<SupplementDto> GetByNameAsync(string name)
        {
            try
            {
                var supplements = await _supplementPersistence.GetByNameAsync(name);
                if (supplements is null) return null;

                return _mapper.Map<SupplementDto>(supplements);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-S03", 500, $"Unexpected error getting supplement: {ex.Message}");
            }
        }
        public async Task<SupplementDto> Add(SupplementDto model)
        {
            try
            {
                var supplement = _mapper.Map<Supplement>(model);
                _supplementPersistence.Add<Supplement>(supplement);

                var checkName = await _supplementPersistence.GetByNameAsync(supplement.Name);
                if (checkName is not null) throw new DmsException("AP-S04", 409, "Supplement name already exists.");
                
                var save = await _supplementPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-S05", 500, "Unexpected error adding this supplement from the database.");

                var supplementDto = await _supplementPersistence.GetByIdAsync(supplement.Id);
                if (supplementDto is null) throw new DmsException("AP-S06", 500, "Unexpected error adding supplement.");

                return _mapper.Map<SupplementDto>(supplementDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-S07", 500, $"Unexpected error adding supplement: {ex.Message}");
            }
            
        }

        public async Task<SupplementDto> Update(int id, SupplementDto model)
        {
            try
            {
                var checkSupplement = await _supplementPersistence.GetByIdAsync(id);
                if (checkSupplement is null) throw new DmsException("AP-S08", 404, "This supplement does not exist.");

                model.Id = id;

                if (checkSupplement.Name != model.Name) {
                    var checkName = await _supplementPersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-S09", 409, "Supplement name already exists.");
                }

                var supplement = _mapper.Map<Supplement>(model);
                _supplementPersistence.Update<Supplement>(supplement);

                var save = await _supplementPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-S10", 500, "Unexpected error updating this supplement from the database.");
                
                var result = await _supplementPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-S11", 500, "Unexpected error updating supplement.");
                return _mapper.Map<SupplementDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-S12", 500, $"Unexpected error updating supplement: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var supplement = await _supplementPersistence.GetByIdAsync(id);
                if (supplement is null) throw new DmsException("AP-S13", 404, "This supplement does not exist.");

                _supplementPersistence.Delete<Supplement>(supplement);
                return await _supplementPersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-S14", 500, $"Unexpected error deleting supplement: {ex.Message}");
            }
        }
    }
}