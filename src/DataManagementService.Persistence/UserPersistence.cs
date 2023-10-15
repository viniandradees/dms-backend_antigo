
using DataManagementService.Domain.Identity;
using DataManagementService.Persistence.Contexts;
using DataManagementService.Persistence.Interfaces.Idenity;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Persistence
{
    public class UserPersistence : GeneralPersistence, IUserPersistence
    {
        private readonly DataManagementServiceContext _context;
        public UserPersistence(DataManagementServiceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByUserNameAsync(string name)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName.ToLower() == "admin");
        }
    }
}