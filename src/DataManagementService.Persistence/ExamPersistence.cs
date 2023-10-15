using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class ExamPersistence : GeneralPersistence, IExamPersistence
    {
        private readonly DataManagementServiceContext _context;

        public ExamPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Exam[]> GetAllAsync(bool getFullData = false)
        {
            IQueryable<Exam> query = _context.Exams.AsNoTracking();

            if (getFullData)
            {
                query = query
                .Include(e => e.ExamResultReferences).ThenInclude(err => err.MeasurementUnit)
                .Include(e => e.ExamResultReferences).ThenInclude(err => err.Variations)
                .Include(e => e.ExamResultReferences).ThenInclude(err => err.Countries).ThenInclude(c => c.Country)
                .Include(e => e.RelatedDiseases)
                .Include(e => e.RelatedSupplements).ThenInclude(rs => rs.Supplement)
                .Include(d => d.RelatedSupplements).ThenInclude(rs => rs.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.RelatedSupplements).ThenInclude(rs => rs.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(e => e.RelatedFoods).ThenInclude(rf => rf.Food)
                .Include(e => e.RelatedLifestyles);
            }

            query = query.OrderBy(e => e.Name);

            return await query.ToArrayAsync();
        }

        public async Task<Exam> GetByIdAsync(int id)
        {
            Exam exam = await _context.Exams.AsNoTracking()
                .Include(e => e.ExamResultReferences).ThenInclude(err => err.MeasurementUnit)
                .Include(e => e.ExamResultReferences).ThenInclude(err => err.Variations)
                .Include(e => e.ExamResultReferences).ThenInclude(err => err.Countries).ThenInclude(c => c.Country)
                .Include(e => e.RelatedDiseases)
                .Include(e => e.RelatedSupplements).ThenInclude(rs => rs.Supplement)
                .Include(d => d.RelatedSupplements).ThenInclude(rs => rs.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.RelatedSupplements).ThenInclude(rs => rs.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(e => e.RelatedFoods).ThenInclude(rf => rf.Food)
                .Include(e => e.RelatedLifestyles)
                .Where(d => d.Id == id)
                .FirstOrDefaultAsync();

            if (exam != null)
            {
                exam.ExamResultReferences = exam.ExamResultReferences.OrderBy(err => err.MeasurementUnit.Name).ToList();
                exam.RelatedSupplements = exam.RelatedSupplements.OrderBy(rs => rs.Supplement.Name).ToList();
                exam.RelatedFoods = exam.RelatedFoods.OrderBy(rd => rd.Food.Name).ToList();
                exam.RelatedLifestyles = exam.RelatedLifestyles.OrderBy(rd => rd.Lifestyle.Name).ToList();
            }

            return exam;
        }
        public async Task<Exam> GetByNameAsync(string name)
        {
            Exam exam = await _context.Exams.AsNoTracking()
                .Include(e => e.ExamResultReferences).ThenInclude(err => err.MeasurementUnit)
                .Include(e => e.ExamResultReferences).ThenInclude(err => err.Variations)
                .Include(e => e.ExamResultReferences).ThenInclude(err => err.Countries).ThenInclude(c => c.Country)
                .Include(e => e.RelatedDiseases)
                .Include(e => e.RelatedSupplements).ThenInclude(rs => rs.Supplement)
                .Include(d => d.RelatedSupplements).ThenInclude(rs => rs.Dosages).ThenInclude(dos => dos.MeasurementUnit)
                .Include(d => d.RelatedSupplements).ThenInclude(rs => rs.Dosages).ThenInclude(dos => dos.AgeRanges)
                .Include(e => e.RelatedFoods).ThenInclude(rf => rf.Food)
                .Include(e => e.RelatedLifestyles)
                .Where(d => d.Name == name)
                .FirstOrDefaultAsync();

            if (exam != null)
            {
                exam.ExamResultReferences = exam.ExamResultReferences.OrderBy(err => err.MeasurementUnit.Name).ToList();
                exam.RelatedSupplements = exam.RelatedSupplements.OrderBy(rs => rs.Supplement.Name).ToList();
                exam.RelatedFoods = exam.RelatedFoods.OrderBy(rd => rd.Food.Name).ToList();
                exam.RelatedLifestyles = exam.RelatedLifestyles.OrderBy(rd => rd.Lifestyle.Name).ToList();
            }

            return exam;
        }
    }
}