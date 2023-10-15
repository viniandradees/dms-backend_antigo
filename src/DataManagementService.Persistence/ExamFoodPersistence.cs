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
    public class ExamFoodPersistence : GeneralPersistence, IExamFoodPersistence
    {
        private readonly DataManagementServiceContext _context;

        public ExamFoodPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ExamFood> GetByIdAsync(int id)
        {
            IQueryable<ExamFood> query = _context.ExamFoods.AsNoTracking()
                .Include(dd => dd.Exam)
                .Include(dd => dd.Food);

            query = query.Where(dd => dd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ExamFood> GetByRelatedIdAsync(int examId, int foodId) 
        {
            IQueryable<ExamFood> query = _context.ExamFoods.AsNoTracking()
                .Include(dd => dd.Exam)
                .Include(dd => dd.Food);

            query = query.Where(dd => dd.ExamId == examId && dd.FoodId == foodId);

            return await query.FirstOrDefaultAsync();
        }
    }
}