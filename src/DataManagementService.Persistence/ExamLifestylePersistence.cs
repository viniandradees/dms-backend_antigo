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
    public class ExamLifestylePersistence : GeneralPersistence, IExamLifestylePersistence
    {
        private readonly DataManagementServiceContext _context;

        public ExamLifestylePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ExamLifestyle> GetByIdAsync(int id)
        {
            IQueryable<ExamLifestyle> query = _context.ExamLifestyles.AsNoTracking()
                .Include(dd => dd.Exam)
                .Include(dd => dd.Lifestyle);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ExamLifestyle> GetByRelatedIdAsync(int examId, int lifestyleId) 
        {
            IQueryable<ExamLifestyle> query = _context.ExamLifestyles.AsNoTracking()
                .Include(dd => dd.Exam)
                .Include(dd => dd.Lifestyle);

            query = query.Where(dd => dd.ExamId == examId && dd.LifestyleId == lifestyleId);

            return await query.FirstOrDefaultAsync();
        }
    }
}