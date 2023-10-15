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
    public class ExamResultReferencePersistence : GeneralPersistence, IExamResultReferencePersistence
    {
        private readonly DataManagementServiceContext _context;

        public ExamResultReferencePersistence(DataManagementServiceContext context) : base(context) => _context = context;

        public async Task<ExamResultReference> GetByIdAsync(int id)
        {
            IQueryable<ExamResultReference> query = _context.ExamResultReferences.AsNoTracking()
                .Include(err => err.MeasurementUnit);

            query = query.Where(err => err.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ExamResultReference> GetByRelatedIdAsync(int examId, int measurementUnitId) 
        {
            IQueryable<ExamResultReference> query = _context.ExamResultReferences.AsNoTracking()
                .Include(dd => dd.Exam)
                .Include(dd => dd.MeasurementUnit);

            query = query.Where(dd => dd.ExamId == examId && dd.MeasurementUnitId == measurementUnitId);

            return await query.FirstOrDefaultAsync();
        }
    }
}