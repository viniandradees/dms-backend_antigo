using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain;

namespace DataManagementService.Persistence.Interfaces
{
    public interface IExamPersistence : IGeneralPersistence
    {
        Task<Exam[]> GetAllAsync(bool getFullData = false);
        Task<Exam> GetByIdAsync(int id);
        Task<Exam> GetByNameAsync(string name);
    }
}