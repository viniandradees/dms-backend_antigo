
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class HealtyObjectiveService : IHealtyObjectiveService
    {
        private readonly IHealtyObjectivePersistence _healtyObjectivePersistence;
        private readonly IMapper _mapper;
        public HealtyObjectiveService(IHealtyObjectivePersistence healtyObjectivePersistence, IMapper mapper)
        {
            _healtyObjectivePersistence = healtyObjectivePersistence;
            _mapper = mapper;
            
        }

        public async Task<HealtyObjectiveDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var healtyObjectives = await _healtyObjectivePersistence.GetAllAsync(getFullData);
                if (healtyObjectives is null) return null;

                return _mapper.Map<HealtyObjectiveDto[]>(healtyObjectives);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-HO01", 500, $"Unexpected error getting healty objectives: {ex.Message}");
            }
        }

        public async Task<HealtyObjectiveDto> GetByIdAsync(int id)
        {
            try
            {
                var healtyObjectives = await _healtyObjectivePersistence.GetByIdAsync(id);
                if (healtyObjectives is null) return null;

                return _mapper.Map<HealtyObjectiveDto>(healtyObjectives);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-HO02", 500, $"Unexpected error getting healty objective: {ex.Message}");
            }
        }

        public async Task<HealtyObjectiveDto> GetByNameAsync(string name)
        {
            try
            {
                var healtyObjectives = await _healtyObjectivePersistence.GetByNameAsync(name);
                if (healtyObjectives is null) return null;

                return _mapper.Map<HealtyObjectiveDto>(healtyObjectives);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-HO03", 500, $"Unexpected error getting healty objective: {ex.Message}");
            }
        }
        public async Task<HealtyObjectiveDto> Add(HealtyObjectiveDto model)
        {
            try
            {
                var healtyObjective = _mapper.Map<HealtyObjective>(model);
                _healtyObjectivePersistence.Add<HealtyObjective>(healtyObjective);

                var checkName = await _healtyObjectivePersistence.GetByNameAsync(healtyObjective.Name);
                if (checkName is not null) throw new DmsException("AP-HO04", 409, "Healty objective name already exists.");
                
                var save = await _healtyObjectivePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-HO05", 500, "Unexpected error adding this healty objective from the database.");

                var healtyObjectiveDto = await _healtyObjectivePersistence.GetByIdAsync(healtyObjective.Id);
                if (healtyObjectiveDto is null) throw new DmsException("AP-HO06", 500, "Unexpected error adding healty objective.");

                return _mapper.Map<HealtyObjectiveDto>(healtyObjectiveDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-HO07", 500, $"Unexpected error adding healty objective: {ex.Message}");
            }
            
        }

        public async Task<HealtyObjectiveDto> Update(int id, HealtyObjectiveDto model)
        {
            try
            {
                var checkHealtyObjective = await _healtyObjectivePersistence.GetByIdAsync(id);
                if (checkHealtyObjective is null) throw new DmsException("AP-HO08", 404, "This healty objective does not exist.");

                model.Id = id;

                if (checkHealtyObjective.Name != model.Name) {
                    var checkName = await _healtyObjectivePersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-HO09", 409, "Healty objective name already exists.");
                }

                var healtyObjective = _mapper.Map<HealtyObjective>(model);
                _healtyObjectivePersistence.Update<HealtyObjective>(healtyObjective);

                var save = await _healtyObjectivePersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-HO10", 500, "Unexpected error updating this healty objective from the database.");
                
                var result = await _healtyObjectivePersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-HO11", 500, "Unexpected error updating healty objective.");
                return _mapper.Map<HealtyObjectiveDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-HO12", 500, $"Unexpected error updating healty objective: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var healtyObjective = await _healtyObjectivePersistence.GetByIdAsync(id);
                if (healtyObjective is null) throw new DmsException("AP-HO13", 404, "This healty objective does not exist.");

                _healtyObjectivePersistence.Delete<HealtyObjective>(healtyObjective);
                return await _healtyObjectivePersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-HO14", 500, $"Unexpected error deleting healty objective: {ex.Message}");
            }
        }
    }
}