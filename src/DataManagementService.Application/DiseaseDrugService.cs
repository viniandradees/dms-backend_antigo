
using AutoMapper;
using DataManagementService.Application.Dtos;
using DataManagementService.Application.Helpers;
using DataManagementService.Application.Interfaces;
using DataManagementService.Domain;
using DataManagementService.Persistence.Interfaces;

namespace DataManagementService.Application
{
    public class DiseaseDrugService : IDiseaseDrugService
    {
        private readonly IDiseaseDrugPersistence _diseaseDrugPersistence;
        private readonly IDiseasePersistence _diseasePersistence;
        private readonly IDrugPersistence _drugPersistence;
        private readonly IMapper _mapper;
        public DiseaseDrugService(IDiseaseDrugPersistence diseaseDrugPersistence, IDiseasePersistence diseasePersistence, IDrugPersistence drugPersistence, IMapper mapper)
        {
            _diseaseDrugPersistence = diseaseDrugPersistence;
            _diseasePersistence = diseasePersistence;
            _drugPersistence = drugPersistence;
            _mapper = mapper;
            
        }
        public async Task<DiseaseDrugDto> Add(DiseaseDrugDto model)
        {
            try
            {
                var disease = await _diseasePersistence.GetByIdAsync(model.DiseaseId);
                var drug = await _drugPersistence.GetByIdAsync(model.DrugId);
                if (disease is null || drug is null) throw new DmsException("AP-DD01", 400, "Related disease/drug does not exist.");

                var diseaseDrug = await _diseaseDrugPersistence.GetByRelatedIdAsync(model.DiseaseId, model.DrugId);
                if (diseaseDrug is not null) throw new DmsException("AP-DD02", 409, "This treatment drug already exists.");

                var toAddDiseaseDrug = _mapper.Map<DiseaseDrug>(model);
                _diseaseDrugPersistence.Add<DiseaseDrug>(toAddDiseaseDrug);

                var save = await _diseaseDrugPersistence.SaveChangesAsync();
                if (!save) throw new DmsException("AP-DD03", 500, "Unexpected error saving this treatment drug from the database.");

                var newDiseaseDrug = await _diseaseDrugPersistence.GetByRelatedIdAsync(model.DiseaseId, model.DrugId);
                if (newDiseaseDrug is null) throw new DmsException("AP-DD04", 500, "Unexpected error adding treatment drug.");
                
                return _mapper.Map<DiseaseDrugDto>(newDiseaseDrug);
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DD05", 500, $"Unexpected error adding treatment drug: {ex.Message}");
            }
            
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var diseaseDrug = await _diseaseDrugPersistence.GetByIdAsync(id);
                if (diseaseDrug is null) throw new DmsException("AP-DD06", 404, "This treatment drug does not exist.");

                _diseaseDrugPersistence.Delete<DiseaseDrug>(diseaseDrug);
                var save = await _diseaseDrugPersistence.SaveChangesAsync();

                if (!save) throw new DmsException("AP-DD07", 500, "Unexpected error deleting this treatment drug from the database.");
                return true;
            }
            catch (DmsException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new DmsException("AP-DD08", 500, $"Unexpected error deleting treatment drug: {ex.Message}");
            }
        }
    }
}