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
    public class ExamSupplementPersistence : GeneralPersistence, IExamSupplementPersistence
    {
        private readonly DataManagementServiceContext _context;

        public ExamSupplementPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ExamSupplement> GetByIdAsync(int id)
        {
            IQueryable<ExamSupplement> query = _context.ExamSupplements.AsNoTracking()
                .Include(dd => dd.Exam)
                .Include(dd => dd.Supplement);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ExamSupplement> GetByRelatedIdAsync(int examId, int supplementId) 
        {
            IQueryable<ExamSupplement> query = _context.ExamSupplements.AsNoTracking()
                .Include(dd => dd.Exam)
                .Include(dd => dd.Supplement);

            query = query.Where(dd => dd.ExamId == examId && dd.SupplementId == supplementId);

            return await query.FirstOrDefaultAsync();
        }
    }
}