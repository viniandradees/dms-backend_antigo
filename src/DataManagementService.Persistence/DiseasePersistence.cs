using DataManagementService.Domain;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class DiseasePersistence : GeneralPersistence, IDiseasePersistence
    {
        private readonly DataManagementServiceContext _context;

        public DiseasePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Disease[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<Disease> query = _context.Diseases.AsNoTracking();

            if (getFullData)
            {
                query = query
                .Include(d => d.TreatmentDrugs).ThenInclude(td => td.Drug)
                .Include(d => d.TreatmentDrugs).ThenInclude(td => td.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.TreatmentDrugs).ThenInclude(td => td.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(d => d.TreatmentSupplements).ThenInclude(ts => ts.Supplement)
                .Include(d => d.TreatmentSupplements).ThenInclude(ts => ts.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.TreatmentSupplements).ThenInclude(ts => ts.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(d => d.TreatmentFoods).ThenInclude(tf => tf.Food)
                .Include(d => d.TreatmentFoods).ThenInclude(tf => tf.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.TreatmentFoods).ThenInclude(tf => tf.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(d => d.TreatmentLifestyles).ThenInclude(tl => tl.Lifestyle)
                .Include(d => d.DiagnoseExams)
                .Include(d => d.TreatmentFoods)
                .Include(d => d.DiagnoseExams).ThenInclude(de => de.Exam)
                .Include(d => d.DiagnoseSymptoms).ThenInclude(ds => ds.Symptom)
                .Include(d => d.SymptomOfDiseases).ThenInclude(sod => sod.Disease)
                .Include(d => d.SideEffectOfDrugs).ThenInclude(seod => seod.Drug);
            }

            query = query.OrderBy(d => d.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Disease> GetByIdAsync(int id)
        {
            Disease disease = await _context.Diseases.AsNoTracking()
                .Include(d => d.TreatmentDrugs).ThenInclude(td => td.Drug)
                .Include(d => d.TreatmentDrugs).ThenInclude(td => td.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.TreatmentDrugs).ThenInclude(td => td.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(d => d.TreatmentSupplements).ThenInclude(ts => ts.Supplement)
                .Include(d => d.TreatmentSupplements).ThenInclude(ts => ts.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.TreatmentSupplements).ThenInclude(ts => ts.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(d => d.TreatmentFoods).ThenInclude(tf => tf.Food)
                .Include(d => d.TreatmentFoods).ThenInclude(tf => tf.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.TreatmentFoods).ThenInclude(tf => tf.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(d => d.TreatmentLifestyles).ThenInclude(tl => tl.Lifestyle)
                .Include(d => d.DiagnoseExams)
                .Include(d => d.TreatmentFoods)
                .Include(d => d.DiagnoseExams).ThenInclude(de => de.Exam)
                .Include(d => d.DiagnoseSymptoms).ThenInclude(ds => ds.Symptom)
                .Include(d => d.SymptomOfDiseases).ThenInclude(sod => sod.Disease)
                .Include(d => d.SideEffectOfDrugs).ThenInclude(seod => seod.Drug)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            if (disease != null)
            {
                disease.TreatmentDrugs = disease.TreatmentDrugs.OrderBy(td => td.Drug.Name).ToList();
                disease.TreatmentSupplements = disease.TreatmentSupplements.OrderBy(ts => ts.Supplement.Name).ToList();
                disease.DiagnoseExams = disease.DiagnoseExams.OrderBy(te => te.Exam.Name).ToList();
                disease.TreatmentFoods = disease.TreatmentFoods.OrderBy(tf => tf.Food.Name).ToList();
                disease.TreatmentLifestyles = disease.TreatmentLifestyles.OrderBy(tl => tl.Lifestyle.Name).ToList();
                disease.DiagnoseExams = disease.DiagnoseExams.OrderBy(ds => ds.Exam.Name).ToList();
                disease.DiagnoseSymptoms = disease.DiagnoseSymptoms.OrderBy(ds => ds.Symptom.Name).ToList();
                disease.SymptomOfDiseases = disease.SymptomOfDiseases.OrderBy(sod => sod.Disease.Name).ToList();
                disease.SideEffectOfDrugs = disease.SideEffectOfDrugs.OrderBy(seod => seod.Disease.Name).ToList();
            }

            return disease;
        }

        public async Task<Disease> GetByNameAsync(string name)
        {
            Disease disease = await _context.Diseases.AsNoTracking()
                .Include(d => d.TreatmentDrugs).ThenInclude(td => td.Drug)
                .Include(d => d.TreatmentDrugs).ThenInclude(td => td.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.TreatmentDrugs).ThenInclude(td => td.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(d => d.TreatmentSupplements).ThenInclude(ts => ts.Supplement)
                .Include(d => d.TreatmentSupplements).ThenInclude(ts => ts.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.TreatmentSupplements).ThenInclude(ts => ts.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(d => d.TreatmentFoods).ThenInclude(tf => tf.Food)
                .Include(d => d.TreatmentFoods).ThenInclude(tf => tf.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.TreatmentFoods).ThenInclude(tf => tf.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(d => d.TreatmentLifestyles).ThenInclude(tl => tl.Lifestyle)
                .Include(d => d.DiagnoseExams)
                .Include(d => d.TreatmentFoods)
                .Include(d => d.DiagnoseExams).ThenInclude(de => de.Exam)
                .Include(d => d.DiagnoseSymptoms).ThenInclude(ds => ds.Symptom)
                .Include(d => d.SymptomOfDiseases).ThenInclude(sod => sod.Disease)
                .Include(d => d.SideEffectOfDrugs).ThenInclude(seod => seod.Drug)
                .Where(d => d.Name == name)
                .FirstOrDefaultAsync();

            if (disease != null)
            {
                disease.TreatmentDrugs = disease.TreatmentDrugs.OrderBy(td => td.Drug.Name).ToList();
                disease.TreatmentSupplements = disease.TreatmentSupplements.OrderBy(ts => ts.Supplement.Name).ToList();
                disease.DiagnoseExams = disease.DiagnoseExams.OrderBy(te => te.Exam.Name).ToList();
                disease.TreatmentFoods = disease.TreatmentFoods.OrderBy(tf => tf.Food.Name).ToList();
                disease.TreatmentLifestyles = disease.TreatmentLifestyles.OrderBy(tl => tl.Lifestyle.Name).ToList();
                disease.DiagnoseExams = disease.DiagnoseExams.OrderBy(ds => ds.Exam.Name).ToList();
                disease.DiagnoseSymptoms = disease.DiagnoseSymptoms.OrderBy(ds => ds.Symptom.Name).ToList();
                disease.SymptomOfDiseases = disease.SymptomOfDiseases.OrderBy(sod => sod.Disease.Name).ToList();
                disease.SideEffectOfDrugs = disease.SideEffectOfDrugs.OrderBy(seod => seod.Disease.Name).ToList();
            }

            return disease;
        }
    }
}