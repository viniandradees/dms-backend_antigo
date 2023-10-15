
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DrugService : IDrugService
    {
        private readonly IDrugPersistence _drugPersistence;
        private readonly IMapper _mapper;
        public DrugService(IDrugPersistence drugPersistence, IMapper mapper)
        {
            _drugPersistence = drugPersistence;
            _mapper = mapper;
            
        }

        public async Task<DrugDto[]> GetAllAsync(bool getFullData = false)
        {
            try
            {
                var drugs = await _drugPersistence.GetAllAsync(getFullData);
                if (drugs is null) return null;

                return _mapper.Map<DrugDto[]>(drugs);
                
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DR01", 500, $"Unexpected error getting drugs: {ex.Message}");
            }
        }

        public async Task<DrugDto> GetByIdAsync(int id)
        {
            try
            {
                var drugs = await _drugPersistence.GetByIdAsync(id);
                if (drugs is null) return null;

                return _mapper.Map<DrugDto>(drugs);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DR02", 500, $"Unexpected error getting drug: {ex.Message}");
            }
        }

        public async Task<DrugDto> GetByNameAsync(string name)
        {
            try
            {
                var drugs = await _drugPersistence.GetByNameAsync(name);
                if (drugs is null) return null;

                return _mapper.Map<DrugDto>(drugs);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DR03", 500, $"Unexpected error getting drug: {ex.Message}");
            }
        }
        public async Task<DrugDto> Add(DrugDto model)
        {
            try
            {
                var drug = _mapper.Map<Drug>(model);
                _drugPersistence.Add<Drug>(drug);

                var checkName = await _drugPersistence.GetByNameAsync(drug.Name);
                if (checkName is not null) throw new DmsException("AP-DR04", 409, "Drug name already exists.");
                
                var save = await _drugPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DR05", 500, "Unexpected error adding this drug from the database.");

                var drugDto = await _drugPersistence.GetByIdAsync(drug.Id);
                if (drugDto is null) throw new DmsException("AP-DR06", 500, "Unexpected error adding drug.");

                return _mapper.Map<DrugDto>(drugDto);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DR07", 500, $"Unexpected error adding drug: {ex.Message}");
            }
            
        }

        public async Task<DrugDto> Update(int id, DrugDto model)
        {
            try
            {
                var checkDrug = await _drugPersistence.GetByIdAsync(id);
                if (checkDrug is null) throw new DmsException("AP-DR08", 404, "This drug does not exist.");

                model.Id = id;

                if (checkDrug.Name != model.Name) {
                    var checkName = await _drugPersistence.GetByNameAsync(model.Name);
                    if (checkName is not null) throw new DmsException("AP-DR09", 409, "Drug name already exists.");
                }

                var drug = _mapper.Map<Drug>(model);
                _drugPersistence.Update<Drug>(drug);

                var save = await _drugPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DR10", 500, "Unexpected error updating this drug from the database.");
                
                var result = await _drugPersistence.GetByIdAsync(id);
                if (result is null) throw new DmsException("AP-DR11", 500, "Unexpected error updating drug.");
                return _mapper.Map<DrugDto>(result);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DR12", 500, $"Unexpected error updating drug: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var drug = await _drugPersistence.GetByIdAsync(id);
                if (drug is null) throw new DmsException("AP-DR13", 404, "This drug does not exist.");

                _drugPersistence.Delete<Drug>(drug);
                return await _drugPersistence.SaveChangesAsync();
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DR14", 500, $"Unexpected error deleting drug: {ex.Message}");
            }
        }
    }
}