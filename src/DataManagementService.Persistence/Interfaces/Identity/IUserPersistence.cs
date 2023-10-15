using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataManagementService.Domain.Identity;

namespace DataManagementService.Persistence.Interfaces.Idenity
{
    public interface IUserPersistence : IGeneralPersistence
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<User> GetByUserNameAsync(string name);
    }
}