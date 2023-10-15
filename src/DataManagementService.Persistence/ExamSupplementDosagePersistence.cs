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
    public class ExamSupplementDosagePersistence : GeneralPersistence, IExamSupplementDosagePersistence
    {
        private readonly DataManagementServiceContext _context;

        public ExamSupplementDosagePersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ExamSupplementDosage> GetByIdAsync(int id)
        {
            IQueryable<ExamSupplementDosage> query = _context.ExamSupplementDosages.AsNoTracking();

            query = query.Where(ddd => ddd.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<ExamSupplementDosage> GetByRelatedIdAsync(int examSupplementId, int measurementUnitId) 
        {
            IQueryable<ExamSupplementDosage> query = _context.ExamSupplementDosages.AsNoTracking();

            query = query.Where(ddd => ddd.ExamSupplementId == examSupplementId && ddd.MeasurementUnitId == measurementUnitId);

            return await query.FirstOrDefaultAsync();
        }
    }
}