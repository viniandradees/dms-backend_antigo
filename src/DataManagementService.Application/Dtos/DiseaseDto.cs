using DataManagementService.Domain;

namespace DataManagementService.Application.Dtos
{
    public class DiseaseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MostIndicatedTreatment { get; set; }

        public IEnumerable<DiseaseDrug> TreatmentDrugs { get; set; }
        public IEnumerable<DiseaseSupplement> TreatmentSupplements { get; set; }
        public IEnumerable<DiseaseFood> TreatmentFoods { get; set; }
        public IEnumerable<DiseaseLifestyle> TreatmentLifestyles { get; set; }
        public IEnumerable<DiseaseExam> DiagnoseExams { get; set; }
        public IEnumerable<DiseaseDisease> DiagnoseSymptoms { get; set; }
        public IEnumerable<DiseaseDisease> SymptomOfDiseases { get; set; }
        public IEnumerable<DrugDisease> SideEffectOfDrugs { get; set; }
        public IEnumerable<SupplementDisease> SideEffectOfSupplements { get; set; }
        public IEnumerable<FoodDisease> SideEffectOfFoods { get; set; }
        public IEnumerable<LifestyleDisease> SideEffectOfLifestyles { get; set; }
    }
}