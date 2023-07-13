using ExpenseTracking.Core.Models.Concrete;
using ExpenseTracking.Core.Repositories;
using ExpenseTracking.Repository.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracking.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<User>> GetUsersWithRole()
        {
            //Eager Loading
            return _context.Users.AsNoTracking().Include(x => x.Role).AsEnumerable();
        }

        public Task<User> GetUserWithRole(int id)
        {
            //Eager Loading
            return _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
